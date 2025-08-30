using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.Studenti;
using Smis.Mappers;
using Smis.Mappers.TranskriptaNotaveMapper;
using Smis.Models;
using AutoMapper;
using Smis.Dtos.RegjistrimiSemestrit;
using Smis.Dtos.GrupiDto;
using Microsoft.AspNetCore.Authorization;


namespace Smis.Controllers
{
    //[Authorize]
    [Route("api/studenti")]
    [ApiController]
    public class StudentiController : ControllerBase
    {
        private readonly SmisContext _context;
        private readonly TranskriptaNotaveMapper _notaMapper;
        private readonly IMapper _mapper;
        public StudentiController(SmisContext smisContext, IMapper mapper)
        {
            _context = smisContext;
            _notaMapper = new TranskriptaNotaveMapper();
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var studentet = _context.Studenti.ToList()
            .Select(s => s.ToStudentiDto());
            return Ok(studentet);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var studenti = _context.Studenti.Find(id);
            if (studenti == null)
            {
                return NotFound();
            }
            return Ok(studenti.ToStudentiDto());
        }
        [HttpPost]
        public IActionResult Create([FromBody] StudentiDto studentiDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var studentModel = studentiDto.ToStudentiFromCreateDTO();
            _context.Studenti.Add(studentModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = studentModel.StudentiId }, new
            {
                message = "Studenti u shtua me sukses",
                studentiId = studentModel.StudentiId
            });
        }
        [HttpPost("regjistrosemestrin")]
        public async Task<IActionResult> RegjistroSemestrin([FromBody] CreateRegjistrimiSemestritDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var regjistrimiSemestrit = _mapper.Map<RegjistrimiSemestrit>(dto);
            _context.RegjistrimiSemestrit.Add(regjistrimiSemestrit);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(RegjistroSemestrin), new { id = regjistrimiSemestrit.RegjistrimiSemestritId }, new
            {
                message = "Regjistrimi i semestrit u krye me sukses",
                regjistrimiId = regjistrimiSemestrit.RegjistrimiSemestritId
            });
        }
        [HttpDelete("cregjistrosemestrin/{id}")]
        public async Task<IActionResult> DeleteRegjistrimiSemestrit(int id)
        {
            var regjistrimiSemestrit = await _context.RegjistrimiSemestrit.FindAsync(id);
            if (regjistrimiSemestrit == null)
            {
                return NotFound();
            }
            _context.RegjistrimiSemestrit.Remove(regjistrimiSemestrit);
            await _context.SaveChangesAsync();
            return Ok(new { message = " Semestri u c'regjistrua me sukses." });
        }

        [HttpGet("{studentId}/regjistrimi-semestrit")]
        public async Task<IActionResult> GetRegjistrimiSemestritPerStudent(int studentId)
        {
            var regjistrimi = await _context.RegjistrimiSemestrit
                .Where(r => r.StudentiId == studentId)
                .FirstOrDefaultAsync();

            if (regjistrimi == null)
                return NotFound();

            return Ok(regjistrimi);
        }

        [HttpGet("transkripta/{studentId}")]
        public async Task<IActionResult> GjeneroTranskripten(int studentId)
        {
            var notat = await _context.Nota
             .Include(n => n.ParaqitjaEprovimit)
                 .ThenInclude(p => p.Lenda)
             .Where(n => n.StudentiId == studentId && !n.EshteRefuzuar)
             .ToListAsync();

            if (notat == null || !notat.Any())
            {
                return NotFound("Nuk u gjet asnjë notë për këtë student.");
            }

            var notatDto = _notaMapper.MapToDto(notat);
            return Ok(notatDto);


        }
        [HttpPut("zgjedhgrupin")]
        public async Task<IActionResult> ZgjedhGrupin([FromBody] ZgjedhGrupinDto dto)
        {
            var studenti = await _context.Studenti.FindAsync(dto.StudentiId);
            if (studenti == null)
                return NotFound("Studenti nuk u gjet.");

            var grupi = await _context.Grupi.FindAsync(dto.GrupiId);
            if (grupi == null)
                return NotFound("Grupi nuk u gjet.");

            studenti.GrupiId = dto.GrupiId;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Grupi u ruajt me sukses." });
        }


    }
}
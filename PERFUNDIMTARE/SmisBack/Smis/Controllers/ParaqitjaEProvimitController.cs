using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smis.Dtos.ParaqitjaEProvimitDto;
using Smis.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;
using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers
{
    [Authorize]
    [Route("api/paraqitjaeprovimit")]
    [ApiController]

    public class ParaqitjaEProvimitController : ControllerBase
    {
        private readonly SmisContext _context;
        private readonly IMapper _mapper;

        public ParaqitjaEProvimitController(SmisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetParaqitjaEProvimitDto>>> GetParaqitjaEProvimit()
        {
            var paraqitjaEProvimit = await _context.ParaqitjaEprovimit.ToListAsync();
            var paraqitjaEProvimitDto = _mapper.Map<List<GetParaqitjaEProvimitDto>>(paraqitjaEProvimit);
            return Ok(paraqitjaEProvimitDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetParaqitjaEProvimitDto>> GetParaqitjaEProvimitByID(int id)
        {
            var paraqitjaEProvimit = await _context.ParaqitjaEprovimit.FirstOrDefaultAsync(p => p.ParaqitjaId == id);
            if (paraqitjaEProvimit == null) return NotFound();
            var paraqitjaEProvimitDto = _mapper.Map<GetParaqitjaEProvimitDto>(paraqitjaEProvimit);
            return Ok(paraqitjaEProvimitDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParaqitjaEProvimit([FromBody] CreateEditParaqitjaEProvimitDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var provimi = await _context.Provimi
        .FirstOrDefaultAsync(p => p.LendaId == dto.LendaId && p.StafiAkademikId == dto.StafiId);
            if (provimi == null) return NotFound("Nuk ekziston një provim për këtë lëndë nga ligjëruesi i zgjedhur.");
            var paraqitjaEProvimit = new ParaqitjaEprovimit
            {
                LendaId = dto.LendaId,
                StudentiId = dto.StudentiId,
                StafiAkademikId = dto.StafiId
            };
            _context.ParaqitjaEprovimit.Add(paraqitjaEProvimit);
            await _context.SaveChangesAsync();


            return CreatedAtAction(nameof(CreateParaqitjaEProvimit), new { id = paraqitjaEProvimit.ParaqitjaId }, new
            {
                message = "Provimi u paraqit me sukses",
                paraqitjaId = paraqitjaEProvimit.ParaqitjaId
            });


        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateParaqitjaEProvimit(int id, [FromBody] CreateEditParaqitjaEProvimitDto dto)
        {
            var paraqitjaEProvimit = await _context.ParaqitjaEprovimit.FindAsync(id);
            if (paraqitjaEProvimit == null) return NotFound();
            _mapper.Map(dto, paraqitjaEProvimit);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Paraqitja e Provimit u përditësua me sukses" });

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParaqitjaEProvimit(int id)
        {
            var paraqitjaEProvimit = await _context.ParaqitjaEprovimit.FindAsync(id);
            if (paraqitjaEProvimit == null) return NotFound();

            var ekzistonNota = await _context.Nota.AnyAsync(n => n.ParaqitjaId == id);
            if (ekzistonNota)
                return BadRequest("Nuk mund të anulohet paraqitja sepse ekziston një notë për këtë provim. Së pari duhet të fshihet nota.");
            _context.ParaqitjaEprovimit.Remove(paraqitjaEProvimit);
            await _context.SaveChangesAsync();
            return Ok(new { message = $" Paraqitja e Provimitme ID= {id} u fshi me sukses" });
        }
        [HttpGet("ligjeruesit-per-lende/{lendaId}")]
        public async Task<IActionResult> MerrLigjeruesitPerLende(int lendaId)
        {
            var ligjeruesit = await _context.Ligjerata
                .Where(l => l.LendaId == lendaId)
                .Include(l => l.Stafi)
                .ThenInclude(s => s.Useri)
                .Select(l => new
                {
                    l.StafiId,
                    Emri = l.Stafi.Useri.Emri,
                    Mbiemri = l.Stafi.Useri.Mbiemri
                })
                .ToListAsync();

            return Ok(ligjeruesit);
        }
        [HttpGet("provimet-e-paraqitura/studenti/{studentiId}")]
        public async Task<IActionResult> GetParaqitjetEStudentit(int studentiId)
        {
            var paraqitjet = await _context.ParaqitjaEprovimit
            .Where(p => p.StudentiId == studentiId)
            .Include(p => p.Lenda)
            .Include(p => p.StafiAkademik)
            .ThenInclude(sa => sa.Useri)
            .ToListAsync();

            var dtoList = _mapper.Map<List<ProvimetEParaqituraDto>>(paraqitjet);
            return Ok(dtoList);

        }




    }

}
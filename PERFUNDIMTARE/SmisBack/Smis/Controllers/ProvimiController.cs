using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Models;
using Smis.Dtos.ProvimiDto;
using Microsoft.AspNetCore.Authorization;


namespace Smis.Controllers
{
    //  [Authorize(Roles = "Admin")]
    [Route("api/provimi")]
    [ApiController]

    public class ProvimiController : ControllerBase
    {
        private readonly SmisContext _context;
        private readonly IMapper _mapper;

        public ProvimiController(SmisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<GetProvimiDto>>> GetProvimi()
        {
            var provimet = await _context.Provimi
                .Include(p => p.Lenda)
                .ThenInclude(l => l.Semestri)
                .Include(p => p.StafiAkademik)
                // .Include(p => p.Orari)
                .ToListAsync();

            var provimiDtos = _mapper.Map<List<GetProvimiDto>>(provimet);
            return Ok(provimiDtos);
        }
        [HttpGet("departamenti/{departamentiId}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<GetProvimiDto>>> GetProvimiByDepartamenti(int departamentiId)
        {
            var provimet = await _context.Provimi
                .Include(p => p.Lenda)
                .ThenInclude(l => l.Semestri)
                .Include(p => p.StafiAkademik)
                .Where(p => p.StafiAkademik.DepartamentiId == departamentiId)
                // .Include(p => p.Orari)
                .ToListAsync();

            var provimiDtos = _mapper.Map<List<GetProvimiDto>>(provimet);
            return Ok(provimiDtos);
        }
        [HttpGet("{id}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<GetProvimiDto>> GetProvimiById(int id)
        {
            var provimi = await _context.Provimi
                .Include(p => p.Lenda)
                .Include(p => p.StafiAkademik)
                // .Include(p => p.Orari)
                .FirstOrDefaultAsync(p => p.ProvimiId == id);

            if (provimi == null)
            {
                return NotFound();
            }

            var provimiDto = _mapper.Map<GetProvimiDto>(provimi);
            return Ok(provimiDto);
        }

        [HttpPost]
           [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> CreateProvimi([FromBody] CreateProvimiDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var provimi = _mapper.Map<Provimi>(dto);
            _context.Provimi.Add(provimi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateProvimi), new { id = provimi.ProvimiId }, new
            {
                message = "Provimi u shtua me sukses",
                provimiId = provimi.ProvimiId
            });
        }
        [HttpPut("{id}")]
           [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> UpdateProvimi(int id, [FromBody] CreateProvimiDto dto)
        {
            var existingProvimi = await _context.Provimi.FindAsync(id);
            if (existingProvimi == null) return NotFound();

            _mapper.Map(dto, existingProvimi);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Provimi u përditësua me sukses" });
        }
        [HttpDelete]
           [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> DeleteProvimi(int id)
        {
            var provimi = await _context.Provimi.FindAsync(id);
            if (provimi == null)
            {
                return NotFound();
            }
            _context.Provimi.Remove(provimi);
            await _context.SaveChangesAsync();
            return Ok(new { message = $"Provimi me ID = {id} u fshi me sukses." });
        }

    }
}
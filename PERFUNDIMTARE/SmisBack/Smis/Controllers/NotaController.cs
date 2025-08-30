using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.NotaDto;
using Smis.Mappers.TranskriptaNotaveMapper;
using Smis.Models;
using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers

{
    // [Authorize(Roles = "SfafAkademik")]
    [Route("api/nota")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly SmisContext _context;
        private readonly IMapper _mapper;

        public NotaController(SmisContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
           [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<GetNotaDto>>> GetNotat()
        {
            var notat = await _context.Nota.ToListAsync();
            var notaDtos = _mapper.Map<List<GetNotaDto>>(notat);
            return Ok(notaDtos);
        }
        [HttpGet("studenti/{studentiId}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<GetNotaDto>>> GetNotatPerStudent(int studentiId)
        {
            var notat = await _context.Nota
                .Where(n => n.StudentiId == studentiId)
                .ToListAsync();

            var NotaDtos = _mapper.Map<List<GetNotaDto>>(notat);
            return Ok(NotaDtos);
        }

        [HttpGet("{id}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<GetNotaDto>> GetNota(int id)
        {
            var nota = await _context.Nota.FirstOrDefaultAsync(n => n.NotaId == id);

            if (nota == null)
            {
                return NotFound();
            }

            var notaDto = _mapper.Map<GetNotaDto>(nota);
            return Ok(notaDto);
        }

        [HttpPost("vendos-noten")]
           [Authorize(Roles = "StafAkademik")]

        public async Task<IActionResult> CreateNota([FromBody] CreateNotaDto notaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nota = CreateNotaMapper.ToEntity(notaDto);
            _context.Nota.Add(nota);
            var paraqitja = await _context.ParaqitjaEprovimit
       .FirstOrDefaultAsync(p => p.ParaqitjaId == notaDto.ParaqitjaId);

            if (paraqitja != null)
            {
                paraqitja.EshtePerfunduar = true;
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateNota), new { id = nota.NotaId }, new
            {
                message = "Nota u shtua me sukses",
                notaId = nota.NotaId
            });
        }

        [HttpPut("{id}")]
           [Authorize(Roles = "StafAkademik")]

        public async Task<IActionResult> UpdateNota(int id, [FromBody] CreateNotaDto dto)
        {
            var nota = await _context.Nota.FindAsync(id);
            if (nota == null)
                return NotFound();

            CreateNotaMapper.UpdateEntity(nota, dto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Nota u përditësua me sukses" });
        }
        [HttpDelete("{id}")]
           [Authorize(Roles = "StafAkademik")]

        public async Task<IActionResult> DeleteNote(int id)
        {
            var nota = await _context.Nota.FindAsync(id);
            if (nota == null)
                return NotFound();

            _context.Nota.Remove(nota);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Nota u fshi me sukses" });
        }

        [HttpPost("refuzo-noten/{notaId}")]
           [Authorize(Roles = "Student,StafAkademik")]

        public async Task<IActionResult> RefuzoNoten(int notaId)
        {
            var nota = await _context.Nota.FindAsync(notaId);
            if (nota == null)
                return NotFound();

            nota.EshteRefuzuar = true;
            await _context.SaveChangesAsync();

            return Ok("Nota u refuzua me sukses.");
        }

        [HttpPost("vendos-thjesht")]
           [Authorize(Roles = "StafAkademik")]

        public async Task<IActionResult> CreateOrUpdateNotaThjesht([FromBody] CreateNotaSimpleDto dto)
        {
            if (dto == null || dto.NotaNr < 0 || dto.NotaNr > 10)
                return BadRequest("Të dhënat janë të pavlefshme.");

            var paraqitja = await _context.ParaqitjaEprovimit
                .FirstOrDefaultAsync(p => p.ParaqitjaId == dto.ParaqitjaId);

            if (paraqitja != null)
            {
                paraqitja.EshtePerfunduar = true;
            }

            // ✅ Llogarit nota shkronjore
            char notaShkronje;
            if (dto.NotaNr >= 9) notaShkronje = 'A';
            else if (dto.NotaNr >= 8) notaShkronje = 'B';
            else if (dto.NotaNr >= 7) notaShkronje = 'C';
            else if (dto.NotaNr >= 6) notaShkronje = 'D';
            else notaShkronje = 'F';

            var nota = await _context.Nota
                .FirstOrDefaultAsync(n => n.ParaqitjaId == dto.ParaqitjaId);

            if (nota != null)
            {
                // Përditëso notën ekzistuese
                nota.NotaNr = dto.NotaNr;
                nota.NotaShkronje = notaShkronje;
                nota.DataVendosjes = DateOnly.FromDateTime(DateTime.Today);
            }
            else
            {
                // Shto notë të re
                nota = new Nota
                {
                    ParaqitjaId = dto.ParaqitjaId,
                    NotaNr = dto.NotaNr,
                    NotaShkronje = notaShkronje,
                    DataVendosjes = DateOnly.FromDateTime(DateTime.Today),
                    StudentiId = paraqitja.StudentiId
                };
                _context.Nota.Add(nota);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Nota u vendos me sukses", notaId = nota.NotaId });
        }



        [HttpGet("byParaqitja/{paraqitjaId}")]
           [Authorize(Roles = "Student,StafAkademik")]

        public async Task<ActionResult<Nota>> GetNotaByParaqitjaId(int paraqitjaId)
        {
            var nota = await _context.Nota
                .FirstOrDefaultAsync(n => n.ParaqitjaId == paraqitjaId);

            if (nota == null)
                return NotFound();

            return Ok(nota);
        }



    }
}
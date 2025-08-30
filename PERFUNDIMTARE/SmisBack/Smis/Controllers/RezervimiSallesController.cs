using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Models;
using Microsoft.AspNetCore.Authorization;

using Smis.Dtos.RezervimiDto;
using Smis.Mappers;

namespace Smis.Controllers
{
    // [Authorize(Roles = "StafAkademik")]
    [ApiController]
    [Route("api/rezervimisalles")]
    public class RezervimiSallesController : ControllerBase
    {
        private readonly SmisContext _context;

        public RezervimiSallesController(SmisContext context)
        {
            _context = context;
        }

        [HttpGet]
        // [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<RezervimiDto>>> GetAll()
        {
            // HAPI 1 - merr të dhënat nga databaza
            var rezervimet = await _context.RezervimiSalles
                .Include(r => r.Salla)
                .Include(r => r.StafiAkademik)
                    .ThenInclude(s => s.Useri)
                .ToListAsync(); // e përfundon query-n

            // HAPI 2 - transformo në DTO
            var dtoList = rezervimet.Select(r => new RezervimiDto
            {
                Id = r.RezervimiId,
                Data = r.Data,
                Koha = r.Koha,
                SallaId = r.SallaId ?? 0,
                StafiAkademikId = r.StafiAkademikId ?? 0,
                Emri = r.StafiAkademik?.Useri?.Emri ?? "",
                Mbiemri = r.StafiAkademik?.Useri?.Mbiemri ?? "",
                NrSalles = r.Salla?.NrSalles ?? "(e panjohur)"
            }).ToList();

            // HAPI 3 - kthe listën e DTO-ve
            return dtoList;

        }




        [HttpGet("{id}")]
        //    [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> GetById(int id)
        {
            var rezervimi = await _context.RezervimiSalles
                .Include(r => r.Salla)
                .Include(r => r.StafiAkademik)
                .FirstOrDefaultAsync(r => r.RezervimiId == id);

            if (rezervimi == null)
                return NotFound();

            return Ok(rezervimi.ToRezervimiDto());
        }

        [HttpPost]
        //     [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> RezervoSallen([FromBody] CreateRezervimiDto dto)
        {
            if (dto.Data == null || dto.Koha == null)
                return BadRequest("Data ose Koha mungon");

            var stafi = await _context.StafiAkademik.FindAsync(dto.StafiAkademikId);
            var salla = await _context.Salla.FindAsync(dto.SallaId);

            if (stafi == null || salla == null)
                return NotFound("Stafi ose Salla nuk ekziston");

            var rezervimi = new RezervimiSalles
            {

                StafiAkademikId = dto.StafiAkademikId,
                SallaId = dto.SallaId,
                Data = DateOnly.FromDateTime(dto.Data.Value),
                Koha = TimeOnly.FromTimeSpan(dto.Koha.Value)
            };

            _context.RezervimiSalles.Add(rezervimi);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Rezervimi u bë me sukses" });

        }


        [HttpPut("{id}")]
        //     [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> Update(int id, [FromBody] CreateRezervimiDto dto)
        {
            var rezervimi = await _context.RezervimiSalles.FindAsync(id);
            if (rezervimi == null)
                return NotFound();

            if (dto.Data == null || dto.Koha == null)
                return BadRequest("Data ose Koha mungon");

            // ✅ Përditëso direkt në objektin ekzistues
            rezervimi.Data = DateOnly.FromDateTime(dto.Data.Value);
            rezervimi.Koha = TimeOnly.FromTimeSpan(dto.Koha.Value);
            rezervimi.SallaId = dto.SallaId;
            rezervimi.StafiAkademikId = dto.StafiAkademikId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Rezervimi u bë me sukses" });

        }


        [HttpDelete("{id}")]
        //     [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> Delete(int id)
        {
            var rezervimi = await _context.RezervimiSalles.FindAsync(id);
            if (rezervimi == null)
                return NotFound();

            _context.RezervimiSalles.Remove(rezervimi);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Rezervimi u bë me sukses" });

        }
    }
}

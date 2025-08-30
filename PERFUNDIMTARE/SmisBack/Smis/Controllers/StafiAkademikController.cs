using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos;
using Smis.Dtos.Stafi;
using Smis.Dtos.StafiAkademikDto;
using Smis.Mappers;
using Smis.Mappers.StafiMappers;
using Smis.Models;
using Microsoft.AspNetCore.Authorization;



namespace Smis.Controllers
{
    [Route("api/stafiakademik")]
    [ApiController]
    public class StafiAkademikController : ControllerBase
    {
        private readonly SmisContext _context;

        public StafiAkademikController(SmisContext context)
        {
            _context = context;
        }


        // [HttpPost("create-with-user")]
        // public async Task<ActionResult> CreateWithUser(CreateStafiWithUserDto dto)
        // {
        //     const int stafiRoleid = 3;
        //     // Krijo Userin nga DTO
        //     var user = new Useri
        //     {
        //         Emri = dto.Emri,
        //         Mbiemri = dto.Mbiemri,
        //         Email = dto.Email,
        //         Datelindja = dto.Datelindja,
        //         Password = dto.Password,
        //         NrLeternjoftimit = dto.NrLeternjoftimit,
        //         VendLindja = dto.VendLindja,
        //         Gjinia = dto.Gjinia,
        //         Shteti = dto.Shteti,
        //         Vendbanimi = dto.Vendbanimi,
        //         Adresa = dto.Adresa,
        //         Zipkodi = dto.Zipkodi,
        //         Telefoni = dto.Telefoni,
        //         Nenshtetesia = dto.Nenshtetesia,
        //         Foto = dto.Foto,
        //         RoleId = dto.RoleId ?? stafiRoleid,
        //     };
        //     _context.Useri.Add(user);
        //     await _context.SaveChangesAsync();

        //     // Krijo Studentin me UserID nga User i sapokrijuar
        //     var stafi = new StafiAkademik
        //     {
        //         VitiRegjistrimit = dto.VitiRegjistrimit,
        //         Roli = dto.Roli,
        //         Titulli = dto.Titulli,
        //         UserID = user.UserId,
        //         DepartamentiId = dto.DepartamentiId,
        //         UniId = dto.UniId,


        //     };

        //     _context.StafiAkademik.Add(stafi);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(GetStafi), new { id = stafi.StafiId }, stafi.ToStafiAkademikDto());

        // }







        // GET: api/stafiakademik

        [HttpGet]
          [Authorize(Roles = "Admin,SuperAdmin")]
        //[Authorize]

        public async Task<ActionResult<IEnumerable<StafiAkademikDto>>> GetAll()
        {
            var staffList = await _context.StafiAkademik
                .Include(s => s.Useri)
                .Include(s => s.Departamenti)
                .Include(s => s.Universiteti)
                .ToListAsync();

            var result = staffList.Select(stafi => new StafiAkademikDto
            {
                // Nga User
                Emri = stafi.Useri?.Emri,
                Mbiemri = stafi.Useri?.Mbiemri,
                Email = stafi.Useri?.Email,
                Datelindja = stafi.Useri?.Datelindja,
                NrLeternjoftimit = stafi.Useri?.NrLeternjoftimit,
                VendLindja = stafi.Useri?.VendLindja,
                Gjinia = stafi.Useri?.Gjinia,
                Shteti = stafi.Useri?.Shteti,
                Vendbanimi = stafi.Useri?.Vendbanimi,
                Adresa = stafi.Useri?.Adresa,
                Zipkodi = stafi.Useri?.Zipkodi,
                Telefoni = stafi.Useri?.Telefoni,
                Nenshtetesia = stafi.Useri?.Nenshtetesia,
                Foto = stafi.Useri?.Foto,

                // Nga Stafi
                StafiId = stafi.StafiId,
                VitiRegjistrimit = stafi.VitiRegjistrimit ?? 0,
                RoliStafit = stafi.RoliStafit,
                Titulli = stafi.Titulli,

                // Nga lidhjet
                DepartamentiId = stafi.DepartamentiId ?? 0,
                UniId = stafi.UniId ?? 0,
                DepartamentiEmri = stafi.Departamenti?.Emri,
                UniversitetiEmri = stafi.Universiteti?.Emri

            }).ToList();

            return Ok(result);
        }


        // GET: api/stafiakademik/{id}
        [HttpGet("{id}")]
          [Authorize(Roles = "Admin,SuperAdmin")]

        public async Task<ActionResult<StafiAkademikDto>> GetStafi(int id)
        {
            var stafi = await _context.StafiAkademik
                .Include(s => s.Useri)
                .Include(s => s.Departamenti)
                .Include(s => s.Universiteti)
                .FirstOrDefaultAsync(s => s.StafiId == id);

            if (stafi == null)
                return NotFound();

            var stafidto = new StafiAkademikDto
            {
                // Nga User
                Emri = stafi.Useri?.Emri,
                Mbiemri = stafi.Useri?.Mbiemri,
                Email = stafi.Useri?.Email,

                Datelindja = stafi.Useri?.Datelindja,
                NrLeternjoftimit = stafi.Useri?.NrLeternjoftimit,
                VendLindja = stafi.Useri?.VendLindja,
                Gjinia = stafi.Useri?.Gjinia,
                Shteti = stafi.Useri?.Shteti,
                Vendbanimi = stafi.Useri?.Vendbanimi,
                Adresa = stafi.Useri?.Adresa,
                Zipkodi = stafi.Useri?.Zipkodi,
                Telefoni = stafi.Useri?.Telefoni,
                Nenshtetesia = stafi.Useri?.Nenshtetesia,
                Foto = stafi.Useri?.Foto,

                // Nga Stafi
                StafiId = stafi.StafiId,
                VitiRegjistrimit = stafi.VitiRegjistrimit ?? 0,
                RoliStafit = stafi.RoliStafit,
                Titulli = stafi.Titulli,

                // Nga lidhjet
                DepartamentiId = stafi.DepartamentiId ?? 0,
                UniId = stafi.UniId ?? 0,
                DepartamentiEmri = stafi.Departamenti?.Emri,
                UniversitetiEmri = stafi.Universiteti?.Emri

            };

            return Ok(stafidto);
        }

        // POST: api/stafiakademik


        // PUT: api/stafiakademik/{id}
        [HttpPut("{id}")]
          [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateStafiWithUserDto dto)
        {
            var stafi = await _context.StafiAkademik
                .Include(s => s.Useri)
                .FirstOrDefaultAsync(s => s.StafiId == id);

            if (stafi == null) return NotFound();

            // Update User
            var user = stafi.Useri;
            user.Emri = dto.Emri;
            user.Mbiemri = dto.Mbiemri;
            user.Email = dto.Email;
            user.Datelindja = dto.Datelindja;
            user.NrLeternjoftimit = dto.NrLeternjoftimit;
            user.VendLindja = dto.VendLindja;
            user.Gjinia = dto.Gjinia;
            user.Shteti = dto.Shteti;
            user.Vendbanimi = dto.Vendbanimi;
            user.Adresa = dto.Adresa;
            user.Zipkodi = dto.Zipkodi;
            user.Telefoni = dto.Telefoni;
            user.Nenshtetesia = dto.Nenshtetesia;
            user.Foto = dto.Foto;


            // Update StafiAkademik
            stafi.VitiRegjistrimit = dto.VitiRegjistrimit;
            stafi.RoliStafit = dto.RoliStafit;
            stafi.Titulli = dto.Titulli;
            stafi.DepartamentiId = dto.DepartamentiId;
            stafi.UniId = dto.UniId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/stafiakademik/{id}
        [HttpDelete("{id}")]
          [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var stafi = await _context.StafiAkademik
                .Include(s => s.Useri)
                .FirstOrDefaultAsync(s => s.StafiId == id);

            if (stafi == null) return NotFound();

            _context.Useri.Remove(stafi.Useri); // fshin edhe userin përkatës
            _context.StafiAkademik.Remove(stafi);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // [HttpGet("departamenti/{departamentiId}")]
        // public async Task<ActionResult<IEnumerable<StafiAkademikDto>>> GetStaffByDepartamenti(int departamentiId)
        // {
        //     var staffList = await _context.StafiAkademik
        //         .Include(s => s.Useri)
        //         .Include(s => s.Departamenti)
        //         .Include(s => s.Universiteti)
        //         .Where(s => s.DepartamentiId == departamentiId)
        //         .ToListAsync();

        //     var result = staffList.Select(stafi => new StafiAkademikDto
        //     {
        //         // Nga User
        //         Emri = stafi.Useri?.Emri,
        //         Mbiemri = stafi.Useri?.Mbiemri,
        //         Email = stafi.Useri?.Email,
        //         Datelindja = stafi.Useri?.Datelindja,
        //         NrLeternjoftimit = stafi.Useri?.NrLeternjoftimit,
        //         VendLindja = stafi.Useri?.VendLindja,
        //         Gjinia = stafi.Useri?.Gjinia,
        //         Shteti = stafi.Useri?.Shteti,
        //         Vendbanimi = stafi.Useri?.Vendbanimi,
        //         Adresa = stafi.Useri?.Adresa,
        //         Zipkodi = stafi.Useri?.Zipkodi,
        //         Telefoni = stafi.Useri?.Telefoni,
        //         Nenshtetesia = stafi.Useri?.Nenshtetesia,
        //         Foto = stafi.Useri?.Foto,

        //         // Nga Stafi
        //         StafiId = stafi.StafiId,
        //         VitiRegjistrimit = stafi.VitiRegjistrimit ?? 0,
        //         RoliStafit = stafi.RoliStafit,
        //         Titulli = stafi.Titulli,

        //         // Nga lidhjet
        //         DepartamentiId = stafi.DepartamentiId ?? 0,
        //         UniId = stafi.UniId ?? 0,
        //         DepartamentiEmri = stafi.Departamenti?.Emri,
        //         UniversitetiEmri = stafi.Universiteti?.Emri

        //     }).ToList();

        //     return Ok(result);
        // }

        [HttpGet("universiteti/{uniId}/departamenti/{departamentiId}")]
          [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]
        public async Task<ActionResult<IEnumerable<StafiAkademikDto>>> GetStaffByUniversitetiDheDepartamenti(int uniId, int departamentiId)
        {
            var staffList = await _context.StafiAkademik
                .Include(s => s.Useri)
                .Include(s => s.Departamenti)
                .Include(s => s.Universiteti)
                .Where(s => s.DepartamentiId == departamentiId && s.UniId == uniId)
                .ToListAsync();

            var result = staffList.Select(stafi => new StafiAkademikDto
            {
                // Nga User
                Emri = stafi.Useri?.Emri,
                Mbiemri = stafi.Useri?.Mbiemri,
                Email = stafi.Useri?.Email,
                Datelindja = stafi.Useri?.Datelindja,
                NrLeternjoftimit = stafi.Useri?.NrLeternjoftimit,
                VendLindja = stafi.Useri?.VendLindja,
                Gjinia = stafi.Useri?.Gjinia,
                Shteti = stafi.Useri?.Shteti,
                Vendbanimi = stafi.Useri?.Vendbanimi,
                Adresa = stafi.Useri?.Adresa,
                Zipkodi = stafi.Useri?.Zipkodi,
                Telefoni = stafi.Useri?.Telefoni,
                Nenshtetesia = stafi.Useri?.Nenshtetesia,
                Foto = stafi.Useri?.Foto,

                // Nga Stafi
                StafiId = stafi.StafiId,
                VitiRegjistrimit = stafi.VitiRegjistrimit ?? 0,
                RoliStafit = stafi.RoliStafit,
                Titulli = stafi.Titulli,

                // Nga lidhjet
                DepartamentiId = stafi.DepartamentiId ?? 0,
                UniId = stafi.UniId ?? 0,
                DepartamentiEmri = stafi.Departamenti?.Emri,
                UniversitetiEmri = stafi.Universiteti?.Emri
            }).ToList();

            return Ok(result);
        }




    }
}

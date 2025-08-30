using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos;
using Smis.Mappers;
using Smis.Mappers.AdminMappers;
using Smis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Dtos.AdminDto;  // Update with the correct namespace
using Smis.Mappers.AdminMappers; // Update with the correct namespace
using Smis.Models; // Ensure that models are imported
using Microsoft.AspNetCore.Authorization;



namespace Smis.Controllers
{

    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly SmisContext _context;

        public AdminController(SmisContext context)
        {
            _context = context;
        }

        [HttpGet]
           [Authorize(Roles = "Admin,SuperAdmin")]


        public async Task<ActionResult<IEnumerable<AdminDto>>> GetAll()
        {
            var adminList = await _context.Admin
                .Include(a => a.User)              // Për të marrë të dhënat e User-it
                .Include(a => a.Departamenti)       // Për të marrë emrin e departamentit
                .Include(a => a.Uni)       // Për të marrë emrin e universitetit
                .ToListAsync();

            var result = adminList.Select(a => new AdminDto
            {
                AdminId = a.AdminId,
                // Password = a.User?.Password,
                DepartamentiId = a.DepartamentiId,
                DepartamentiEmri = a.Departamenti?.Emri,
                Id = a.Id,
                UniId = a.UniId,
                UniversitetiEmri = a.Uni?.Emri,
                Emri = a.User?.Emri,
                Mbiemri = a.User?.Mbiemri,
                Email = a.User?.Email,
                NrLeternjoftimit = a.User?.NrLeternjoftimit,
                Gjinia = a.User?.Gjinia,
                Datelindja = a.User?.Datelindja,
                Telefoni = a.User?.Telefoni,
                Shteti = a.User?.Shteti,
                Vendbanimi = a.User?.Vendbanimi,
                Adresa = a.User?.Adresa,
                Zipkodi = a.User?.Zipkodi,
                Nenshtetesia = a.User?.Nenshtetesia,
                Foto = a.User?.Foto
            }).ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
  [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult<AdminDto>> GetById(int id)
        {
            var admin = await _context.Admin
                .Include(a => a.User)              // Për të marrë të dhënat e User-it
                .Include(a => a.Departamenti)       // Për të marrë emrin e departamentit
                .Include(a => a.Uni)       // Për të marrë emrin e universitetit
                .FirstOrDefaultAsync(a => a.AdminId == id);

            if (admin == null)
                return NotFound();

            var adminDto = new AdminDto
            {
                AdminId = admin.AdminId,
                // Password = admin.User?.Password,
                DepartamentiId = admin.DepartamentiId,
                DepartamentiEmri = admin.Departamenti?.Emri,
                Id = admin.Id,
                UniId = admin.UniId,
                UniversitetiEmri = admin.Uni?.Emri,
                Emri = admin.User?.Emri,
                Mbiemri = admin.User?.Mbiemri,
                Email = admin.User?.Email,
                NrLeternjoftimit = admin.User?.NrLeternjoftimit,
                Gjinia = admin.User?.Gjinia,
                Datelindja = admin.User?.Datelindja,
                Telefoni = admin.User?.Telefoni,
                Shteti = admin.User?.Shteti,
                Vendbanimi = admin.User?.Vendbanimi,
                Adresa = admin.User?.Adresa,
                Zipkodi = admin.User?.Zipkodi,
                Nenshtetesia = admin.User?.Nenshtetesia,
                Foto = admin.User?.Foto
            };

            return Ok(adminDto);
        }


        [HttpPost]
          [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<ActionResult> Create(CreateAdminDto dto)
        {
            var newAdmin = dto.ToAdminEntity();
            _context.Admin.Add(newAdmin);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newAdmin.AdminId }, newAdmin);
        }





        [HttpPut("{id}")]
          [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(int id, CreateAdminWithUserDto dto)
        {
            var admin = await _context.Admin
                .Include(a => a.User) // Merr edhe Userin lidhur me Adminin
                .FirstOrDefaultAsync(a => a.AdminId == id);

            if (admin == null)
                return NotFound();

            // Përditëso fushat e Admin-it
            admin.DepartamentiId = dto.DepartamentiId;
            admin.UniId = dto.UniId;

            // Përditëso fushat e User-it
            if (admin.User != null)
            {
                admin.User.Emri = dto.Emri;
                admin.User.Mbiemri = dto.Mbiemri;
                admin.User.Email = dto.Email;
                admin.User.Datelindja = dto.Datelindja;
                admin.User.NrLeternjoftimit = dto.NrLeternjoftimit;
                admin.User.VendLindja = dto.VendLindja;
                admin.User.Gjinia = dto.Gjinia;
                admin.User.Shteti = dto.Shteti;
                admin.User.Vendbanimi = dto.Vendbanimi;
                admin.User.Adresa = dto.Adresa;
                admin.User.Zipkodi = dto.Zipkodi;
                admin.User.Telefoni = dto.Telefoni;
                admin.User.Nenshtetesia = dto.Nenshtetesia;
                admin.User.Foto = dto.Foto;
                // admin.User.Password = dto.Password; // Vetëm nëse ka vlerë të re
                // admin.User.RoleId = dto.RoleId ?? admin.User.RoleId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }




        [HttpDelete("{id}")]
          [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> DeleteAdminWithUser(int id)
        {
            // Gjej adminin sipas id
            var admin = await _context.Admin.FindAsync(id);
            if (admin == null) return NotFound();

            // Merr UserId që i përket këtij admini
            var Id = admin.Id;

            // Fshi Adminin
            _context.Admin.Remove(admin);

            // Ruaj ndryshimet për Adminin (nëse ke cascade, mund të presësh para fshirjes së User-it)
            await _context.SaveChangesAsync();

            // Gjej Userin dhe fshi Userin
            var user = await _context.Useri.FindAsync(Id);
            if (user != null)
            {
                _context.Useri.Remove(user);
                await _context.SaveChangesAsync();
            }

            return NoContent();
        }




        // [HttpPost("create-with-user")]
        // public async Task<ActionResult> CreateAdminWithUser(CreateAdminWithUserDto dto)
        // {
        //     const int adminRoleId = 1; // Përdorim rolin e adminit

        //     // Krijo Userin nga DTO
        //     var user = new Useri
        //     {
        //         Emri = dto.Emri,
        //         Mbiemri = dto.Mbiemri,
        //         Email = dto.Email,
        //         Datelindja = dto.Datelindja,
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
        //         // RoleId = dto.RoleId ?? adminRoleId,
        //         // Password = dto.Password // 🔐 Shto fjalëkalimin (hash-uar nga frontend)
        //         // Defaulti për adminin është 1
        //     };

        //     _context.Useri.Add(user);
        //     await _context.SaveChangesAsync(); // Ruajmë përdoruesin

        //     // Krijo Adminin me UserID nga User i sapokrijuar
        //     var admin = new Admin
        //     {
        //         Id = user.Id, // Përdorim Id të përdoruesit që e krijuam
        //         DepartamentiId = dto.DepartamentiId,
        //         UniId = dto.UniId
        //     };

        //     _context.Admin.Add(admin);
        //     await _context.SaveChangesAsync(); // Ruajmë adminin

        //     // Kthe një adminDTO si përgjigje
        //     return CreatedAtAction(nameof(GetById), new { id = admin.AdminId }, admin.ToAdminDto());
        // }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos;
using Smis.Dtos.StudentDtoERA;
using Smis.Mappers;
using Smis.Mappers.StudentiMappersERA;
using Smis.Models;
using Microsoft.AspNetCore.Authorization;

namespace Smis.Controllers
{
    
    [Route("api/studentiera")]
    [ApiController]
    public class StudentiControllerEra : ControllerBase
    {
        private readonly SmisContext _context;

        public StudentiControllerEra(SmisContext context)
        {
            _context = context;
        }


        [HttpGet]
           [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<StudentiDtoERA>>> GetAllStudents()
        {
            // PROVË: Përdor UniId të fiksuar (p.sh. UniId = 1)
            int currentUniId = 1;

            var students = await _context.Studenti
                .Include(s => s.Useri)
                .Include(s => s.Departamenti)
                .Include(s => s.Semestri)
                .Where(s => s.UniId == currentUniId)  // ➕ Filtrimi sipas UniId
                .ToListAsync();

            var studentDtos = students.Select(s => new StudentiDtoERA
            {
                StudentiId = s.StudentiId,
                VitiRegjistrimit = s.VitiRegjistrimit,
                Statusi = s.Statusi,
                UniId = s.UniId,
                DepartamentiId = s.DepartamentiId,
                DepartamentiEmri = s.Departamenti?.Emri,
                GrupiId = s.GrupiId,
                Id = s.Id,
                Emri = s.Useri?.Emri,
                Mbiemri = s.Useri?.Mbiemri,
                Email = s.Useri?.Email,
                NrLeternjoftimit = s.Useri?.NrLeternjoftimit,
                Gjinia = s.Useri?.Gjinia,
                Datelindja = s.Useri?.Datelindja,
                Telefoni = s.Useri?.Telefoni,
                SemestriId = s.SemestriID,
                SemestriEmri = s.Semestri?.Semestri1
            }).ToList();

            return Ok(studentDtos);
        }
        // [HttpGet("departamenti/{departamentiId}")]
        // public async Task<ActionResult<IEnumerable<StudentiDtoERA>>> GetAllStudentsByDepartamenti(int departamentiId)
        // {
        //     // PROVË: Përdor UniId të fiksuar (p.sh. UniId = 1)
        //     // int currentUniId = 1;

        //     var students = await _context.Studenti
        //      .Include(s => s.Useri)
        //      .Include(s => s.Departamenti)
        //      .Include(s => s.Semestri)
        //      .Include(s => s.Grupi)
        //      .Where(s => s.DepartamentiId == departamentiId)
        //      .ToListAsync();

        //     var studentDtos = students.Select(s => new StudentiDtoERA
        //     {
        //         StudentiId = s.StudentiId,
        //         VitiRegjistrimit = s.VitiRegjistrimit,
        //         Statusi = s.Statusi,
        //         UniId = s.UniId,
        //         DepartamentiId = s.DepartamentiId,
        //         DepartamentiEmri = s.Departamenti?.Emri,
        //         GrupiId = s.GrupiId,
        //         Id = s.Id,
        //         Emri = s.Useri?.Emri,
        //         Mbiemri = s.Useri?.Mbiemri,
        //         Email = s.Useri?.Email,
        //         NrLeternjoftimit = s.Useri?.NrLeternjoftimit,
        //         Gjinia = s.Useri?.Gjinia,
        //         Datelindja = s.Useri?.Datelindja,
        //         Telefoni = s.Useri?.Telefoni,
        //         SemestriId = s.SemestriID,
        //         SemestriEmri = s.Semestri?.Semestri1,
        //         GrupiEmri = s.Grupi?.Emri
        //     }).ToList();

        //     return Ok(studentDtos);
        // }

        [HttpGet("universiteti/{uniId}/departamenti/{departamentiId}")]
           [Authorize(Roles = "Admin,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<IEnumerable<StudentiDtoERA>>> GetAllStudentsByUniversitetiDheDepartamenti(int uniId, int departamentiId)
        {
            var students = await _context.Studenti
                .Include(s => s.Useri)
                .Include(s => s.Departamenti)
                .Include(s => s.Semestri)
                .Include(s => s.Grupi)
                .Where(s => s.DepartamentiId == departamentiId && s.UniId == uniId)
                .ToListAsync();

            var studentDtos = students.Select(s => new StudentiDtoERA
            {
                StudentiId = s.StudentiId,
                VitiRegjistrimit = s.VitiRegjistrimit,
                Statusi = s.Statusi,
                UniId = s.UniId,
                DepartamentiId = s.DepartamentiId,
                DepartamentiEmri = s.Departamenti?.Emri,
                GrupiId = s.GrupiId,
                Id = s.Id,
                Emri = s.Useri?.Emri,
                Mbiemri = s.Useri?.Mbiemri,
                Email = s.Useri?.Email,
                NrLeternjoftimit = s.Useri?.NrLeternjoftimit,
                Gjinia = s.Useri?.Gjinia,
                Datelindja = s.Useri?.Datelindja,
                Telefoni = s.Useri?.Telefoni,
                SemestriId = s.SemestriID,
                SemestriEmri = s.Semestri?.Semestri1,
                GrupiEmri = s.Grupi?.Emri
            }).ToList();

            return Ok(studentDtos);
        }




        [HttpGet("{id}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<ActionResult<StudentiDtoERA>> GetStudent(int id)
        {
            var student = await _context.Studenti
                .Include(s => s.Useri)              // për të marrë të dhënat e User-it
                .Include(s => s.Departamenti)       // për të marrë emrin e departamentit
                .Include(s => s.Semestri)            // për të marrë emrin e semestrit
                .Include(s => s.Universiteti)        // për të marrë emrin e universitetit
                .FirstOrDefaultAsync(s => s.StudentiId == id);

            if (student == null)
                return NotFound();

            var studentDto = new StudentiDtoERA
            {
                StudentiId = student.StudentiId,
                VitiRegjistrimit = student.VitiRegjistrimit,
                Statusi = student.Statusi,
                UniId = student.UniId,
                UniversitetiEmri = student.Universiteti?.Emri,
                DepartamentiId = student.DepartamentiId,
                DepartamentiEmri = student.Departamenti?.Emri,
                GrupiId = student.GrupiId,
                Id = student.Id,
                Emri = student.Useri?.Emri,
                Mbiemri = student.Useri?.Mbiemri,
                Email = student.Useri?.Email,
                NrLeternjoftimit = student.Useri?.NrLeternjoftimit,
                VendLindja = student.Useri?.VendLindja,
                Gjinia = student.Useri?.Gjinia,
                Datelindja = student.Useri?.Datelindja,
                Shteti = student.Useri?.Shteti,
                Vendbanimi = student.Useri?.Vendbanimi,
                Adresa = student.Useri?.Adresa,
                Zipkodi = student.Useri?.Zipkodi,
                Telefoni = student.Useri?.Telefoni,
                Nenshtetesia = student.Useri?.Nenshtetesia,
                Foto = student.Useri?.Foto,
                SemestriId = student.SemestriID,
                SemestriEmri = student.Semestri?.Semestri1
            };

            return Ok(studentDto);
        }



        // [HttpPost]
        // public async Task<ActionResult> CreateStudent(CreateStudentiRequestDto createDto)
        // {
        //     var student = createDto.ToStudenti();
        //     _context.Studenti.Add(student);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(GetStudent), new { id = student.StudentiId }, student.ToStudentiDto());
        // }

        [HttpGet("grupi/{grupiId}")]
           [Authorize(Roles = "Admin,Student,StafAkademik,SuperAdmin")]

        public async Task<IActionResult> GetStudentetByGrupi(int grupiId)
        {
            var studentet = await _context.Studenti
                .Where(s => s.GrupiId == grupiId)
                .Include(s => s.Useri)
                .Select(s => new
                {
                    s.StudentiId,
                    s.Useri.Emri,
                    s.Useri.Mbiemri,
                    s.Useri.Email
                })
                .ToListAsync();

            return Ok(studentet);
        }







        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateStudentWithUserDto dto)
        {
            var student = await _context.Studenti
                .Include(s => s.Useri)  // Merr userin lidhur me studentin
                .FirstOrDefaultAsync(s => s.StudentiId == id);

            if (student == null)
                return NotFound();

            // Përditëso fushat e studentit
            student.VitiRegjistrimit = dto.VitiRegjistrimit;
            student.Statusi = dto.Statusi;
            student.UniId = dto.UniId;
            student.DepartamentiId = dto.DepartamentiId;
            student.GrupiId = dto.GrupiId;
            student.SemestriID = dto.SemestriID;

            // Përditëso fushat e User-it
            if (student.Useri != null)
            {
                student.Useri.Emri = dto.Emri;
                student.Useri.Mbiemri = dto.Mbiemri;
                student.Useri.Email = dto.Email;
                student.Useri.Datelindja = dto.Datelindja;
                student.Useri.NrLeternjoftimit = dto.NrLeternjoftimit;
                student.Useri.VendLindja = dto.VendLindja;
                student.Useri.Gjinia = dto.Gjinia;
                student.Useri.Shteti = dto.Shteti;
                student.Useri.Vendbanimi = dto.Vendbanimi;
                student.Useri.Adresa = dto.Adresa;
                student.Useri.Zipkodi = dto.Zipkodi;
                student.Useri.Telefoni = dto.Telefoni;
                student.Useri.Nenshtetesia = dto.Nenshtetesia;
                student.Useri.Foto = dto.Foto;

            }

            await _context.SaveChangesAsync();

            return NoContent();
        }








        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentAndUser(int id)
        {
            var student = await _context.Studenti.FindAsync(id);
            if (student == null)
                return NotFound("Studenti nuk u gjet.");

            var user = await _context.Useri.FindAsync(student.Id);
            if (user == null)
                return NotFound("Useri i studentit nuk u gjet.");

            _context.Studenti.Remove(student);
            _context.Useri.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // [HttpPost("create-with-user")]
        // public async Task<ActionResult> CreateStudentWithUser(CreateStudentWithUserDto dto)
        // {
        //     const int studentRoleId = 2;
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


        //     };


        //     _context.Useri.Add(user);
        //     await _context.SaveChangesAsync();

        //     // Krijo Studentin me UserID nga User i sapokrijuar
        //     var student = new Studenti
        //     {
        //         VitiRegjistrimit = dto.VitiRegjistrimit,
        //         Statusi = dto.Statusi,
        //         UniId = dto.UniId,
        //         DepartamentiId = dto.DepartamentiId,
        //         GrupiId = dto.GrupiId,
        //         SemestriID = dto.SemestriID,
        //         UserID = user.UserId
        //     };

        //     _context.Studenti.Add(student);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(GetStudent), new { id = student.StudentiId }, student.ToStudentiDto());
        // }





    }
}

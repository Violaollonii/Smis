using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.StudentDtoERA
{
    public class CreateStudentWithUserDto
    {
        // Fusha nga Useri
        public string? Emri { get; set; }
        public string? Mbiemri { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public DateOnly? Datelindja { get; set; }
        public string? NrLeternjoftimit { get; set; }
        public string? VendLindja { get; set; }
        public string? Gjinia { get; set; }
        public string? Shteti { get; set; }
        public string? Vendbanimi { get; set; }
        public string? Adresa { get; set; }
        public string? Zipkodi { get; set; }
        public string? Telefoni { get; set; }
        public string? Nenshtetesia { get; set; }
        public string? Foto { get; set; }
        public int? RoleId { get; set; }
        // Fusha nga Studenti
        public int? VitiRegjistrimit { get; set; }
        public string? Statusi { get; set; }
        public int? UniId { get; set; }
        public int? DepartamentiId { get; set; }
        public int? GrupiId { get; set; }
        public int SemestriID { get; set; }
        public string? SemestriEmri { get; set; }
        public string? DepartamentiEmri { get; set; }
        public string? UniversitetiEmri { get; set; }
        public string? GrupiEmri { get; set; }
    }
}

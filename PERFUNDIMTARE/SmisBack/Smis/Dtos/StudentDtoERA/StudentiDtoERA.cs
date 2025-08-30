using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Smis.Dtos.StudentDtoERA
{
    public class StudentiDtoERA
    {
        public int StudentiId { get; set; }
        public int? VitiRegjistrimit { get; set; }
        public string? Statusi { get; set; }

        public int? UniId { get; set; }
        public string? UniversitetiEmri { get; set; }  // Nëse do ta përdorësh
        public int? DepartamentiId { get; set; }
        public string? DepartamentiEmri { get; set; }
        public int? GrupiId { get; set; }
        public string Id { get; set; }
        public string? Emri { get; set; }
        public string? Mbiemri { get; set; }
        public string? Email { get; set; }
        public string? NrLeternjoftimit { get; set; }
        public string? VendLindja { get; set; }
        public string? Gjinia { get; set; }
        public DateOnly? Datelindja { get; set; }
        public string? Shteti { get; set; }
        public string? Vendbanimi { get; set; }
        public string? Adresa { get; set; }
        public string? Zipkodi { get; set; }
        public string? Telefoni { get; set; }
        public string? Nenshtetesia { get; set; }
        public string? Foto { get; set; }
        public int? SemestriId { get; set; }
        public string? SemestriEmri { get; set; }
        public string? GrupiEmri { get; set; }
    }

}

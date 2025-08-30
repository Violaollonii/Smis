using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Smis.Models;

namespace Smis.Dtos.AccountDto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string? Emri { get; set; }
        public string? Mbiemri { get; set; }
        public string? Roli { get; set; }

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

        // ===== ADMIN =====
        public int? DepartamentiId { get; set; }
        public int? UniId { get; set; }
        public string? DepartamentiEmri { get; set; }
        public string? UniversitetiEmri { get; set; }

        // ===== STUDENT =====
        public int? VitiRegjistrimit { get; set; }
        public string? Statusi { get; set; }
        public int? GrupiId { get; set; }
        public int? SemestriID { get; set; }

        // ===== STAFI =====
        public string? RoliStafit { get; set; }
        public string? Titulli { get; set; }
    }
}

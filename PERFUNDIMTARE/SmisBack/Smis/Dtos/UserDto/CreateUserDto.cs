using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.Useri
{
    public class CreateUserDto
    {
        public string? Emri { get; set; }
        public string? Mbiemri { get; set; }
        public string? Email { get; set; }
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

    }
}
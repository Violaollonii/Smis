using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.NotaDto
{
    public class NotaDto
    {
        public decimal? NotaNr { get; set; }
        public string? Emri { get; set; }
        public int? Kredite { get; set; }
        public char? NotaShkronje { get; set; }
        public string? Kategoria { get; set; }
    }
}
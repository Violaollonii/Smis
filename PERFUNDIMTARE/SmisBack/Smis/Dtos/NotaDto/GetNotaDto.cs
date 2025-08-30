using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.NotaDto
{
    public class GetNotaDto
    {
        public int NotaId { get; set; }

        public decimal? NotaNr { get; set; }
        public char? NotaShkronje { get; set; }

        public DateOnly? DataVendosjes { get; set; }

        public int? StudentiId { get; set; }

        public int? ParaqitjaId { get; set; }
        public bool EshteRefuzuar { get; set; } = false;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.NotaDto
{
    public class CreateNotaDto
    {
        public decimal NotaNr { get; set; }
        public char NotaShkronje { get; set; }
        public int StudentiId { get; set; }
        public int? ParaqitjaId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.StafiAkademikDto
{
    public class CreateStafiAkademikDto
    {
        public int VitiRegjistrimit { get; set; }
        public string? RoliStafit { get; set; }
        public string? Titulli { get; set; }
        public string? Password { get; set;}

        public int DepartamentiId { get; set; }
        public int UniId { get; set; }
    }
}

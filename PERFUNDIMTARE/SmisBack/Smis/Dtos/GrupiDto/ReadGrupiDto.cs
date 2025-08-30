using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.GrupiDto
{
    public class ReadGrupiDto
    {
        public int? GrupiId { get; set; }
        public string? Emri { get; set; }
        public string? Semestri1 { get; set; }
        public string? EmriDepartamentit { get; set; }
        public int? DepartamentiId { get; set; }
         public int? Kapaciteti { get; set; }
        public bool? Aktiv { get; set; }
    }
}
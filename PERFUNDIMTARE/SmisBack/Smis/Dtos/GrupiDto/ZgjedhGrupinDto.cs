using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.GrupiDto
{
    public class ZgjedhGrupinDto
    {


        public int StudentiId { get; set; }
        public int GrupiId { get; set; }
         public int? Kapaciteti { get; set; }
        public bool? Aktiv { get; set; }

    }
}
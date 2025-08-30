using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.GrupiDto
{
    public class CreateGrupiDto
    {


        public string? Emri { get; set; }

        public int? SemestriId { get; set; }

        public int? DepartamentiId { get; set; }

        public int? OrariId { get; set; }

        public int? Kapaciteti { get; set; }
        public bool? Aktiv { get; set; }

    }
}
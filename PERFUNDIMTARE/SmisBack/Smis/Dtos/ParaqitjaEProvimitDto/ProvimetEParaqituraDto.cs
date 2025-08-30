using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.ParaqitjaEProvimitDto
{
    public class ProvimetEParaqituraDto
    {
        public int ParaqitjaId { get; set; }
        public int? LendaId { get; set; }
        public string? Emri { get; set; }
        public int StafiAkademikId { get; set; }

        public string? EmriStafit { get; set; }

        public string? Mbiemri { get; set; }

        public string? Kategoria { get; set; }
        public bool EshtePerfunduar { get; set; } = false;



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.ParaqitjaEProvimitDto
{
    public class GetParaqitjaEProvimitDto
    {
        public int ParaqitjaId { get; set; }

        public DateOnly? DataProvimit { get; set; }

        public int? StudentiId { get; set; }

        public int? LendaId { get; set; }

        public bool EshtePerfunduar { get; set; } = false;

    }
}
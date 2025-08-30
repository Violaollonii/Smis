using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.OrariDto
{

    public class OrariDto
    {
        public int OrariId { get; set; }

        public DateOnly? Data { get; set; }

        public TimeOnly? Koha { get; set; }

        public string? Lloji { get; set; }

        public string? Ndërrimi { get; set; }
    }
}
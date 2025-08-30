using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.ProvimiDto
{
    public class CreateProvimiDto
    {
        public string? Statusi { get; set; }

        public int StafiAkademikId { get; set; }

        public int LendaId { get; set; }

        public int OrariId { get; set; }
    }
}
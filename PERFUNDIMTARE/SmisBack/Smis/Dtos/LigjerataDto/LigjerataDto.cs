using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Models;

namespace Smis.Dtos.Ligjerata
{
    public class LigjerataDto
    {
        public int StafiId { get; set; }
        public int LendaId { get; set; }

        public string? LendaEmri { get; set; }
        public string? ProfesoriEmri { get; set; }
        // Nëse do, mund të shtosh fusha shtesë që do të kthehen në përgjigje
    }
}

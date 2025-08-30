using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Models;

namespace Smis.Dtos.Ligjerata
{
    public class CreateLigjerataDto
    {
        public int StafiId { get; set; }
        public int LendaId { get; set; }
        public string? EmriLendes { get; set; } // Kjo është prona e re për emrin e lëndës
        public string? ProfesoriEmri { get; set; }
        // Nëse ke nevojë për fusha shtesë gjatë krijimit/ndryshimit, shtoji këtu
    }
}

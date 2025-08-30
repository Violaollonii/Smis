using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.ProvimiDto
{
    public class GetProvimiDto
    {
        public int ProvimiId { get; set; }
        public string? Statusi { get; set; }

        public int? LendaId { get; set; }
        public string? Emri { get; set; }
        public int? Kredite { get; set; }
        public string? Kategoria { get; set; }
        public int? StafiAkademikId { get; set; }
        public string? Semestri1 { get; set; }
        public int? OrariId { get; set; }
        // public DateOnly DataProvimit { get; set; }

    }
}
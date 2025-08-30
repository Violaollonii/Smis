using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.ParaqitjaEProvimitDto
{
    public class ParaqitjaEProvimitViewDto
    {
        public int ParaqitjaId { get; set; }
        public string KodiLendes { get; set; }
        public string EmriLendes { get; set; }
        public string Kategoria { get; set; }
        public string Studenti { get; set; }
        public decimal? Nota { get; set; }
        public string StatusiNotes { get; set; }
        public DateTime? DataVendosjes { get; set; }
        public int? NotaId { get; set; }
    }
}

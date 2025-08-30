using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.RegjistrimiSemestrit
{
    public class CreateRegjistrimiSemestritDto
    {
        public int StudentiId { get; set; }

        public int SemestriId { get; set; }

        public DateTime DataRegjistrimit { get; set; }
        public string Lokacioni { get; set; }
        public string Nderrimi { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Models
{
    public class RegjistrimiSemestrit
    {
        public int RegjistrimiSemestritId { get; set; }

        public int StudentiId { get; set; }
        public Studenti Studenti { get; set; }

        public int SemestriId { get; set; }
        public Semestri Semestri { get; set; }

        public DateTime DataRegjistrimit { get; set; }
        public string Lokacioni { get; set; }
        public string Nderrimi { get; set; }
    }
}
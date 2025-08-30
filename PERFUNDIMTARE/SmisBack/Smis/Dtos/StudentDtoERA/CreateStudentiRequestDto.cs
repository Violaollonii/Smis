using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.Studenti
{
    public class CreateStudentiRequestDto
    {
        public int? VitiRegjistrimit { get; set; }
        public string? Statusi { get; set; }
        public string? Password { get; set; }

        public int? UniId { get; set; }
        public int? DepartamentiId { get; set; }
        public int? GrupiId { get; set; }
        public int UserID { get; set; }
        public int SemestriID { get; set; }
    }
}

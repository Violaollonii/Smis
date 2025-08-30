using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Smis.Dtos.Departamenti
{
    public class CreateDepartamentiDto
    {
        public string? Emri { get; set; }
        public int? UniId { get; set; }
        public string? UniversitetiEmri { get; set; }
    }
}

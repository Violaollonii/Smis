using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.UniversitetiDto
{
    public class GetUniversitetiDto
    {
        public int UniId { get; set; }
        public string? Emri { get; set; }

        public string? Adress { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
        public List<LokacionetDto> Lokacionet { get; set; } = new List<LokacionetDto>();
    }
}
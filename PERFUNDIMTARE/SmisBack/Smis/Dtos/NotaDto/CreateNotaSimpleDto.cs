using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

using System.Threading.Tasks;

namespace Smis.Dtos.NotaDto
{

    public class CreateNotaSimpleDto
    {
        [Required]
        public int ParaqitjaId { get; set; }

        [Required]
        [Range(0, 10)]
        public decimal NotaNr { get; set; }
    }
}
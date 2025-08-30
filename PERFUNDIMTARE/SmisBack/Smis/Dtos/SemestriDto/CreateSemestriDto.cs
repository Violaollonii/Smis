using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Models;

namespace Smis.Dtos.Semestri
{
    public class CreateSemestriDto
    {
        public string? Semestri1 { get; set; }
        public int? UniId { get; set; }
    }
}

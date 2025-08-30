using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.SallaDto
{
    public class UpdateSallaDto
    {
            public int SallaId { get; set; }

    public string? NrSalles { get; set; }

    public int? Kapaciteti { get; set; }

    public string? Objekti { get; set; }

    public int? StafiAkademikId { get; set; }

    public int? UniId { get; set; }
    }
}
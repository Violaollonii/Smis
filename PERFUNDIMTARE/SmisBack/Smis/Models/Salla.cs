using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Salla
{
    public int SallaId { get; set; }

    public string? NrSalles { get; set; }

    public int? Kapaciteti { get; set; }

    public string? Objekti { get; set; }

    public int? StafiAkademikId { get; set; }

    public int? UniId { get; set; }

    public virtual StafiAkademik? StafiAkademik { get; set; }

    public virtual Universiteti? Uni { get; set; }

    public virtual ICollection<Orari> Orari { get; set; } = new List<Orari>();
}

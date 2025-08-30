using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Provimi
{
    public int ProvimiId { get; set; }

    public string? Statusi { get; set; }

    public int? StafiAkademikId { get; set; }

    public int? LendaId { get; set; }

    public int? OrariId { get; set; }

    public virtual Lenda? Lenda { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();

    public virtual Orari? Orari { get; set; }

    public virtual StafiAkademik? StafiAkademik { get; set; }
}

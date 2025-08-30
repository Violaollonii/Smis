using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Ligjerata
{
    public int StafiId { get; set; }

    public int LendaId { get; set; }

    public virtual Lenda Lenda { get; set; } = null!;

    public virtual StafiAkademik Stafi { get; set; } = null!;

    public virtual ICollection<Orari> Orari { get; set; } = new List<Orari>();
}

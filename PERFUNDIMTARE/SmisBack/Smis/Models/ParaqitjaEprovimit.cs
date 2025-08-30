using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class ParaqitjaEprovimit
{
    public int ParaqitjaId { get; set; }

    public DateOnly? DataProvimit { get; set; }

    public int? StudentiId { get; set; }

    public int? LendaId { get; set; }
    public int StafiAkademikId { get; set; } // pa `?` nëse është i detyrueshëm
    public bool EshtePerfunduar { get; set; } = false;

    public virtual Lenda? Lenda { get; set; }

    public virtual Studenti? Studenti { get; set; }

    public virtual StafiAkademik StafiAkademik { get; set; }
    public virtual Nota? Nota { get; set; }
}

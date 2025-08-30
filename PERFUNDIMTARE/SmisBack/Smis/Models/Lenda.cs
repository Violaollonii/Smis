using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Lenda
{
    public int LendaId { get; set; }

    public string? Emri { get; set; }

    public int? Kredite { get; set; }
    public string? Kategoria { get; set; }

    public int? DepartamentiId { get; set; }

    public int? SemestriId { get; set; }

    public virtual Departamenti? Departamenti { get; set; }

    public virtual ICollection<Ligjerata> Ligjerata { get; set; } = new List<Ligjerata>();

    public virtual ICollection<ParaqitjaEprovimit> ParaqitjaEprovimit { get; set; } = new List<ParaqitjaEprovimit>();

    public virtual ICollection<Provimi> Provimi { get; set; } = new List<Provimi>();

    public virtual Semestri? Semestri { get; set; }
}

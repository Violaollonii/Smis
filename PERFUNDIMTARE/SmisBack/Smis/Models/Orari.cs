using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Orari
{
    public int OrariId { get; set; }

    public DateOnly? Data { get; set; }

    public TimeOnly? Koha { get; set; }

    public string? Lloji { get; set; }

    public string? Ndërrimi { get; set; }

    public virtual ICollection<Grupi> Grupi { get; set; } = new List<Grupi>();

    public virtual ICollection<Provimi> Provimi { get; set; } = new List<Provimi>();

    public virtual ICollection<Ligjerata> Ligjerata { get; set; } = new List<Ligjerata>();

    public virtual ICollection<Salla> Salla { get; set; } = new List<Salla>();
}

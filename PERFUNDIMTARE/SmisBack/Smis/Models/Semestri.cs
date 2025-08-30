using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Semestri
{
    public int SemestriId { get; set; }

    public string? Semestri1 { get; set; }

    public int? UniId { get; set; }

    public virtual ICollection<Grupi> Grupi { get; set; } = new List<Grupi>();

    public virtual ICollection<Lenda> Lenda { get; set; } = new List<Lenda>();

    public virtual Universiteti? Universiteti { get; set; }
    // public virtual ICollection<Studenti> Studentet { get; set; } = new List<Studenti>();
    public virtual ICollection<RegjistrimiSemestrit> RegjistrimiSemestrit { get; set; } = new List<RegjistrimiSemestrit>();
}

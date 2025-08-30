using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Studenti
{
    public int StudentiId { get; set; }

    public int? VitiRegjistrimit { get; set; }

    public string? Statusi { get; set; }

    public int? UniId { get; set; }

    public int? DepartamentiId { get; set; }

    public int? GrupiId { get; set; }
    public string Id { get; set; }
    public int? SemestriID { get; set; }


    public virtual Semestri? Semestri { get; set; }


    public virtual Useri Useri { get; set; }

    public virtual Departamenti Departamenti { get; set; }

    public virtual Grupi? Grupi { get; set; }

    public virtual ICollection<Nota> Nota { get; set; } = new List<Nota>();

    public virtual ICollection<Pagesa> Pagesa { get; set; } = new List<Pagesa>();

    public virtual ICollection<ParaqitjaEprovimit> ParaqitjaEprovimit { get; set; } = new List<ParaqitjaEprovimit>();
    public virtual ICollection<RegjistrimiSemestrit> RegjistrimiSemestrit { get; set; } = new List<RegjistrimiSemestrit>();

    public virtual Universiteti Universiteti { get; set; }
}

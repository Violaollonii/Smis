using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class StafiAkademik
{
    public int StafiId { get; set; }

    public int? VitiRegjistrimit { get; set; }

    public string? RoliStafit { get; set; }

    public string? Titulli { get; set; }

    public int? DepartamentiId { get; set; }

    public int? UniId { get; set; }
    public string Id { get; set; }


    public virtual Useri? Useri { get; set; }

    public virtual Departamenti? Departamenti { get; set; }

    public virtual ICollection<Ligjerata> Ligjerata { get; set; } = new List<Ligjerata>();

    public virtual ICollection<Provimi> Provimi { get; set; } = new List<Provimi>();

    public virtual ICollection<Salla> Salla { get; set; } = new List<Salla>();

    public virtual Universiteti? Universiteti { get; set; }
}

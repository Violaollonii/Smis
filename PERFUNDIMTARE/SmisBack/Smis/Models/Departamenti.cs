using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Departamenti
{
    public int DepartamentiId { get; set; }

    public string? Emri { get; set; }

    public int? UniId { get; set; }

    public virtual ICollection<Admin> Admin { get; set; } = new List<Admin>();

    public virtual ICollection<Grupi> Grupi { get; set; } = new List<Grupi>();

    public virtual ICollection<Lenda> Lenda { get; set; } = new List<Lenda>();

    public virtual ICollection<StafiAkademik> StafiAkademik { get; set; } = new List<StafiAkademik>();

    public virtual ICollection<Studenti> Studenti { get; set; } = new List<Studenti>();

    public virtual Universiteti? Universiteti { get; set; }
}

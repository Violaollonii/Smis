using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Universiteti
{
    public int UniId { get; set; }

    public string? Emri { get; set; }

    public string? Adress { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Admin> Admin { get; set; } = new List<Admin>();

    public virtual ICollection<Departamenti> Departamenti { get; set; } = new List<Departamenti>();

    public virtual ICollection<Salla> Salla { get; set; } = new List<Salla>();

    public virtual ICollection<Semestri> Semestri { get; set; } = new List<Semestri>();

    public virtual ICollection<StafiAkademik> StafiAkademik { get; set; } = new List<StafiAkademik>();

    public virtual ICollection<Studenti> Studenti { get; set; } = new List<Studenti>();
    public List<Lokacionet> Lokacionet { get; set; } = new List<Lokacionet>();
}

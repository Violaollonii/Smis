using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Grupi
{
    public int GrupiId { get; set; }

    public string? Emri { get; set; }

    public int? SemestriId { get; set; }

    public int? DepartamentiId { get; set; }

    public int? OrariId { get; set; }

    // ➕ Shtuar
    public int? Kapaciteti { get; set; }   // Kapaciteti i grupit

    public bool Aktiv { get; set; }        // Aktiv/Joaktiv

    public virtual Departamenti? Departamenti { get; set; }

    public virtual Orari? Orari { get; set; }

    public virtual Semestri? Semestri { get; set; }

    public virtual ICollection<Studenti> Studenti { get; set; } = new List<Studenti>();
}

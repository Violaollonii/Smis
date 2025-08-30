using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Nota
{
    public int NotaId { get; set; }

    public decimal? NotaNr { get; set; }
    public char? NotaShkronje { get; set; }

    public DateOnly? DataVendosjes { get; set; }

    public int? StudentiId { get; set; }

    public int? ParaqitjaId { get; set; }
    public bool EshteRefuzuar { get; set; } = false;


    public virtual ParaqitjaEprovimit? ParaqitjaEprovimit { get; set; }

    public virtual Studenti? Studenti { get; set; }


}

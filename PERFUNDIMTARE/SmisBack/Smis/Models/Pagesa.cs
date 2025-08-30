using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Pagesa
{
    public int PagesaId { get; set; }

    public decimal? Shuma { get; set; }

    public DateOnly? DataPageses { get; set; }

    public int? StudentiId { get; set; }

    public virtual Studenti? Studenti { get; set; }
}

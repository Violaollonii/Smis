using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int DepartamentiId { get; set; }

    public string Id { get; set; }

    public int UniId { get; set; }

    public virtual Departamenti Departamenti { get; set; }

    public virtual Universiteti Uni { get; set; }

    public virtual Useri User { get; set; }
}

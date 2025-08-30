using System;
using System.Collections.Generic;

namespace Smis.Models;

public partial class SuperAdmin
{
    public int SuperAdminId { get; set; }
    public string Id { get; set; }


    public virtual Useri Useri { get; set; }


}

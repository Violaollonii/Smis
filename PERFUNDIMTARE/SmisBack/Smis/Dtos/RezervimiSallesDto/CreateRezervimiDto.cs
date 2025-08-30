using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smis.Dtos.RezervimiDto
{
public class CreateRezervimiDto
{


      public int StafiAkademikId { get; set; }
    public int SallaId { get; set; }
    public DateTime? Data { get; set; } 
    public TimeSpan? Koha { get; set; } 
}
}
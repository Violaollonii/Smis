using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace Smis.Dtos.RezervimiDto
{
    public class RezervimiDto
    {
     
      public int Id { get; set; }
    public DateOnly Data { get; set; }

public TimeOnly? Koha { get; set; }

    public int SallaId { get; set; }
    public int StafiAkademikId { get; set; }
    public string? Emri { get; set; }
public string? Mbiemri { get; set; }

    public string NrSalles { get; set; }
    }
}

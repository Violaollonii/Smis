using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Models;

public class GetSemestriDto
{
    public int SemestriId { get; set; }
    public string Semestri1 { get; set; }
    public int UniId { get; set; }
    public string UniversitetiEmri { get; set; } // KJO është e domosdoshme
}

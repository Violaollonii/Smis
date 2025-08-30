using System.ComponentModel.DataAnnotations;

namespace Smis.Models;

public class RezervimiSalles
{
    [Key]
    public int RezervimiId { get; set; }

    public DateOnly Data { get; set; }
    public TimeOnly Koha { get; set; }

    

    public int? SallaId { get; set; }
    public Salla? Salla { get; set; }

    public int? StafiAkademikId { get; set; }
    public StafiAkademik? StafiAkademik { get; set; }
}

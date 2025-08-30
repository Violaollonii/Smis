namespace Smis.Dtos.Lenda
{
    public class CreateLendaDto
    {

        public string? Emri { get; set; }
        public int? Kredite { get; set; }
        public string? Kategoria { get; set; }
        public int? DepartamentiId { get; set; }
        public int? SemestriId { get; set; }
        public string? Semestri1 { get; set; } // Emri i semestrit


    }
}

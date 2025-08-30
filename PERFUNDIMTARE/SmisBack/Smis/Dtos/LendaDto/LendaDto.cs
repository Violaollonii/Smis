namespace Smis.Dtos.Lenda
{
    public class LendaDto
    {
        public string? DepartamentiEmri { get; set; }
        public int LendaId { get; set; }
        public string? Emri { get; set; }
        public int? Kredite { get; set; }
        public int? DepartamentiId { get; set; }
        public int? SemestriId { get; set; }
        public string? Semestri1 { get; set; } // Emri i semestrit

        public string? Kategoria { get; set; }
    }
}

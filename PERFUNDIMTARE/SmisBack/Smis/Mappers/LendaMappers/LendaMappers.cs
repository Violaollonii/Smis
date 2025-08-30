using Smis.Dtos.Lenda;
using Smis.Models;

namespace Smis.Mappers.LendaMappers
{
    public static class LendaMapper
    {
        public static Lenda ToLenda(this CreateLendaDto dto)
        {
            return new Lenda
            {
                Emri = dto.Emri,
                Kredite = dto.Kredite,
                DepartamentiId = dto.DepartamentiId,
                SemestriId = dto.SemestriId,
                Kategoria = dto.Kategoria
            };
        }

        public static LendaDto ToLendaDto(this Lenda lenda)
        {
            return new LendaDto
            {
                LendaId = lenda.LendaId,
                Emri = lenda.Emri,
                Kredite = lenda.Kredite,
                DepartamentiId = lenda.DepartamentiId,
                DepartamentiEmri = lenda.Departamenti != null ? lenda.Departamenti.Emri : null,
                SemestriId = lenda.SemestriId,
                Semestri1 = lenda.Semestri != null ? lenda.Semestri.Semestri1 : null, // Kjo është ndryshimi
                Kategoria = lenda.Kategoria

            };
        }
    }
}

using Smis.Models;
using Smis.Dtos.Ligjerata;

namespace Smis.Mappers.LigjerataMappers
{
    public static class LigjerataMappers
    {
        // Konverton nga Model në DTO për lexim
        public static LigjerataDto ToLigjerataDto(this Ligjerata ligjerata)
        {
            return new LigjerataDto
            {
                StafiId = ligjerata.StafiId,
                LendaId = ligjerata.LendaId,
                LendaEmri = ligjerata.Lenda != null ? ligjerata.Lenda.Emri : null,
                ProfesoriEmri = ligjerata.Stafi?.Useri != null ?
                        $"{ligjerata.Stafi.Useri.Emri} {ligjerata.Stafi.Useri.Mbiemri}" : "Pa të dhëna"
            };
        }

        // Konverton nga DTO për krijim në Model
        public static Ligjerata ToLigjerata(this CreateLigjerataDto dto)
        {
            return new Ligjerata
            {
                StafiId = dto.StafiId,
                LendaId = dto.LendaId,

            };
        }

        // Përditëson modelin ekzistues nga DTO
        public static void UpdateEntity(this Ligjerata ligjerata, CreateLigjerataDto dto)
        {
            ligjerata.StafiId = dto.StafiId;
            ligjerata.LendaId = dto.LendaId;

        }
    }
}

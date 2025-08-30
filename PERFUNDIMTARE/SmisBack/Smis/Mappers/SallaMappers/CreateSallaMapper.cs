using System;
using Smis.Dtos.SallaDto;
using Smis.Models;

namespace Smis.Mappers
{
    public static class CreateSallaMapper
    {
        public static Salla ToSallaFromCreateDto(this CreateSallaDto dto)
        {
            return new Salla
            {
           
              
                NrSalles = dto.NrSalles,
                Kapaciteti = dto.Kapaciteti,
                Objekti = dto.Objekti,
                StafiAkademikId = dto.StafiAkademikId,
                UniId = dto.UniId
            };
        }
    }
}

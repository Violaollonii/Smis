using System;
using Smis.Dtos.OrariDto;
using Smis.Models;

namespace Smis.Mappers
{
    public static class CreateOrariMapper
    {
        public static Orari ToOrariFromCreateDTO(this CreateOrariDto dto)
        {
            return new Orari
            {
                Data = dto.Data.HasValue ? DateOnly.FromDateTime(dto.Data.Value) : null,
                Koha = dto.Koha,
                Lloji = dto.Lloji,
                Ndërrimi = dto.Ndërrimi
            };
        }
    }
}

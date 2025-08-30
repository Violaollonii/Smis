using System;
using Smis.Dtos.OrariDto;
using Smis.Models;

namespace Smis.Mappers
{
    public static class UpdateOrariMapper
    {
        public static void UpdateEntity(this Orari orari, CreateOrariDto dto)
        {

            orari.Data = dto.Data.HasValue ? DateOnly.FromDateTime(dto.Data.Value) : null;

            orari.Koha = dto.Koha;
            orari.Lloji = dto.Lloji;
            orari.Ndërrimi = dto.Ndërrimi;
        }
    }
}

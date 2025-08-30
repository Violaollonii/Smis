using System;
using Smis.Dtos.OrariDto;
using Smis.Models;

namespace Smis.Mappers
{
    public static class OrariMapper
    {
        public static OrariDto ToOrariDto(this Orari orari)
        {
            return new OrariDto
            {
                OrariId = orari.OrariId,
                Data = orari.Data,
                Koha = orari.Koha,
                Lloji = orari.Lloji,
                Ndërrimi = orari.Ndërrimi
            };
        }
    }
}

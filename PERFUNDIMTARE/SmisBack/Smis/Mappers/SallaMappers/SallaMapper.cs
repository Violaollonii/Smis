using System;
using Smis.Dtos.SallaDto;
using Smis.Models;

namespace Smis.Mappers
{
    public static class SallaMapper
    {
        public static SallaDto ToSallaDto(this Salla salla)
        {
            return new SallaDto
            {
               SallaId = salla.SallaId, 
                NrSalles = salla.NrSalles,
                Kapaciteti = salla.Kapaciteti,
                Objekti = salla.Objekti,
                StafiAkademikId = salla.StafiAkademikId,
                UniId = salla.UniId
            };
        }
    }
}

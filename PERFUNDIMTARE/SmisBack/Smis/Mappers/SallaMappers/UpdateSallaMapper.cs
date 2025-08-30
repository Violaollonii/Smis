using System;
using Smis.Dtos.SallaDto;
using Smis.Models;

namespace Smis.Mappers
{
    public static class UpdateSallaMapper
    {
  
        public static void UpdateEntity(this Salla salla, CreateSallaDto dto)
        {
         
            salla.NrSalles = dto.NrSalles;
            salla.Kapaciteti = dto.Kapaciteti;
            salla.Objekti = dto.Objekti;
            salla.StafiAkademikId = dto.StafiAkademikId;
            salla.UniId = dto.UniId;
   
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Dtos.NotaDto;
using Smis.Models;

namespace Smis.Mappers.TranskriptaNotaveMapper
{
    public static class CreateNotaMapper
    {
        public static Nota ToEntity(CreateNotaDto dto)
        {
            return new Nota
            {
                NotaNr = dto.NotaNr,
                NotaShkronje = dto.NotaShkronje,
                StudentiId = dto.StudentiId,
                ParaqitjaId = dto.ParaqitjaId,
                DataVendosjes = DateOnly.FromDateTime(DateTime.UtcNow)
            };
        }
        public static void UpdateEntity(Nota nota, CreateNotaDto dto)
        {
            nota.NotaNr = dto.NotaNr;
            nota.NotaShkronje = dto.NotaShkronje;
            nota.StudentiId = dto.StudentiId;
            nota.ParaqitjaId = dto.ParaqitjaId;

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Dtos.NotaDto;
using Smis.Models;

namespace Smis.Mappers.TranskriptaNotaveMapper
{
    public class TranskriptaNotaveMapper
    {
        public List<NotaDto> MapToDto(List<Nota> notat)
        {
            if (notat == null) return new List<NotaDto>();

            return notat.Select(n => new NotaDto
            {
                NotaNr = n.NotaNr,
                Emri = n.ParaqitjaEprovimit?.Lenda?.Emri,
                Kredite = n.ParaqitjaEprovimit?.Lenda?.Kredite,
                NotaShkronje = n.NotaShkronje,
                Kategoria = n.ParaqitjaEprovimit?.Lenda?.Kategoria
            }).ToList();
        }
    }
}
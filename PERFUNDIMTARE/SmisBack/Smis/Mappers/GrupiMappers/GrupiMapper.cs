using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Smis.Dtos.GrupiDto;
using Smis.Models;

namespace Smis.Mappers.GrupiMappers
{
    public class GrupiMapper
    {
        private readonly SmisContext _context;

        public GrupiMapper(SmisContext context)
        {
            _context = context;
        }

        // public async Task<Grupi> ToEntityAsync(CreateGrupiDto dto)
        // {
        //     var semestri = await _context.Semestri.FirstOrDefaultAsync(s => s.Semestri1 == dto.Semestri1);
        //     var departamenti = await _context.Departamenti.FirstOrDefaultAsync(d => d.Emri == dto.EmriDepartamentit);
        //     if (semestri == null)
        //         throw new Exception($"Semestri '{dto.Semestri1}' not found.");
        //     if (departamenti == null)
        //         throw new Exception($"Departamenti '{dto.EmriDepartamentit}' not found.");

        //     return new Grupi
        //     {
        //         Emri = dto.EmriGrupit,
        //         SemestriId = semestri.SemestriId,
        //         DepartamentiId = departamenti.DepartamentiId

        //     };

        // }

        public ReadGrupiDto ToReadDto(Grupi grupi)
        {
            return new ReadGrupiDto
            {
                GrupiId = grupi.GrupiId,
                Emri = grupi.Emri,
                Semestri1 = grupi.Semestri?.Semestri1,
                EmriDepartamentit = grupi.Departamenti?.Emri,
                DepartamentiId = grupi.Departamenti.DepartamentiId,
                Kapaciteti = grupi.Kapaciteti,               // e re
                Aktiv = grupi.Aktiv   
            };
        }


    }
}
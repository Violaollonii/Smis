using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Dtos.Studenti;
using Smis.Models;

namespace Smis.Mappers
{
    public static class StudentiMapper
    {
        public static StudentiDto ToStudentiDto(this Studenti studentModel)
        {
            return new StudentiDto
            {



            };
        }
        public static Studenti ToStudentiFromCreateDTO(this StudentiDto studentiDto)
        {
            return new Studenti
            {

                VitiRegjistrimit = studentiDto.VitiRegjistrimit,
                Statusi = studentiDto.Statusi,
                UniId = studentiDto.UniId,
                DepartamentiId = studentiDto.DepartamentiId,
                GrupiId = studentiDto.GrupiId,
                Id = studentiDto.Id,
                // SemestriID = studentiDto.SemestriID

            };
        }

    }
}
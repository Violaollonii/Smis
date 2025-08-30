using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Dtos.Useri;
using Smis.Models;

namespace Smis.Mappers.UserMappers
{
    public static class CreateUserMapper
    {
        public static Useri ToUserEntity(this CreateUserDto dto)
        {
            return new Useri
            {
                Emri = dto.Emri,
                Mbiemri = dto.Mbiemri,
                Email = dto.Email,
                Datelindja = dto.Datelindja,
                NrLeternjoftimit = dto.NrLeternjoftimit,
                VendLindja = dto.VendLindja,
                Gjinia = dto.Gjinia,
                Shteti = dto.Shteti,
                Vendbanimi = dto.Vendbanimi,
                Adresa = dto.Adresa,
                Zipkodi = dto.Zipkodi,
                Telefoni = dto.Telefoni,
                Nenshtetesia = dto.Nenshtetesia,
                Foto = dto.Foto,
                // RoleId = dto.RoleId
            };
        }
    }
}
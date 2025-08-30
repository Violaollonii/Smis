using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Dtos.Useri;
using Smis.Models;

namespace Smis.Mappers.UserMappers
{
    public static class UpdateUserMapper
    {
        public static void UpdateEntity(this Useri user, CreateUserDto dto)
        {
            user.Emri = dto.Emri;
            user.Mbiemri = dto.Mbiemri;
            user.Email = dto.Email;
            user.Datelindja = dto.Datelindja;
            user.NrLeternjoftimit = dto.NrLeternjoftimit;
            user.VendLindja = dto.VendLindja;
            user.Gjinia = dto.Gjinia;
            user.Shteti = dto.Shteti;
            user.Vendbanimi = dto.Vendbanimi;
            user.Adresa = dto.Adresa;
            user.Zipkodi = dto.Zipkodi;
            user.Telefoni = dto.Telefoni;
            user.Nenshtetesia = dto.Nenshtetesia;
            user.Foto = dto.Foto;
        }
    }
}
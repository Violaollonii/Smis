using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Smis.Dtos;
using Smis.Models;

namespace Smis.Mappers
{
    public static class UserProfileMapper
    {
        public static UserProfileDto ToUserProfileDto(this Useri user)
        {
            return new UserProfileDto
            {
                Id = user.Id,
                Emri = user.Emri,
                Mbiemri = user.Mbiemri,
                Email = user.Email,
                Datelindja = user.Datelindja,
                NrLeternjoftimit = user.NrLeternjoftimit,
                VendLindja = user.VendLindja,
                Gjinia = user.Gjinia,
                Shteti = user.Shteti,
                Vendbanimi = user.Vendbanimi,
                Adresa = user.Adresa,
                Zipkodi = user.Zipkodi,
                Telefoni = user.Telefoni,
                Nenshtetesia = user.Nenshtetesia,
                Foto = user.Foto
            };
        }

    }
}
using Smis.Models;
using Smis.Dtos.Stafi;
using Smis.Dtos.StafiAkademikDto;
using Smis.Dtos.Stafi;

using Smis.Dtos.Stafi;
using Smis.Models;

namespace Smis.Mappers.StafiMappers
{
    public static class CreateStafiWithUserMapper
    {
        public static Useri ToUser(this CreateStafiWithUserDto dto)
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

            };
        }

        public static StafiAkademikDto ToStafiAkademikDto(this StafiAkademik stafi)
        {
            return new StafiAkademikDto
            {
                Emri = stafi.Useri?.Emri,
                Mbiemri = stafi.Useri?.Mbiemri,
                Email = stafi.Useri?.Email,
                Datelindja = stafi.Useri?.Datelindja,
                NrLeternjoftimit = stafi.Useri?.NrLeternjoftimit,
                VendLindja = stafi.Useri?.VendLindja,
                Gjinia = stafi.Useri?.Gjinia,
                Shteti = stafi.Useri?.Shteti,
                Vendbanimi = stafi.Useri?.Vendbanimi,
                Adresa = stafi.Useri?.Adresa,
                Zipkodi = stafi.Useri?.Zipkodi,
                Telefoni = stafi.Useri?.Telefoni,
                Nenshtetesia = stafi.Useri?.Nenshtetesia,
                Foto = stafi.Useri?.Foto,


                StafiId = stafi.StafiId,
                VitiRegjistrimit = stafi.VitiRegjistrimit ?? 0,
                RoliStafit = stafi.RoliStafit,
                Titulli = stafi.Titulli,

                DepartamentiId = stafi.DepartamentiId ?? 0,
                UniId = stafi.UniId ?? 0,
                DepartamentiEmri = stafi.Departamenti?.Emri,
                UniversitetiEmri = stafi.Universiteti?.Emri

            };
        }

        public static StafiAkademik ToStafiAkademik(this CreateStafiWithUserDto dto)
        {
            return new StafiAkademik
            {
                VitiRegjistrimit = dto.VitiRegjistrimit,
                RoliStafit = dto.RoliStafit,
                Titulli = dto.Titulli,
                DepartamentiId = dto.DepartamentiId,
                UniId = dto.UniId
            };
        }

    }
}

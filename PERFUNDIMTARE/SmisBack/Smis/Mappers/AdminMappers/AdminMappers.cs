using Smis.Models;
using Smis.Dtos.AdminDto;

namespace Smis.Mappers.AdminMappers
{
    public static class AdminMapper
    {
        public static AdminDto ToAdminDto(this Admin admin) => new AdminDto
        {
            DepartamentiEmri = admin.Departamenti != null ? admin.Departamenti.Emri : null,
            UniversitetiEmri = admin.Uni != null ? admin.Uni.Emri : null,  // Kontroll për null
            AdminId = admin.AdminId,
            DepartamentiId = admin.DepartamentiId,
            Id = admin.Id,
            UniId = admin.UniId,
            Emri = admin.User?.Emri,
            Mbiemri = admin.User?.Mbiemri,
            Email = admin.User?.Email,
            NrLeternjoftimit = admin.User?.NrLeternjoftimit,
            VendLindja = admin.User?.VendLindja,
            Gjinia = admin.User?.Gjinia,
            Datelindja = admin.User?.Datelindja,
            Shteti = admin.User?.Shteti,
            Vendbanimi = admin.User?.Vendbanimi,
            Adresa = admin.User?.Adresa,
            Zipkodi = admin.User?.Zipkodi,
            Telefoni = admin.User?.Telefoni,
            Nenshtetesia = admin.User?.Nenshtetesia,
            Foto = admin.User?.Foto,
        };

        public static Admin ToAdminEntity(this CreateAdminDto dto)
        {
            return new Admin
            {
                DepartamentiId = dto.DepartamentiId.Value,
                Id = dto.Id,
                UniId = dto.UniId.Value
            };
        }

        public static void UpdateEntity(this Admin admin, CreateAdminDto dto)
        {
            admin.DepartamentiId = dto.DepartamentiId.Value;
            admin.Id = dto.Id;
            admin.UniId = dto.UniId.Value;
        }
    }
}

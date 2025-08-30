using Smis.Dtos.Departamenti;
using Smis.Models;

namespace Smis.Mappers.DepartamentiMappers
{
    public static class DepartamentiMapper
    {
        public static Departamenti ToDepartamenti(this CreateDepartamentiDto dto)
        {
            return new Departamenti
            {
                Emri = dto.Emri,
                UniId = dto.UniId
            };
        }

        public static DepartamentiDto ToDepartamentiDto(this Departamenti departamenti)
        {
            return new DepartamentiDto
            {
                DepartamentiId = departamenti.DepartamentiId,
                Emri = departamenti.Emri,
                UniId = departamenti.UniId,
                UniversitetiEmri = departamenti.Universiteti != null ? departamenti.Universiteti.Emri : null
            };
        }
    }
}

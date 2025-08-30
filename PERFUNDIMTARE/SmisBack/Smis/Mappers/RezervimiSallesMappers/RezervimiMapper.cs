using Smis.Dtos.RezervimiDto;
using Smis.Models;

namespace Smis.Mappers
{
    public static class RezervimiMapper
    {
        public static RezervimiDto ToRezervimiDto(this RezervimiSalles rezervimi)
        {
            return new RezervimiDto
            {
               
                Id = rezervimi.RezervimiId,
                Data = rezervimi.Data,
                 Koha = rezervimi.Koha,





                SallaId = rezervimi.SallaId ?? 0,
                StafiAkademikId = rezervimi.StafiAkademikId ?? 0
            };
        }
    }
}

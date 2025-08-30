using Smis.Models;
using Smis.Dtos.Studenti;
using Smis.Dtos.StudentDtoERA;

namespace Smis.Mappers.StudentiMappersERA
{
    public static class StudentiMappersERA
    {
        // Konverton Model -> DTO me të gjitha fushat
        public static StudentiDtoERA ToStudentiDto(this Studenti student)
        {
            return new StudentiDtoERA
            {
                StudentiId = student.StudentiId,
                VitiRegjistrimit = student.VitiRegjistrimit,
                Statusi = student.Statusi,
                UniId = student.UniId,
                UniversitetiEmri = student.Universiteti != null ? student.Universiteti.Emri : null,  // Kontroll për null
                DepartamentiId = student.DepartamentiId,
                DepartamentiEmri = student.Departamenti != null ? student.Departamenti.Emri : null,
                GrupiId = student.GrupiId,
                Id = student.Id,
                Emri = student.Useri?.Emri,
                Mbiemri = student.Useri?.Mbiemri,
                Email = student.Useri?.Email,
                NrLeternjoftimit = student.Useri?.NrLeternjoftimit,
                VendLindja = student.Useri?.VendLindja,
                Gjinia = student.Useri?.Gjinia,
                Datelindja = student.Useri?.Datelindja,
                Shteti = student.Useri?.Shteti,
                Vendbanimi = student.Useri?.Vendbanimi,
                Adresa = student.Useri?.Adresa,
                Zipkodi = student.Useri?.Zipkodi,
                Telefoni = student.Useri?.Telefoni,
                Nenshtetesia = student.Useri?.Nenshtetesia,
                Foto = student.Useri?.Foto,
                SemestriId = student.SemestriID,
                SemestriEmri = student.Semestri != null ? student.Semestri.Semestri1 : null
            };
        }

        // Konverton DTO për krijim -> Model
        // public static Studenti ToStudenti(this CreateStudentiRequestDto dto)
        // {
        //     return new Studenti
        //     {
        //         VitiRegjistrimit = dto.VitiRegjistrimit,
        //         Statusi = dto.Statusi,
        //         UniId = dto.UniId,
        //         DepartamentiId = dto.DepartamentiId,
        //         GrupiId = dto.GrupiId,
        //         Id = dto.Id,
        //         SemestriID = dto.SemestriID
        //     };
        // }

        // Përditëson një model ekzistues nga DTO
        // public static void UpdateEntity(this Studenti student, CreateStudentiRequestDto dto)
        // {
        //     student.VitiRegjistrimit = dto.VitiRegjistrimit;
        //     student.Statusi = dto.Statusi;
        //     student.UniId = dto.UniId;
        //     student.DepartamentiId = dto.DepartamentiId;
        //     student.GrupiId = dto.GrupiId;
        //     student.Id = dto.Id;
        //     student.SemestriID = dto.SemestriID;

        // }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Smis.Dtos.GrupiDto;
using Smis.Dtos.NotaDto;
using Smis.Dtos.ParaqitjaEProvimitDto;
using Smis.Dtos.ProvimiDto;
using Smis.Models;
using Smis.Dtos.RegjistrimiSemestrit;
using Smis.Dtos.Semestri;
using Smis.Dtos.UniversitetiDto;

namespace Smis.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Provimi, GetProvimiDto>()
    .ForMember(dest => dest.Emri, opt => opt.MapFrom(src => src.Lenda != null ? src.Lenda.Emri : null))
    .ForMember(dest => dest.Kredite, opt => opt.MapFrom(src => src.Lenda != null ? src.Lenda.Kredite : null))
    .ForMember(dest => dest.Kategoria, opt => opt.MapFrom(src => src.Lenda != null ? src.Lenda.Kategoria : null))
    .ForMember(dest => dest.Semestri1, opt => opt.MapFrom(src => src.Lenda != null && src.Lenda.Semestri != null ? src.Lenda.Semestri.Semestri1 : null));
            // .ForMember(dest => dest.DataProvimit, opt => opt.MapFrom(src => src.Orari.Data));
            CreateMap<ParaqitjaEprovimit, ProvimetEParaqituraDto>()
            .ForMember(dest => dest.Emri, opt => opt.MapFrom(src => src.Lenda != null ? src.Lenda.Emri : null))
            .ForMember(dest => dest.EmriStafit, opt => opt.MapFrom(src => src.StafiAkademik != null ? src.StafiAkademik.Useri.Emri : null))
            .ForMember(dest => dest.Mbiemri, opt => opt.MapFrom(src => src.StafiAkademik != null ? src.StafiAkademik.Useri.Mbiemri : null))
            .ForMember(dest => dest.Kategoria, opt => opt.MapFrom(src => src.Lenda != null ? src.Lenda.Kategoria : null));

            CreateMap<CreateProvimiDto, Provimi>();
            CreateMap<CreateGrupiDto, Grupi>();
            CreateMap<EditGrupiDto, Grupi>();
            CreateMap<Nota, GetNotaDto>();
            CreateMap<ParaqitjaEprovimit, GetParaqitjaEProvimitDto>();
            CreateMap<CreateEditParaqitjaEProvimitDto, ParaqitjaEprovimit>();
            CreateMap<CreateRegjistrimiSemestritDto, RegjistrimiSemestrit>();
            CreateMap<CreateSemestriDto, Semestri>();
            CreateMap<Semestri, SemestriDto>();

            CreateMap<Universiteti, GetUniversitetiDto>();
            CreateMap<Lokacionet, LokacionetDto>();

            CreateMap<CreateEditUniversitetiDto, Universiteti>();
            CreateMap<LokacionetDto, Lokacionet>();

        }
    }
}
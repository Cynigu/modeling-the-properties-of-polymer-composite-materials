using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers
{
    internal class MaterialProfile : Profile, IAdminEntityProfile
    {
        public MaterialProfile()
        {
            MapModelToEntity();
            MapEntityToModel();
            MapModelToModel();
        }

        public void MapModelToEntity()
        {
            CreateMap<MaterialModel, MaterialEntity>()
                .ForMember(
                    dest => dest.Id,
                    opt
                        => opt.MapFrom<int>(src => src.Id!.Value)
                )
                .ForMember(
                    dest => dest.EnglishName,
                    opt
                        => opt.MapFrom<string>(src => src.EnglishName ?? string.Empty)
                )
                .ForMember(
                    dest => dest.RussianName,
                    opt
                        => opt.MapFrom<string>(src => src.RussianName ?? string.Empty)
                )
                .ForMember(
                    dest => dest.QuickName,
                    opt
                        => opt.MapFrom<string>(src => src.QuickName ?? string.Empty)
                );
        }

        public void MapEntityToModel()
        {
            CreateMap<MaterialEntity, MaterialModel>()
                .ForMember(
                    dest => dest.Id,
                    opt
                        => opt.MapFrom<int>(src => src.Id)
                )
                .ForMember(
                    dest => dest.EnglishName,
                    opt
                        => opt.MapFrom<string>(src => src.EnglishName ?? string.Empty)
                )
                .ForMember(
                    dest => dest.RussianName,
                    opt
                        => opt.MapFrom<string>(src => src.RussianName ?? string.Empty)
                )
                .ForMember(
                    dest => dest.QuickName,
                    opt
                        => opt.MapFrom<string>(src => src.QuickName ?? string.Empty)
                );
        }

        public void MapModelToModel()
        {
            CreateMap<MaterialModel, MaterialModel>()
                .ForMember(
                    dest => dest.Id,
                    opt
                        => opt.MapFrom<int>(src => src.Id!.Value)
                )
                .ForMember(
                    dest => dest.EnglishName,
                    opt
                        => opt.MapFrom<string>(src => src.EnglishName ?? string.Empty)
                )
                .ForMember(
                    dest => dest.RussianName,
                    opt
                        => opt.MapFrom<string>(src => src.RussianName ?? string.Empty)
                )
                .ForMember(
                    dest => dest.QuickName,
                    opt
                        => opt.MapFrom<string>(src => src.QuickName ?? string.Empty)
                );
        }
    }
}

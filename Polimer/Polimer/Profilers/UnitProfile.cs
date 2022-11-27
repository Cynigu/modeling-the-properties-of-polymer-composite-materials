using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal class UnitProfile : Profile, IAdminEntityProfile
{
    public UnitProfile()
    {
        MapEntityToModel();
        MapModelToEntity();
        MapModelToModel();
    }

    public void MapModelToModel()
    {
        CreateMap<UnitModel, UnitModel>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id!.Value)
            )
            .ForMember(
                dest => dest.Name,
                opt
                    => opt.MapFrom<string>(src => src.Name ?? string.Empty)
            );
    }

    public void MapEntityToModel()
    {
        CreateMap<UnitEntity, UnitModel>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id)
            )
            .ForMember(
                dest => dest.Name,
                opt
                    => opt.MapFrom<string>(src => src.Name ?? string.Empty)
            );
    }

    public void MapModelToEntity()
    {
        CreateMap<UnitModel, UnitEntity>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id!.Value)
            )
            .ForMember(
                dest => dest.Name,
                opt
                    => opt.MapFrom<string>(src => src.Name ?? string.Empty)
            );
    }

}
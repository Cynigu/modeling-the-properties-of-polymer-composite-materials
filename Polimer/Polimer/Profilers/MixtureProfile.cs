using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal class MixtureProfile : Profile, IAdminEntityProfile
{
    public MixtureProfile()
    {
        MapEntityToModel();
        MapModelToEntity();
        MapModelToModel();
    }

    public void MapModelToModel()
    {
        CreateMap<MixtureModel, MixtureModel>()
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
        CreateMap<MixtureEntity, MixtureModel>()
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
        CreateMap<MixtureModel, MixtureEntity>()
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
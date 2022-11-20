using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal class AdditiveProfile : Profile, IAdminEntityProfile
{
    public AdditiveProfile()
    {
        MapEntityToModel();
        MapModelToEntity();
        MapModelToModel();
    }

    public void MapModelToModel()
    {
        CreateMap<AdditiveModel, AdditiveModel>()
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
        CreateMap<AdditiveEntity, AdditiveModel>()
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
        CreateMap<AdditiveModel, AdditiveEntity>()
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
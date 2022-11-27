using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal class PropertyProfile : Profile, IAdminEntityProfile
{
    public PropertyProfile()
    {
        MapEntityToModel();
        MapModelToEntity();
        MapModelToModel();
    }

    public void MapModelToModel()
    {
        CreateMap<PropertyModel, PropertyModel>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id!.Value)
            )
            .ForMember(
                dest => dest.Name,
                opt
                    => opt.MapFrom<string>(src => src.Name ?? string.Empty)
            )
            .ForMember(
                dest => dest.Unit,
                opt
                    => opt.MapFrom<UnitModel>(src => src.Unit)
            );
    }

    public void MapEntityToModel()
    {
        CreateMap<PropertyEntity, PropertyModel>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id)
            )
            .ForMember(
                dest => dest.Name,
                opt
                    => opt.MapFrom<string>(src => src.Name ?? string.Empty)
            )
            .ForMember(
                dest => dest.Unit,
                opt
                    => opt.MapFrom<UnitEntity>(src => src.Unit)
            );
    }

    public void MapModelToEntity()
    {
        CreateMap<PropertyModel, PropertyEntity>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id!.Value)
            )
            .ForMember(
                dest => dest.Name,
                opt
                    => opt.MapFrom<string>(src => src.Name ?? string.Empty)
            )
            .ForMember(
                dest => dest.Unit,
                opt
                    => opt.MapFrom<UnitModel>(src => src.Unit)
            );

    }
}
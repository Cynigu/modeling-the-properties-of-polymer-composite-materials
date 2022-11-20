using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal class CompatibilityMaterialProfile : Profile, IAdminEntityProfile
{
    public CompatibilityMaterialProfile()
    {
        MapEntityToModel();
        MapModelToEntity();
        MapModelToModel();
    }

    public void MapModelToModel()
    {
        CreateMap<CompatibilityMaterialModel, CompatibilityMaterialModel>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id!.Value)
            )
            .ForMember(
                dest => dest.FirstMaterial,
                opt
                    => opt.MapFrom<MaterialModel>(src => src.FirstMaterial)
            )
            .ForMember(
                dest => dest.SecondMaterial,
                opt
                    => opt.MapFrom<MaterialModel>(src => src.SecondMaterial)
            );
    }

    public void MapEntityToModel()
    {
        CreateMap<CompatibilityMaterialEntity, CompatibilityMaterialModel>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id)
            )
            .ForMember(
                dest => dest.FirstMaterial,
                opt
                    => opt.MapFrom<MaterialEntity>(src => src.FirstMaterial)
            )
            .ForMember(
                dest => dest.SecondMaterial,
                opt
                    => opt.MapFrom<MaterialEntity>(src => src.SecondMaterial)
            );
    }

    public void MapModelToEntity()
    {
        CreateMap<CompatibilityMaterialModel, CompatibilityMaterialEntity>()
            .ForMember(
                dest => dest.Id,
                opt
                    => opt.MapFrom<int>(src => src.Id!.Value)
            )
            .ForMember(
                dest => dest.FirstMaterial,
                opt
                    => opt.MapFrom<MaterialModel>(src => src.FirstMaterial)
            )
            .ForMember(
                dest => dest.SecondMaterial,
                opt
                    => opt.MapFrom<MaterialModel>(src => src.SecondMaterial)
            )
            .ForMember(
                dest => dest.IdFirstMaterial,
                opt
                    => opt.MapFrom<int>(src => src.FirstMaterial.Id!.Value)
            )
            .ForMember(
                dest => dest.IdSecondMaterial,
                opt
                    => opt.MapFrom<int>(src => src.SecondMaterial.Id!.Value)
            );

    }
}
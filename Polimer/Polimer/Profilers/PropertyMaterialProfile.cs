using AutoMapper;
using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal class PropertyMaterialProfile : ProfileAbstract<PropertyMaterialEntity, PropertyMaterialModel>
{
    //public PropertyMaterialProfile()
    //{
    //    MapEntityToModel();
    //    MapModelToEntity();
    //    MapModelToModel();
    //}

    //public void MapModelToModel()
    //{
    //    CreateMap<PropertyMaterialModel, PropertyMaterialModel>()
    //        .ForMember(
    //            dest => dest.Id,
    //            opt
    //                => opt.MapFrom<int>(src => src.Id!.Value)
    //        )
    //        .ForMember(
    //            dest => dest.Property,
    //            opt
    //                => opt.MapFrom<PropertyModel>(src => src.Property)
    //        )
    //        .ForMember(
    //            dest => dest.Material,
    //            opt
    //                => opt.MapFrom<MaterialModel>(src => src.Material)
    //        );
    //}

    //public void MapEntityToModel()
    //{
    //    CreateMap<PropertyMaterialEntity, PropertyMaterialModel>()
    //        .ForMember(
    //            dest => dest.Id,
    //            opt
    //                => opt.MapFrom(src => src.Id)
    //        )
    //        .ForMember(
    //            dest => dest.Property,
    //            opt
    //                => opt.MapFrom(src => src.Property)
    //        )
    //        .ForMember(
    //            dest => dest.Material,
    //            opt
    //                => opt.MapFrom(src => src.Material)
    //        );
    //}

    //public void MapModelToEntity()
    //{
    //    CreateMap<PropertyMaterialModel, PropertyMaterialModel>()
    //        .ForMember(
    //            dest => dest.Id,
    //            opt
    //                => opt.MapFrom<int>(src => src.Id!.Value)
    //        )
    //        .ForMember(
    //            dest => dest.Property,
    //            opt
    //                => opt.MapFrom<PropertyModel>(src => src.Property)
    //        )
    //        .ForMember(
    //            dest => dest.Material,
    //            opt
    //                => opt.MapFrom<MaterialModel>(src => src.Material)
    //        );
    //}

}
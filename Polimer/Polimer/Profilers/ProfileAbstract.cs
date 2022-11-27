using AutoMapper;
using Polimer.App.ViewModel;
using Polimer.Data.Models;

namespace Polimer.App.Profilers;

internal abstract class ProfileAbstract<TEntity, TModelAsEntity> : Profile, IAdminEntityProfile
    where TEntity : class, IEntity
    where TModelAsEntity : class, IModelAsEntity
{
    public ProfileAbstract()
    {
        MapEntityToModel();
        MapModelToEntity();
        MapModelToModel();
    }

    public virtual void  MapEntityToModel()
    {
        CreateMap<TEntity, TModelAsEntity>();
    }

    public virtual void MapModelToEntity()
    {
        CreateMap<TModelAsEntity, TEntity>();
    }

    public virtual void MapModelToModel()
    {
        CreateMap<TModelAsEntity, TModelAsEntity>();
    }
}
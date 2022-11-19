using Autofac;
using AutoMapper;
using Polimer.App.Profilers;

namespace Polimer.App.Autofac;

internal class AutoMapperModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfiler());
            cfg.AddProfile(new MaterialProfile());
        }));
        builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
    }
}
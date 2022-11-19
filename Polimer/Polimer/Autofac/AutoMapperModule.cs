using Autofac;
using AutoMapper;
using Polimer.App.Profilers;
using Polimer.App.ViewModel;
using Polimer.App.ViewModel.Admin;
using Polimer.App.ViewModel.Admin.Factory;
using Polimer.App.ViewModel.Authorization;
using Polimer.App.ViewModel.Authorization.Factory;

namespace Polimer.App.Autofac;

internal class AutoMapperModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(ctx => new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new UserProfiler());
        }));
        builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
    }
}
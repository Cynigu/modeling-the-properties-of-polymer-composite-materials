using Autofac;
using Polimer.ViewModel;

namespace Polimer.Autofac;

internal class ViewModelModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AuthorizationViewModel>()
            .AsSelf();

        builder
            .RegisterType<AdminViewModel>()
            .AsSelf();

        builder
            .RegisterType<TechnologistViewModel>()
            .AsSelf();

    }
}
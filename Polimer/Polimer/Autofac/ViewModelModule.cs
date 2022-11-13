using Autofac;
using Polimer.App.ViewModel.Admin;
using Polimer.App.ViewModel.Authorization;

namespace Polimer.App.Autofac;

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
    }
}
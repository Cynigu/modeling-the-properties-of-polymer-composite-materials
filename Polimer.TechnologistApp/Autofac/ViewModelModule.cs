using Autofac;
using Polimer.TechnologistApp.ViewModel.Admin;
using Polimer.TechnologistApp.ViewModel.Authorization;

namespace Polimer.TechnologistApp.Autofac;

internal class ViewModelModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AuthorizationViewModel>()
            .AsSelf();

        builder
            .RegisterType<TechnologistViewModel>()
            .AsSelf();
    }
}
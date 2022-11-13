using Autofac;
using Polimer.TechnologistApp.View;
using Polimer.TechnologistApp.ViewModel.Admin;
using Polimer.TechnologistApp.ViewModel.Authorization;

namespace Polimer.TechnologistApp.Autofac;

internal class WindowModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(c => new AuthorizationWindow() { DataContext = c.Resolve<AuthorizationViewModel>() })
            .AsSelf();

        builder
            .Register(c => new TechnologistWindow() { DataContext = c.Resolve<TechnologistViewModel>() })
            .AsSelf();
    }

}
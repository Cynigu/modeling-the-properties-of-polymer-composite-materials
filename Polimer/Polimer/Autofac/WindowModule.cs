using Autofac;
using Polimer.View;
using Polimer.ViewModel;

namespace Polimer.Autofac;

internal class WindowModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(c => new AuthorizationWindow() {DataContext = c.Resolve<AuthorizationViewModel>()})
            .AsSelf();

        builder
            .Register(c => new AdminWindow() { DataContext = c.Resolve<AdminViewModel>() })
            .AsSelf();

        builder
            .Register(c => new TechnologistWindow() { DataContext = c.Resolve<TechnologistViewModel>() })
            .AsSelf();
    }

}
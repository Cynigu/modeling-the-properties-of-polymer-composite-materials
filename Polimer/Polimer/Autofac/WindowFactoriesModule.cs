using Autofac;
using Polimer.App.View;
using Polimer.App.View.Factories;

namespace Polimer.App.Autofac;

internal class WindowFactoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AuthorizationWindowFactory>().As<IWindowFactory<AuthorizationWindow>>();
        builder.RegisterType<AdminWindowFactory>().As<IWindowFactory<AdminWindow>>();
    }
}
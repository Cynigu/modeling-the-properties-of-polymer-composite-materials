using Autofac;
using Polimer.App.ViewModel;
using Polimer.App.ViewModel.Admin;
using Polimer.App.ViewModel.Admin.Factory;
using Polimer.App.ViewModel.Authorization;
using Polimer.App.ViewModel.Authorization.Factory;

namespace Polimer.App.Autofac;

internal class ViewModelFactoriesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<AuthorizationViewModelFactory>()
            .As<IViewModelFactory<AuthorizationViewModel>>();

        builder
            .RegisterType<AdminViewModelFactory>()
            .As<IViewModelFactory<AdminViewModel>>();
    }
}
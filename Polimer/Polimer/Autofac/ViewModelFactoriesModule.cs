using Autofac;
using Polimer.App.Services;
using Polimer.App.ViewModel;
using Polimer.App.ViewModel.Admin;
using Polimer.App.ViewModel.Admin.Factory;
using Polimer.App.ViewModel.Authorization;
using Polimer.App.ViewModel.Authorization.Factory;
using Polimer.App.ViewModel.Technology;
using Polimer.App.ViewModel.Technology.Factory;

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

        builder
            .RegisterType<TcnologyViewModelFactory>()
            .As<IViewModelFactory<TechnologyViewModel>>();
    }
}


internal class ServicesModule : Module
{

    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<ReportService>()
            .As<IFileService>();

        builder
            .RegisterType<DefaultDialogService>()
            .As<IDialogService>();

    }
}
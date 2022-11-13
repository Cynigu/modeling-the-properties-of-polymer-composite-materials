﻿using Autofac;
using PolimerAdministratorApp.ViewModel.Admin;
using PolimerAdministratorApp.ViewModel.Authorization;

namespace PolimerAdministratorApp.Autofac;

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
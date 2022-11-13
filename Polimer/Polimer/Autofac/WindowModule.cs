﻿using Autofac;
using PolimerAdministratorApp.View;
using PolimerAdministratorApp.ViewModel.Admin;
using PolimerAdministratorApp.ViewModel.Authorization;

namespace PolimerAdministratorApp.Autofac;

internal class WindowModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .Register(c => new AuthorizationWindow() { DataContext = c.Resolve<AuthorizationViewModel>() })
            .AsSelf();

        builder
            .Register(c => new AdminWindow() { DataContext = c.Resolve<AdminViewModel>() })
            .AsSelf();
    }

}
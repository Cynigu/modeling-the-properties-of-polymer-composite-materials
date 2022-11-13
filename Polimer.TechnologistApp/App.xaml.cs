using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Autofac;
using Polimer.Data.Autofac;
using Polimer.TechnologistApp.Autofac;
using Polimer.TechnologistApp.View;

namespace Polimer.TechnologistApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builderBase = new ContainerBuilder();

            builderBase.RegisterModule(new DataModule());
            builderBase.RegisterModule(new ViewModelModule());
            builderBase.RegisterModule(new WindowModule());

            var containerBase = builderBase.Build();

            var viewBase = containerBase.Resolve<AuthorizationWindow>();

            viewBase.Show();
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Ошибка\n" + e.Exception.Message);

            e.Handled = true;
        }
    }
}

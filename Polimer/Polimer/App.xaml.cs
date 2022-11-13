using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Polimer.Autofac;
using Polimer.View;
using Polimer.ViewModel;

namespace Polimer
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

            var containerBase = builderBase.Build();

            var viewmodelBase = new AuthorizationViewModel();

            var viewBase = new AuthorizationWindow() { DataContext = viewmodelBase };

            viewBase.Show();
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Ошибка\n" + e.Exception.StackTrace + " " + "Исключение: "
                            + e.Exception.GetType().ToString() + " " + e.Exception.Message);

            e.Handled = true;
        }
    }
}

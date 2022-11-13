using System.Windows;
using System.Windows.Threading;
using Autofac;
using Polimer.App.Autofac;
using Polimer.App.View;
using Polimer.App.View.Factories;
using Polimer.Data.Autofac;

namespace Polimer.App
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

            var viewBase = containerBase.Resolve<IWindowFactory<AuthorizationWindow>>().CreateWindow();

            viewBase.Show();
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Ошибка\n" + e.Exception.Message);

            e.Handled = true;
        }
    }
}

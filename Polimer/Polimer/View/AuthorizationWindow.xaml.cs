using System.Windows;

namespace Polimer.App.View
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
            // This line should fix it:
            this.Closed += (sender, e) => this.Dispatcher.InvokeShutdown();
        }
    }
}

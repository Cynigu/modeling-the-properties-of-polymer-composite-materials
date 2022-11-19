using System.Windows;

namespace Polimer.App.View
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private AuthorizationWindow()
        {
            InitializeComponent();
        }

        public static AuthorizationWindow CreateInstance()
        {
            return new AuthorizationWindow();
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using Polimer.App.ViewModel.Authorization;

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

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null && DataContext is AuthorizationViewModel)
            {
                var viewModel = (AuthorizationViewModel)DataContext;
                viewModel.UserModel.Password = ((PasswordBox)sender).Password;
            }
            //{ ((AuthorizationViewModel)this.DataContext).UserModel.Password = ((PasswordBox)sender).Password; }
        }
    }
}

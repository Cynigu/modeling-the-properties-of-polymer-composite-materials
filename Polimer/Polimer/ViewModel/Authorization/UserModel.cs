namespace PolimerAdministratorApp.ViewModel.Authorization
{
    public class UserModel : ViewModelBase
    {
        private string? _login;
        private string? _password;

        public string? Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }
        public string? Login
        {
            get => _login;
            set => SetField(ref _login, value);
        }
    }
}

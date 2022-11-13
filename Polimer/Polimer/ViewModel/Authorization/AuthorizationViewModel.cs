using System;
using System.Threading.Tasks;
using Polimer.Data.Repository;
using PolimerAdministratorApp.View;

namespace PolimerAdministratorApp.ViewModel.Authorization
{
    public class AuthorizationViewModel : ViewModelBase
    {
        private readonly AdminWindow _adminWindow;
        private readonly Repository _repository;

        public AuthorizationViewModel(AdminWindow adminWindow, Repository repository)
        {
            _adminWindow = adminWindow ?? throw new ArgumentNullException(nameof(adminWindow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            UserModel = new UserModel();

            LogInCommand = new AsyncCommand(LogInMethodAsync, CanLogIn);
        }

        #region Fields

        private UserModel _userModel;

        #endregion

        #region Properties

        public UserModel UserModel
        {
            get => _userModel;
            set
            {
                if (Equals(value, _userModel)) return;
                _userModel = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands
        public AsyncCommand LogInCommand { get; set; }

        public bool CanLogIn() =>
            !string.IsNullOrWhiteSpace(UserModel.Login) && !string.IsNullOrWhiteSpace(UserModel.Password); 

        #endregion

        #region Methods

        private async Task LogInMethodAsync()
        {
            var login = UserModel.Login;
            var password = UserModel.Password;

            if(login == null || password == null)
                throw new ArgumentNullException("Не введен логин или пароль!");
            
            var user = await _repository.UserByLoginAndPasswordAsync(login, password);

            if (user.Success == false)
            {
                throw new Exception("Пользователя не существует");
            }

            if (user.Role == "Администратор")
            {
                _adminWindow.Show();
            }
            else
            {
                throw new InvalidOperationException("Пользователя-админимтратора не существует!");
            }
        }
        

        #endregion
    }
}

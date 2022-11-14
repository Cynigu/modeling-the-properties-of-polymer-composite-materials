using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Polimer.App.View;
using Polimer.App.View.Factories;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Authorization
{
    public class AuthorizationViewModel : ViewModelBase
    {
        private readonly IWindowFactory<AdminWindow> _adminWindowFactory;
        private readonly Repository _repository;

        public AuthorizationViewModel(IWindowFactory<AdminWindow> adminWindow, Repository repository)
        {
            _adminWindowFactory = adminWindow ?? throw new ArgumentNullException(nameof(adminWindow));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            LogInCommand = new AsyncCommand(LogInMethodAsync, CanLogIn);
        }

        #region Fields

        private UserModel _userModel = new();

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
        public ICommand LogInCommand { get; set; }

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
                throw new Exception("Пользователя не существует!");
            }

            if (user.Role == "Администратор")
            {
                 var window = _adminWindowFactory.CreateWindow();
                 window.Show();
            }
            else if (user.Role == "Технолог")
            {
                throw new InvalidOperationException("Технолога пока нет!");
            }
            else
            {
                throw new InvalidOperationException("Пользователя не существует!");
            }
        }
        

        #endregion
    }
}

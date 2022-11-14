using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Polimer.App.ViewModel.Admin
{
    public class UsersViewModel: ViewModelBase
    {
        private readonly Repository _repository;
        
        public UsersViewModel(Repository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));

            Roles = new ObservableCollection<string>() {"Администратор", "Технолог"};

            AddUserCommand = new AsyncCommand(AddUserAsync, CanAddUser);
        }

        #region Fields

        private UserModel? _selectedUser;
        private string? _login;
        private string? _password;
        private string? _selectedRole;
        private ObservableCollection<UserModel> _users;
        private ObservableCollection<string> _roles;

        #endregion

        #region Properties

        public ObservableCollection<UserModel> Users
        {
            get => _users;
            set => SetField(ref _users, value);
        }

        public ObservableCollection<string> Roles
        {
            get => _roles;
            set => SetField(ref _roles, value);
        }

        public UserModel? SelectedUser
        {
            get => _selectedUser;
            set
            {
                SetField(ref _selectedUser, value);
                Login = SelectedUser?.Login;
                Password = SelectedUser?.Password;
                SelectedRole = SelectedUser?.Role;
            }
        }

        public string? Login
        {
            get => _login;
            set => SetField(ref _login, value);
        }

        public string? Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }


        public string? SelectedRole
        {
            get => _selectedRole;
            set => SetField(ref _selectedRole, value);
        }

        #endregion

        #region Commands

        public ICommand AddUserCommand { get; set; }

        private bool CanAddUser() => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password) &&
                                     !string.IsNullOrWhiteSpace(SelectedRole);

        #endregion


        #region Methods

        private async Task AddUserAsync()
        {
            if (string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(SelectedRole))
            {
                throw new ArgumentNullException("Поля должны быть заполнены!");
            }
            await _repository.AddUserAsync(Login, Password, SelectedRole);
            await UpdateEntitiesAsync();
        }

        public async Task UpdateEntitiesAsync()
        {
            var users = (await _repository.GetUsersAsync()).Select(x => new UserModel()
            {
                Id = x.Id,
                Login = x.Login,
                Password = x.Password,
                Role = x.Role
            });

            Users = new ObservableCollection<UserModel>(users);
        }

        #endregion
    }
}

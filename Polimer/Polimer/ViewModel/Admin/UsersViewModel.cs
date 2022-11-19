using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Repository;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.Data.Models;
using Polimer.App.ViewModel.Admin.Abstract;

namespace Polimer.App.ViewModel.Admin
{
    public sealed class UsersViewModel: TabAdminBaseViewModel<UserEntity, UserModel>
    {
        private UsersViewModel(UserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            NameTab = "Пользователи";
            Roles = new ObservableCollection<string>() {"Администратор", "Технолог"};
            ChangingModel = new UserModel();
        }

        public static UsersViewModel CreateInstance(UserRepository userRepository, IMapper mapper)
        {
            return new UsersViewModel(userRepository, mapper);
        }

        private ObservableCollection<string> _roles;

        public ObservableCollection<string> Roles
        {
            get => _roles;
            set => SetField(ref _roles, value);
        }


        protected override async Task<bool> CheckingForExistenceAsync()
        {
            var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => u.Login == ChangingModel.Login);
            return user == null;
        }

        protected override bool CanAdd() => !string.IsNullOrWhiteSpace(ChangingModel?.Login) 
                                            && !string.IsNullOrWhiteSpace(ChangingModel?.Password) 
                                            && !string.IsNullOrWhiteSpace(ChangingModel?.Role);


    }
}

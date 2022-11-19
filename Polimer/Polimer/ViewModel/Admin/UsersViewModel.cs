using Polimer.App.ViewModel.Admin.Models;
using Polimer.Data.Repository;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AutoMapper;
using Polimer.Data.Models;

namespace Polimer.App.ViewModel.Admin
{
    public sealed class UsersViewModel: TabAdminBaseViewModel<UserEntity, UserModel>
    {
        private UsersViewModel(UserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            Roles = new ObservableCollection<string>() {"Администратор", "Технолог"};
            ChangingModel = new UserModel();
        }

        public static UsersViewModel CreateInstance(UserRepository userRepository, IMapper mapper)
        {
            return new UsersViewModel(userRepository, mapper);
        }

        #region Fields
        private ObservableCollection<string> _roles;

        #endregion

        #region Properties

        public ObservableCollection<string> Roles
        {
            get => _roles;
            set => SetField(ref _roles, value);
        }

        #endregion


        #region Override Methods

        protected override async Task<bool> CheckingForExistenceAsync()
        {
            if (SelectedModel == null)
                throw new ArgumentException("Нет данных!");
            var user = await _repository.GetEntityByFilterFirstOrDefaultAsync(u => u.Login == SelectedModel.Login);
            return user == null;
        }

        protected override bool CanAdd() => !string.IsNullOrWhiteSpace(ChangingModel?.Login) 
                                            && !string.IsNullOrWhiteSpace(ChangingModel?.Password) 
                                            && !string.IsNullOrWhiteSpace(ChangingModel?.Role);

        #endregion

    }

}

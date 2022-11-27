using Polimer.App.View;
using Polimer.App.View.Factories;
using Polimer.Data.Repository;
using System;
using System.Windows;
using Polimer.Data.Repository.Factory;

namespace Polimer.App.ViewModel.Authorization.Factory
{

    public class AuthorizationViewModelFactory : IViewModelFactory<AuthorizationViewModel>
    {
        private readonly UserRepository _userRepository;
        private readonly IWindowFactory<AdminWindow> _adminWindowFactory;
        private readonly IWindowFactory<TechnolgyWindow> _techWindowFactory;

        public AuthorizationViewModelFactory(RepositoriesFactory repositoryFactory, IWindowFactory<AdminWindow> windowFactory, IWindowFactory<TechnolgyWindow> techWindowFactory)
        {
            _userRepository = (repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory))).CreateUserRepository();
            _adminWindowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));
            _techWindowFactory = techWindowFactory ?? throw new ArgumentNullException(nameof(techWindowFactory));
        }

        public AuthorizationViewModel CreateViewModel(Window currentWindow)
        {
            return AuthorizationViewModel.CreateInstance(_adminWindowFactory, _userRepository, _techWindowFactory, currentWindow);
        }

        public AuthorizationViewModel CreateViewModel()
        {
            return AuthorizationViewModel.CreateInstance(_adminWindowFactory, _userRepository, _techWindowFactory);
        }
    }
}

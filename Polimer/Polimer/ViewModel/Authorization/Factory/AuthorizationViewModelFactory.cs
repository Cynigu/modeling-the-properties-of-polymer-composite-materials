using Polimer.App.View;
using Polimer.App.View.Factories;
using Polimer.Data.Repository;
using System;
using System.Windows;

namespace Polimer.App.ViewModel.Authorization.Factory
{

    public class AuthorizationViewModelFactory : IViewModelFactory<AuthorizationViewModel>
    {
        private readonly UserRepository _userRepository;
        private readonly IWindowFactory<AdminWindow> _windowFactory;

        public AuthorizationViewModelFactory(UserRepository userRepository, IWindowFactory<AdminWindow> windowFactory)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _windowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));
        }

        public AuthorizationViewModel CreateViewModel(Window currentWindow)
        {
            return AuthorizationViewModel.CreateInstance(_windowFactory, _userRepository, currentWindow);
        }

        public AuthorizationViewModel CreateViewModel()
        {
            return AuthorizationViewModel.CreateInstance(_windowFactory, _userRepository);
        }
    }
}

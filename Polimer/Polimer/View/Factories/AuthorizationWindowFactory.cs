using System;
using Polimer.App.ViewModel;
using Polimer.App.ViewModel.Authorization;

namespace Polimer.App.View.Factories
{
    public class AuthorizationWindowFactory : IWindowFactory<AuthorizationWindow>
    {
        private readonly IViewModelFactory<AuthorizationViewModel> _authViewModel;
        public AuthorizationWindowFactory(IViewModelFactory<AuthorizationViewModel> authViewModel)
        {
            _authViewModel = authViewModel ?? throw new ArgumentNullException(nameof(authViewModel));
        }
        public AuthorizationWindow CreateWindow()
        {
            var window = AuthorizationWindow.CreateInstance();
            window.DataContext = _authViewModel.CreateViewModel();
            return window;
        }
    }
}

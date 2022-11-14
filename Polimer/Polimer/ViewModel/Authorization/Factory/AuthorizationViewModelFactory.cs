using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polimer.App.View;
using Polimer.App.View.Factories;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Authorization.Factory
{
    public class AuthorizationViewModelFactory : IViewModelFactory<AuthorizationViewModel>
    {
        private readonly Repository _repository;
        private readonly IWindowFactory<AdminWindow> _windowFactory;

        public AuthorizationViewModelFactory(Repository repository, IWindowFactory<AdminWindow> windowFactory)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _windowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));
        }

        public AuthorizationViewModel CreateViewModel()
        {
            return new AuthorizationViewModel(_windowFactory, _repository);
        }
    }
}

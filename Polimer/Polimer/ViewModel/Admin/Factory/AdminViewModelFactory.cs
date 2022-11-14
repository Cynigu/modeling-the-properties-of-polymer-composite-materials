using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Polimer.Data.Repository;

namespace Polimer.App.ViewModel.Admin.Factory
{
    internal class AdminViewModelFactory : IViewModelFactory<AdminViewModel>
    {
        private readonly Repository _repository;

        public AdminViewModelFactory(Repository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public AdminViewModel CreateViewModel()
        {
            return new AdminViewModel(_repository);
        }
    }
}

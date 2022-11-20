using AutoMapper;
using Polimer.Data.Repository.Factory;
using System;
using System.Windows;

namespace Polimer.App.ViewModel.Admin.Factory
{
    internal class AdminViewModelFactory : IViewModelFactory<AdminViewModel>
    {
        private readonly RepositoriesFactory _repositoriesFactory;
        private readonly IMapper _mapper;

        public AdminViewModelFactory( 
            IMapper mapper, 
            RepositoriesFactory repositoriesFactory
            )
        {
            _repositoriesFactory = repositoriesFactory ?? throw new ArgumentNullException(nameof(repositoriesFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public AdminViewModel CreateViewModel()
        {
            return AdminViewModel.CreateInstance( _mapper, _repositoriesFactory);
        }

        public AdminViewModel CreateViewModel(Window currentWindow)
        {
            return AdminViewModel.CreateInstance(_mapper, _repositoriesFactory);
        }
    }
}

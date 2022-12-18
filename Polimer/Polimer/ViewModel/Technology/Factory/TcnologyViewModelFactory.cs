using System;
using AutoMapper;
using Polimer.Data.Repository.Factory;

namespace Polimer.App.ViewModel.Technology.Factory
{
    internal class TcnologyViewModelFactory : IViewModelFactory<TechnologyViewModel>
    {
        private readonly RepositoriesFactory _repositoriesFactory;
        private readonly IMapper _mapper;

        public TcnologyViewModelFactory(
            IMapper mapper,
            RepositoriesFactory repositoriesFactory
        )
        {
            _repositoriesFactory = repositoriesFactory ?? throw new ArgumentNullException(nameof(repositoriesFactory));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TechnologyViewModel CreateViewModel()
        {
            return new TechnologyViewModel(_mapper, _repositoriesFactory);
        }

        public TechnologyViewModel CreateViewModel(System.Windows.Window currentWindow)
        {
            return new TechnologyViewModel(_mapper, _repositoriesFactory);
        }
    }
}

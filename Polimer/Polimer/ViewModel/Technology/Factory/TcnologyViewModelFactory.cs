using System;
using AutoMapper;
using Polimer.App.Services;
using Polimer.Data.Repository.Factory;

namespace Polimer.App.ViewModel.Technology.Factory
{
    internal class TcnologyViewModelFactory : IViewModelFactory<TechnologyViewModel>
    {
        private readonly IDialogService _dialogService;
        private readonly IFileService _fileService;
        private readonly RepositoriesFactory _repositoriesFactory;
        private readonly IMapper _mapper;

        public TcnologyViewModelFactory(
            IMapper mapper,
            RepositoriesFactory repositoriesFactory, IDialogService dialogService, IFileService fileService)
        {
            _repositoriesFactory = repositoriesFactory ?? throw new ArgumentNullException(nameof(repositoriesFactory));
            _dialogService = dialogService;
            _fileService = fileService;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public TechnologyViewModel CreateViewModel()
        {
            return new TechnologyViewModel(_mapper, _repositoriesFactory, _fileService, _dialogService);
        }

        public TechnologyViewModel CreateViewModel(System.Windows.Window currentWindow)
        {
            return new TechnologyViewModel(_mapper, _repositoriesFactory, _fileService, _dialogService);
        }
    }
}

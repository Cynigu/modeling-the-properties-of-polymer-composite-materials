using AutoMapper;
using Polimer.Data.Repository;
using System;
using System.Windows;

namespace Polimer.App.ViewModel.Admin.Factory
{
    internal class AdminViewModelFactory : IViewModelFactory<AdminViewModel>
    {
        private readonly UserRepository _userRepository;
        private readonly MaterialRepository _materialRepository;
        private readonly IMapper _mapper;

        public AdminViewModelFactory( IMapper mapper, UserRepository userRepository, MaterialRepository materialRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public AdminViewModel CreateViewModel()
        {
            return AdminViewModel.CreateInstance( _mapper, _userRepository, _materialRepository);
        }

        public AdminViewModel CreateViewModel(Window currentWindow)
        {
            return AdminViewModel.CreateInstance(_mapper, _userRepository, _materialRepository);
        }
    }
}

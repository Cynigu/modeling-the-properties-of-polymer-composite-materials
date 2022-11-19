using AutoMapper;
using Polimer.Data.Models;
using Polimer.Data.Repository;
using System;

namespace Polimer.App.ViewModel.Admin.Factory
{
    internal class AdminViewModelFactory : IViewModelFactory<AdminViewModel>
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public AdminViewModelFactory( IMapper mapper, UserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public AdminViewModel CreateViewModel()
        {
            return AdminViewModel.CreateInstance(_userRepository, _mapper);
        }
    }
}

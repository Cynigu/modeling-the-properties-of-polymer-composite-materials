﻿using System;
using Polimer.App.ViewModel;
using Polimer.App.ViewModel.Admin;

namespace Polimer.App.View.Factories
{
    public class AdminWindowFactory : IWindowFactory<AdminWindow>
    {
        private readonly IViewModelFactory<AdminViewModel> _adminViewModel;
        public AdminWindowFactory(IViewModelFactory<AdminViewModel> adminViewModel)
        {
            _adminViewModel = adminViewModel ?? throw new ArgumentNullException(nameof(adminViewModel));
        }
        public AdminWindow CreateWindow()
        {
            return new AdminWindow() {DataContext = _adminViewModel.CreateViewModel()};
        }
    }
}

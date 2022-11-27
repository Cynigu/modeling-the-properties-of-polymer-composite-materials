using System;
using Polimer.App.ViewModel;
using Polimer.App.ViewModel.Technology;

namespace Polimer.App.View.Factories;

public class TechnologyWindowFactory : IWindowFactory<TechnolgyWindow>
{
    private readonly IViewModelFactory<TechnologyViewModel> _technologyViewModel;
    public TechnologyWindowFactory(IViewModelFactory<TechnologyViewModel> adminViewModel)
    {
        _technologyViewModel = adminViewModel ?? throw new ArgumentNullException(nameof(adminViewModel));
    }
        
    public TechnolgyWindow CreateWindow()
    {
        var window = TechnolgyWindow.CreateInstance();
        window.DataContext = _technologyViewModel.CreateViewModel();
        return window;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Polimer.App.ViewModel
{
    public interface IViewModelFactory<TViewModel> where TViewModel : ViewModelBase
    {
        TViewModel CreateViewModel();

        TViewModel CreateViewModel(Window currentWindow);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polimer.App.ViewModel
{
    public interface IViewModelFactory<TViewModel> where TViewModel : ViewModelBase
    {
        TViewModel CreateViewModel();
    }
}

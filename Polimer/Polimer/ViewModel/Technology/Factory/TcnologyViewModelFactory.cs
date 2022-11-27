namespace Polimer.App.ViewModel.Technology.Factory
{
    internal class TcnologyViewModelFactory : IViewModelFactory<TechnologyViewModel>
    {
        public TechnologyViewModel CreateViewModel()
        {
            return new TechnologyViewModel();
        }

        public TechnologyViewModel CreateViewModel(System.Windows.Window currentWindow)
        {
            return new TechnologyViewModel();
        }
    }
}

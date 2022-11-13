using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Polimer.TechnologistApp.ViewModel
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
    public abstract class AsyncCommandBase : IAsyncCommand
    {
        public abstract bool CanExecute(object parameter);
        public abstract Task ExecuteAsync(object parameter);
        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

    public class AsyncCommand : AsyncCommandBase
    {
        private Func<Task> _execute;
        private Func<bool> _canExecute;
        public AsyncCommand(Func<Task> execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public override Task ExecuteAsync(object parameter)
        {
            return _execute();
        }
    }
}

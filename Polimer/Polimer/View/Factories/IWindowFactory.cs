using System.Windows;

namespace Polimer.App.View.Factories;

public interface IWindowFactory<T>  where T : Window
{
    T CreateWindow();
}

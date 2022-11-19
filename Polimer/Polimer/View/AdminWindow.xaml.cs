using System.Windows;

namespace Polimer.App.View
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private AdminWindow()
        {
            InitializeComponent();
        }

        public static AdminWindow CreateInstance()
        {
            return new AdminWindow();
        }
    }

}

using System.ComponentModel;
using System.Windows;

namespace Polimer.App.View
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        public bool? OpenView()
        {
            return this.ShowDialog();
        }

        public void CloseView()
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            e.Cancel = true;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Polimer.App.View
{
    /// <summary>
    /// Логика взаимодействия для TechnolgyWindow.xaml
    /// </summary>
    public partial class TechnolgyWindow : Window
    {
        private TechnolgyWindow()
        {
            InitializeComponent();
        }

        internal static TechnolgyWindow CreateInstance()
        {
            return new TechnolgyWindow();
        }
    }
}

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

namespace SWEN2_TourPlanner.Views
{
    /// <summary>
    /// Interaction logic for ModifyTourLogWindow.xaml
    /// </summary>
    public partial class ModifyTourLogWindow : Window
    {
        public ModifyTourLogWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

using SWEN2_TourPlanner_ViewModels;
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
    /// Interaction logic for ModifyTourWindow.xaml
    /// </summary>
    public partial class ModifyTourWindow : Window
    {

       

        public ModifyTourWindow()
        {
            InitializeComponent();

           
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

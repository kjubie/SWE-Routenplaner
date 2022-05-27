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
    /// Interaction logic for CreateTourLog.xaml
    /// </summary>
    public partial class CreateTourLogWindow : Window
    {
        CreateTourLogViewModel ViewModel;
        public CreateTourLogWindow(CreateTourLogViewModel viewModel)
        {

            InitializeComponent();
            ViewModel = viewModel;
            DataContext = ViewModel;


            if (ViewModel.CloseAction == null)
            {
                ViewModel.CloseAction = new Action(this.Close);
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

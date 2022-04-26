using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using SWEN2_Tourplanner_ViewModels;
using SWEN2_TourPlanner_ViewModels;

namespace SWEN2_TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {

        CreateTourViewModel ViewModel = new CreateTourViewModel();

        private static readonly HttpClient client = new HttpClient();
        public CreateTour()
        {
            InitializeComponent();
            this.DataContext = ViewModel;

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

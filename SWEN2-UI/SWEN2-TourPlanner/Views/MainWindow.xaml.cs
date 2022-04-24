using HandlebarsDotNet.Collections;
using SWEN2_TourPlanner.View;
using SWEN2_Tourplanner_Models;
using SWEN2_Tourplanner_ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWEN2_TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        ToursViewModel ViewModel = new ToursViewModel();


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
            _ = ViewModel.LoadToursAsync();
            this.DataContext = ViewModel;
        }


        private void AddTour(object sender, RoutedEventArgs e)
        {
            CreateTour createTourWindow = new CreateTour();
            createTourWindow.Show();
        }



        private void UpdateTourDetails(object sender, SelectionChangedEventArgs e)
        {
            ToursViewModel.SelectedTour = (TourModel)TourListCollection.SelectedItem;

        }

        private void DeleteTour(object sender, RoutedEventArgs e)
        {
            var nameTourToDelete = ((Button)sender).Tag;
            ViewModel.DeleteTour(nameTourToDelete.ToString());
        }




    }
}

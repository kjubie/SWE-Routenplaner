using SWEN2_REST.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
using SWEN2_Tourplanner_ViewModel;


namespace SWEN2_TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für TourList.xaml
    /// </summary>
    /// 





    public partial class TourList : UserControl
    {
        //ObservableCollection<string> TourNames = new ObservableCollection<string>();

        // Tours? tourlist = new Tours();

        ToursViewModel ViewModel = new ToursViewModel();


        public TourList()
        {

            InitializeComponent();
            this.DataContext = ViewModel;
            //TourListCollection.ItemsSource = TourNames;             
           


        }

        

        private void AddTour(object sender, RoutedEventArgs e)
        {

            /*
            ViewModel.AddTour(new TourModel { From = "Hamburg", To = "Salzburg", Name = "TestTourAdd" });
            */


            CreateTour createTourWindow = new CreateTour();
            createTourWindow.Show();    
            

        }
    }
}

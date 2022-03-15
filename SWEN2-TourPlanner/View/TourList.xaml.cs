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

namespace SWEN2_TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für TourList.xaml
    /// </summary>
    /// 

  



    public partial class TourList : UserControl
    {
        ObservableCollection<string> TourNames = new ObservableCollection<string>();

        public TourList()
        {
            InitializeComponent();
            TourListCollection.ItemsSource = TourNames;
        }



        private void AddTour(object sender, RoutedEventArgs e)
        {
            TourNames.Add("Tour " + TourNames.Count);

        }
    }
}

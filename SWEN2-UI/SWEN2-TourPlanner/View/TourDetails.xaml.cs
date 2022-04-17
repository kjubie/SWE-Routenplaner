using SWEN2_Tourplanner_ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWEN2_TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für TourDetails.xaml
    /// </summary>
    public partial class TourDetails : UserControl
    {
        ToursViewModel ViewModel = new ToursViewModel();


        public TourDetails()
        {
            InitializeComponent();
            this.DataContext = ViewModel;

            //var selItem = MyTourListUC.TourListCollection.SelectedItem;

        }
    }
}

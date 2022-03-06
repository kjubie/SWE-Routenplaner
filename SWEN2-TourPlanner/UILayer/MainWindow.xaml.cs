using HandlebarsDotNet.Collections;
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
        ObservableCollection<string> TourNames = new ObservableCollection<string>();
        public MainWindow()
        {
            InitializeComponent();
            TourList.ItemsSource = TourNames;
        }

        private void ListView_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }


        private void AddTour(object sender, RoutedEventArgs e)
        {
            TourNames.Add("Tour " + TourNames.Count);

        }
      

        private void TourSearchBox_MouseEnter(object sender, MouseEventArgs e)
        {

            TourSearchBox.Text = "";
        }

        private void TourSearchBox_MouseLeave(object sender, MouseEventArgs e)
        {
            TourSearchBox.Text = "Search Tour...";

        }
    }
}

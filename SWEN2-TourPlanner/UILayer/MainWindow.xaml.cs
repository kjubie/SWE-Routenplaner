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
        public int Date = 10;

        class Log
        {
            public int Date { get; set; }   
            public int Duration { get; set; }
            public float Distance { get; set; }
            public string Comment { get; set; }
            public Log()
            {
                Date = 10;
                Duration = 10;
                Distance = 10;
                Comment = "Test";
            }
        }


        ObservableCollection<string> TourNames = new ObservableCollection<string>();
        ObservableCollection<Log> LogListObject = new ObservableCollection<Log>();
        public MainWindow()
        {
            InitializeComponent();
            TourList.ItemsSource = TourNames;
            LogListObject.Add(new Log());
            LogGrid.DataContext = LogListObject;
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
            TourSearchBox.Text = "Search Tours...";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogListObject.Add(new Log());
        }
    }
}

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
    /// Interaktionslogik für LogList.xaml
    /// </summary>
    /// 
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
    public partial class LogList : UserControl
    {

        ObservableCollection<Log> LogListObject = new ObservableCollection<Log>();


        public LogList()
        {
            InitializeComponent();
            LogListObject.Add(new Log());
            LogGrid.DataContext = LogListObject;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LogListObject.Add(new Log());
        }
    }
}

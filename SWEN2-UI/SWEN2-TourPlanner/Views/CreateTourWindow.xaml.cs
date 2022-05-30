using System;
using System.Windows;
using SWEN2_TourPlanner_ViewModels;

namespace SWEN2_TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für CreateTour.xaml
    /// </summary>
    public partial class CreateTourWindow : Window
    {

       
        public CreateTourWindow()
        {
            InitializeComponent();
          

           
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

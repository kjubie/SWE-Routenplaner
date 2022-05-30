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

        CreateTourViewModel ViewModel = new CreateTourViewModel();
       
        public CreateTourWindow()
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

using SWEN2_TourPlanner.View;
using SWEN2_TourPlanner.Views;
using SWEN2_Tourplanner_Models;
using SWEN2_Tourplanner_ViewModels;
using SWEN2_TourPlanner_ViewModels;
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
    public partial class ToursWindow : Window
    {


        ToursViewModel ViewModel = new ToursViewModel();


        public ToursWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;

            

        }

        private void OpenCreateWindow(object sender, RoutedEventArgs e)
        {
            CreateTourWindow createTourWindow = new CreateTourWindow();
            createTourWindow.Show();
        }

        private void OpenCreateTourLogWindow(object sender, RoutedEventArgs e)
        {
            CreateTourLogViewModel CreateLogViewModel = new CreateTourLogViewModel(ViewModel.GetSelectedTour().Name);
            CreateTourLogWindow createTourLogWindow = new CreateTourLogWindow(CreateLogViewModel);


            //createTourLogWindow.DataContext = CreateLogViewModel;

            createTourLogWindow.Show();
        }

        private void OpenModifyWindow(object sender, RoutedEventArgs e)
        {
            ModifyTourWindow modifyTourWindow = new ModifyTourWindow();

            ModifyTourViewModel modifyTourVM = new ModifyTourViewModel(ViewModel.GetSelectedTour());

            modifyTourWindow.DataContext = modifyTourVM;
            modifyTourWindow.Show();

            if (modifyTourVM.CloseAction == null)
            {
                modifyTourVM.CloseAction = new Action(modifyTourWindow.Close);
                ViewModel.LoadAllTours();

            }
        }

        private void OpenModifyTourlogWindow(object sender, RoutedEventArgs e)
        {
            ModifyTourLogWindow modifyTourLogWindow = new ModifyTourLogWindow();

            ModifyTourLogsViewModel modifyTourLogVM = new ModifyTourLogsViewModel(ViewModel.GetSelectedTourLog());

            modifyTourLogWindow.DataContext = modifyTourLogVM;
            modifyTourLogWindow.Show();

            if (modifyTourLogVM.CloseAction == null)
            {
                modifyTourLogVM.CloseAction = new Action(modifyTourLogWindow.Close);
                ViewModel.LoadAllTours();

            }
        }


        private void ReloadWindow(object sender, RoutedEventArgs e)
        {
            ViewModel.LoadAllTours();
        }

        private void OpenImportTourWindow(object sender, RoutedEventArgs e)
        {
            ImportTourWindow importTourWindow = new ImportTourWindow();
            ImportTourViewModel importTourVM = new ImportTourViewModel(ViewModel);
            importTourWindow.DataContext = importTourVM;


            importTourWindow.Show();

            if (importTourVM.CloseAction == null)
            {
                importTourVM.CloseAction = new Action(importTourWindow.Close);
                ViewModel.LoadAllTours();

            }

        }
    }
}

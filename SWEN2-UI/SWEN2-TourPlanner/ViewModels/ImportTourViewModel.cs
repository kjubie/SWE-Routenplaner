using SWEN2_TourPlanner.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWEN2_Tourplanner_ViewModels
{
    internal class ImportTourViewModel : INotifyPropertyChanged
    {

        private ToursViewModel _toursViewModel;

        public ImportTourViewModel(ToursViewModel toursViewModel)
        {
            _toursViewModel = toursViewModel;
        }

        private string _errorMsg { get; set; }

        public string ErrorMsg
        {
            get
            {
                return _errorMsg;
            }
            set
            {
                _errorMsg = value;
                OnPropertyChanged("ErrorMsg");
            }
        }



        private string _nameImport { get; set; }

        public string NameImport
        {
            get
            {
                return _nameImport;
            }
            set
            {
                _nameImport = value;
                OnPropertyChanged("ErrorMsg");
            }
        }




        private ICommand _importTourCommand;
        public ICommand ImportTourCommand
        {
            get
            {


                if (_importTourCommand != null)
                {
                    return _importTourCommand;
                }
                else
                {
                    _importTourCommand = new Command(() => ImportTour(), true);
                    return _importTourCommand;
                }

            }
        }


        async void ImportTour()
        {
            int respone = await _toursViewModel.ImportTour(_nameImport);

            if (respone == 0)
            {
                CloseAction();

            }
            else
            {
                ErrorMsg = "Error with importing, file odes not exist, or server not responding";
            }
        }


        public Action CloseAction { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}


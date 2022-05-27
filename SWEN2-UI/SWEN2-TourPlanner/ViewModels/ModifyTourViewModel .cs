
using SWEN2_TourPlanner.Commands;
using SWEN2_Tourplanner_DataAccess;
using SWEN2_Tourplanner_Models;
using SWEN2_Tourplanner_ViewModels;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWEN2_TourPlanner_ViewModels
{
    public class ModifyTourViewModel : INotifyPropertyChanged
    {

        private TourModel? _modTour;
        private string _oldTourname;

        public TourModel? ModTour
        {
            get { return _modTour; }
            set { _modTour = value; }

        }

        private IQuery _request;

        public ModifyTourViewModel(TourModel tour)
        {
            _request = new RESTRequest();
            _modTour = tour;
            _oldTourname = tour.Name;
        }



        private ICommand _updateTourCommand;
        public ICommand UpdateTourCommand
        {
            get
            {
                if (_updateTourCommand != null)
                {
                    return _updateTourCommand;
                }
                else
                {

                    _updateTourCommand = new Command(() => UpdateTour(), true);
                    return _updateTourCommand;
                }
            }
        }

        public void UpdateTour()
        {

            ErrorMsg = "";

            try
            {

                if (_modTour.From.IsNullOrWhiteSpace() || _modTour.To.IsNullOrWhiteSpace() || _modTour.Name.IsNullOrWhiteSpace())
                {

                    throw new ArgumentException("Input is not valid");


                }
                else
                {

                    if (_modTour.Description.IsNullOrWhiteSpace())
                    {
                        _modTour.Description = "";
                    }

                    _request.UpdateTour(_oldTourname, _modTour.From, _modTour.To, _modTour.Name, _modTour.TransportType, _modTour.Description);
                    CloseAction();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }

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

        public Action CloseAction { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}


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
    public class CreateTourViewModel : INotifyPropertyChanged
    {

        private TourModel? _createdTour;

        public TourModel? CreatedTour
        {
            get { return _createdTour; }
            set { _createdTour = value; }

        }

        private IQuery _request;

        public CreateTourViewModel()
        {
            ErrorMsg = "";

            _request = new RESTRequest();
            _createdTour = new TourModel();
        }

        private ICommand _saveCreatedCommand;
        public ICommand SaveCreatedCommand
        {
            get
            {
                if (_saveCreatedCommand != null)
                {
                    return _saveCreatedCommand;
                }
                else
                {

                    _saveCreatedCommand = new Command(() => SaveTour(), true);
                    return _saveCreatedCommand;
                }
            }
        }

        public void SaveTour()
        {
            ErrorMsg = "";

            try
            {

                if (_createdTour.From.IsNullOrWhiteSpace() || _createdTour.To.IsNullOrWhiteSpace() || _createdTour.Name.IsNullOrWhiteSpace())
                {
                    throw new ArgumentException("Input is not valid");

                }
                if (_createdTour.Description.IsNullOrWhiteSpace())
                {
                    _createdTour.Description = "";
                }


                _request.PostTour(_createdTour.From, _createdTour.To, _createdTour.Name, _createdTour.TransportType, _createdTour.Description);
                CloseAction();
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

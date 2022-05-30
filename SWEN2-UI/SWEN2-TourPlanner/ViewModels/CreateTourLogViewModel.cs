using SWEN2_TourPlanner.Commands;
using SWEN2_Tourplanner_DataAccess;
using SWEN2_Tourplanner_Models;
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
    public class CreateTourLogViewModel : INotifyPropertyChanged
    {

        private TourLogModel? _createdTourLog;

        public TourLogModel? CreatedTourLog
        {
            get { return _createdTourLog; }
            set { _createdTourLog = value; }

        }


        private IQuery _request;

        public CreateTourLogViewModel(string tourname)
        {
            ErrorMsg = "";
            _createdTourLog = new TourLogModel();
            _createdTourLog.Tourname = tourname;
            _request = new RESTRequest();
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
                    _saveCreatedCommand = new Command(() => SaveTourLog(), true);
                    return _saveCreatedCommand;
                }
            }
        }

        void SaveTourLog()
        {

            ErrorMsg = "";
            try
            {

                if (_createdTourLog.FormatedDate.IsNullOrWhiteSpace() || _createdTourLog.Difficulty == null || _createdTourLog.Time.IsNullOrWhiteSpace() || _createdTourLog.Rating == null)
                {
                    throw new ArgumentException("Input is not valid");
                }
                else
                {
                    //Rating to string
                    _createdTourLog.SetRatingByString();
                    //Difficulty to string
                    _createdTourLog.SetDifficultyByString();

                    _request.PostTourLog(_createdTourLog.Tourname, _createdTourLog.FormatedDate, _createdTourLog.Comment, _createdTourLog.Difficulty, _createdTourLog.Time, _createdTourLog.Rating);
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

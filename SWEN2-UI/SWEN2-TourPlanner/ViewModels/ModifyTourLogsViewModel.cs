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
    internal class ModifyTourLogsViewModel : INotifyPropertyChanged
    {

        private TourLogModel? _modTourLog;
        //private string _oldTourname;

        public TourLogModel? ModTourLog
        {
            get { return _modTourLog; }
            set { _modTourLog = value; }

        }

        private IQuery _request;

        public ModifyTourLogsViewModel(TourLogModel tourlog)
        {
            _request = new RESTRequest();
            _modTourLog = tourlog;
            //_oldTourname = tour.Name;
        }



        private ICommand _updateTourLogCommand;
        public ICommand UpdateTourLogCommand
        {
            get
            {
                if (_updateTourLogCommand != null)
                {
                    return _updateTourLogCommand;
                }
                else
                {

                    _updateTourLogCommand = new Command(() => UpdateTourLog(), true);
                    return _updateTourLogCommand;
                }
            }
        }

        public void UpdateTourLog()
        {


            try
            {

                if (_modTourLog.FormatedDate.IsNullOrWhiteSpace() || _modTourLog.Difficulty == null || _modTourLog.Time.IsNullOrWhiteSpace() || _modTourLog.Rating == null)
                {
                    throw new ArgumentException("Input is not valid");
                }
                else
                {

                    //Rating to string
                    _modTourLog.SetRatingByString();
                    //Difficulty to string
                    _modTourLog.SetDifficultyByString();

                    _request.UpdateTourLog(_modTourLog.Tourname, _modTourLog.FormatedDate, _modTourLog.Comment, _modTourLog.Difficulty, _modTourLog.Time, _modTourLog.Rating, _modTourLog.Id);
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

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Action CloseAction { get; set; }
    }
}

using SWEN2_TourPlanner.Commands;
using SWEN2_Tourplanner_DataAccess;
using SWEN2_Tourplanner_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWEN2_TourPlanner_ViewModels
{
    internal class ModifyTourLogsViewModel
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

            //Rating to string
            string ratingstr = _modTourLog.RatingStringToInt().ToString();
            //Difficulty to string
            string diffstr = _modTourLog.DifficultyStringToInt().ToString();

            _request.UpdateTourLog(_modTourLog.Tourname, _modTourLog.Date, _modTourLog.Comment, diffstr, _modTourLog.Time, ratingstr, _modTourLog.Id);
            CloseAction();

        }

        public Action CloseAction { get; set; }
    }
}

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

            try
            {

                if (_modTourLog.FormatedDate != null && _modTourLog.Difficulty != null && _modTourLog.Time != null && _modTourLog.Rating != null)
                {
                    //Rating to string
                    _modTourLog.SetRatingByString();
                    //Difficulty to string
                    _modTourLog.SetDifficultyByString();

                    _request.UpdateTourLog(_modTourLog.Tourname, _modTourLog.Date, _modTourLog.Comment, _modTourLog.Difficulty, _modTourLog.Time, _modTourLog.Rating, _modTourLog.Id);
                    CloseAction();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Action CloseAction { get; set; }
    }
}

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
    public class CreateTourLogViewModel
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
            try
            {

                if (_createdTourLog.FormatedDate != null && _createdTourLog.Difficulty != null && _createdTourLog.Time != null && _createdTourLog.Rating != null)
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

            }
        }

        public Action CloseAction { get; set; }

    }
}

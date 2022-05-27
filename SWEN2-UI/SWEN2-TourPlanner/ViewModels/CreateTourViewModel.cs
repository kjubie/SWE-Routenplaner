
using SWEN2_TourPlanner.Commands;
using SWEN2_Tourplanner_DataAccess;
using SWEN2_Tourplanner_Models;
using SWEN2_Tourplanner_ViewModels;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWEN2_TourPlanner_ViewModels
{
    public class CreateTourViewModel
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
            try
            {

                if (_createdTour.From != null && _createdTour.To != null && _createdTour.Name != null)
                {
                    if (_createdTour.Description.IsNullOrWhiteSpace())
                    {
                        _createdTour.Description = "";
                    }
                    _request.PostTour(_createdTour.From, _createdTour.To, _createdTour.Name, _createdTour.TransportType, _createdTour.Description);
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

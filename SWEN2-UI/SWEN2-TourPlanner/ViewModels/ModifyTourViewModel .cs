
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
    public class ModifyTourViewModel
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
            try
            {

                if (_modTour.From != null && _modTour.To != null && _modTour.Name != null)
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

            }

        }

        public Action CloseAction { get; set; }
    }
}

using SWEN2_REST.BL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWEN2_Tourplanner_ViewModel
{
    public class ToursViewModel
    {
        private static IList<TourModel>? _tourlist;
        private static Tours? _tours;
        private static TourModel? _selectedTour;

        public ToursViewModel()
        {
            _tourlist = new ObservableCollection<TourModel>();
            //_ = LoadToursAsync();
            
        }

        async Task LoadToursAsync()
        {         

            _tours = await RESTRequestModel.GetTours();

            Dictionary<string, Tour>.ValueCollection values = _tours.TourList.Values;
            foreach (Tour val in values)
            {
                _tourlist.Add(new TourModel(val));
            }
        }       

        public async static Task PostTourAsync(string from, string to, string tourname, string type, string description)
        {
            string task = await RESTRequestModel.PostTour(from, to, tourname, type, description);
            Tour tour = await RESTRequestModel.GetTour(tourname);
            _tours.AddTour(tour);
            _tourlist.Add(new TourModel(tour));
        }


        public static event EventHandler SelectedTourChanged;

        public static TourModel SelectedTour
        {
            get { return _selectedTour; }
            set {
                if (value != _selectedTour)
                {
                    _selectedTour = value;

                    if (SelectedTourChanged != null)
                        SelectedTourChanged(null, EventArgs.Empty);
                }
            }
        }

        public IList<TourModel>? TourListVM
        {
            get { return _tourlist; }
            set { _tourlist = value; }
        }

        public void AddTour(TourModel tour)
        {
            TourListVM.Add(tour);
        }
    }
}

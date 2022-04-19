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
        }

        public async Task LoadToursAsync()
        {
            _tours = await RESTRequest.GetTours();
            _tourlist.Clear();

            Dictionary<string, Tour>.ValueCollection values = _tours.TourList.Values;
            foreach (Tour val in values)
            {
                TourModel t = new TourModel(val);
                if (!(_tourlist.Contains(t)))
                {
                    _tourlist.Add(t);
                }
            }
        }

        public void DeleteTour(string nameTourToDelete)
        {

            string url = "https://localhost:7221/api/Tour/" + nameTourToDelete;
            using var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;

            _ = LoadToursAsync();
        }

        public async static Task PostTourAsync(string from, string to, string tourname, string type, string description)
        {
            string content = "{\"name\":\"" + tourname + "\",\"description\":\"" + description + "\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";

            var data = new StringContent(content, Encoding.UTF8, "application/json");


            var url = "https://localhost:7221/api/Tour";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            Tour tour = await RESTRequestModel.GetTour(tourname);
            _tours.AddTour(tour);
            _tourlist.Add(new TourModel(tour));
        }


        public static event EventHandler SelectedTourChanged;

        public static TourModel SelectedTour
        {
            get { return _selectedTour; }
            set
            {
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

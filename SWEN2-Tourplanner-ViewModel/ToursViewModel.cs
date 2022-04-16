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
        private static IList<TourModel> _tourlist;

        public ToursViewModel()
        {
            _tourlist = new ObservableCollection<TourModel>();
            _ = LoadToursAsync();
        }

        async Task LoadToursAsync()
        {
            

            Tours tourlist = await RESTRequest.GetTours();

            Dictionary<string, Tour>.ValueCollection values = tourlist.TourList.Values;
            foreach (Tour val in values)
            {
                _tourlist.Add(new TourModel(val));
            }


        }       

        public async static Task PostTourAsync(string from, string to, string tourname, string type, string description)
        {
            //string content = "{\"name\":\"" + tourname + ",\"description\":\"" + description + "\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";
            // string content = "{\"name\":\"SuperAwesomeTour\",\"description\":\"desc\",\"from\":\"Vienna, AT\",\"to\":\"Graz, AT\",\"routetype\":\"bicycle\",\"info\":\"info\",\"imagelocation\":\"loc\"}";
            string content = "{\"name\":\"" + tourname + "\",\"description\":\"" + description + "\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";

            var data = new StringContent(content, Encoding.UTF8, "application/json");


            var url = "https://localhost:7221/api/Tour";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

            Tour tour = await RESTRequestModel.GetTour(tourname);
            _tourlist.Add(new TourModel(tour));
        }

        public IList<TourModel> TourListVM
        {
            get { return _tourlist; }
            set { _tourlist = value; }
        }

        public void AddTour(TourModel tour)
        {
            TourListVM.Add(tour);
        }

        /*
        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                {

                    mUpdater = new Updater();
                }
                return mUpdater;
            }
            set
            {
                mUpdater = value;
            }
        }

        */
    }
}

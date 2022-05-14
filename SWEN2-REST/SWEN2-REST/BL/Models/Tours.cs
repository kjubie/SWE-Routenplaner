using System.Text.Json;

namespace SWEN2_REST.BL.Models {
    public class Tours {
        public Dictionary<string, Tour> TourList { get; set; }

        public Tours() {
            TourList = new Dictionary<string, Tour>();
        } 
        
        
        public Tours(string urlRESTServer) {
            TourList = new Dictionary<string, Tour>();
        }

        public int AddTour(Tour tour) {
            try {
                TourList.Add(tour.Name, tour);
                return 0;
            } catch (Exception ex) {
                return -1;
            }
        }

        public int RemoveTour(string name) {
            if (TourList.Remove(name))
                return 0;
            else 
                return -1;
        }

        public Tour GetTour(string name) {
            try {
                return TourList[name];
            } catch (Exception ex) {
                return null;
            }
        }

        public int UpdateTour(string name, Tour tour) {
            try {
                //TourList[name] = tour;
                if(RemoveTour(name) == 0)
                    if(AddTour(tour) == 0)
                        return 0;
                return 0;
            } catch (Exception ex) {
                return -1;
            }
        }
    }
}
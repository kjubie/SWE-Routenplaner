namespace SWEN2_REST.BL.Models {
    public class Tours {
        public Dictionary<string, Tour> TourList { get; set; }

        public Tours() {
            TourList = new Dictionary<string, Tour>();
        }

        public void AddTour(Tour tour) {
            TourList.Add(tour.Name, tour);

        }

        public void RemoveTour(string name) {
            TourList.Remove(name);
        }

        public Tour GetTour(string name) {
            return TourList[name];
        }
    }
}

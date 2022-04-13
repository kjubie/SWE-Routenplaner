namespace SWEN2_REST.BL.Models {
    public class Tours {
        public Dictionary<string, Tour> TourList { get; set; }

        public Tours() {
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

        public void RemoveTour(string name) {
            TourList.Remove(name);
        }

        public Tour GetTour(string name) {
            return TourList[name];
        }
    }
}

namespace SWEN2_Tourplanner_Models {
    public interface ITours {
        Dictionary<string, Tour> TourList { get; set; }

        int AddTour(Tour tour);
        Tour GetTour(string name);
        int RemoveTour(string name);
        int UpdateTour(string name, Tour tour);
    }
}
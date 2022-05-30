using SWEN2_Tourplanner_Models;

namespace SWEN2_REST.DAL {
    public interface ITourContext {
        int DeleteTour(string name);
        int DeleteTourLog(int id);
        void LoadTours(Tours tours);
        int SaveTour(Tour tour);
        int SaveTourLog(TourLog tourLog, int logID);
        int UpdateTour(string name, Tour tour);
        int UpdateTourLog(TourLog tourLog, int logID);
    }
}
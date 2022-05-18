using SWEN2_REST.BL.Models;
using SWEN2_Tourplanner_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN2_Tourplanner_DataAccess
{
    public interface IQuery
    {
        public Task<Tours> GetTours();

        public Task<Tour> GetTour(String tourname);

        public Task PostTour(string from, string to, string tourname, string type, string description);
        public Task UpdateTour(string oldTourName, string from, string to, string tourname, string type, string description);

        public void DeleteTour(string nameTourToDelete);

        public Task<string> GetImageBase64(String tourname);

        public Task<TourLog> GetTourLog(String tourname);




    }
}

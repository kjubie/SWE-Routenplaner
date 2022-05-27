using SWEN2_Tourplanner_Models;
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

        public void DeleteTour(string tourname);
        public void DeleteTourLog(string nameTourLogToDelete, int logId);

        public Task<string> GetImageBase64(String tourname);

        public Task PostTourLog(string tourname, string date, string comment, int difficulty, string time, int rating);
        public Task UpdateTourLog(string tourname, string date, string comment, int difficulty, string time, int rating, int id);

        public void GetPDFTourReport(string tourname);
        public void GetPDFSummarizedTourReport();

        public Task<Tours> GetToursBySearchAll(string searchterm);


    }
}

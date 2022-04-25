using SWEN2_Tourplanner_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_Routenplaner_DataAccess
{
    public interface IQuery
    {
        public Task<Tours> GetTours();

        public Task<Tour> GetTour(String tourname);

        public Task PostTour(string from, string to, string tourname, string type, string description);

        public void DeleteTour(string nameTourToDelete);
    }
}

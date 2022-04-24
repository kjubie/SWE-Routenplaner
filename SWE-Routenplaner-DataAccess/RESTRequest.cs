using SWEN2_Tourplanner_Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWEN2_Tourplanner_DataAccess
{
    public class RESTRequest
    {

        public async static Task<Tours> GetTours()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://localhost:7221/api/Tour");
            Tours? tours = JsonSerializer.Deserialize<Tours>(response.ToString());
            return tours;
        }

        public async static Task<Tour> GetTour(String tourname)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://localhost:7221/api/Tour/" + tourname);
            Tour? tour = JsonSerializer.Deserialize<Tour>(response.ToString());
            return tour;
        }
        public async static Task<Tours> PostTour()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://localhost:7221/api/Tour");
            Tours? tours = JsonSerializer.Deserialize<Tours>(response.ToString());
            return tours;
        }        
    }
}

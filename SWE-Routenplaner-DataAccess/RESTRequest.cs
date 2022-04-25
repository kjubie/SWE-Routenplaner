using SWE_Routenplaner_DataAccess;
using SWEN2_Tourplanner_Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SWEN2_Tourplanner_DataAccess
{
    public class RESTRequest : IQuery
    {

        private string _url = "https://localhost:7221/api/Tour";

        public async Task<Tours> GetTours()
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://localhost:7221/api/Tour");
            Tours? tours = JsonSerializer.Deserialize<Tours>(response.ToString());
            return tours;
        }

        public async Task<Tour> GetTour(String tourname)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://localhost:7221/api/Tour/" + tourname);
            Tour? tour = JsonSerializer.Deserialize<Tour>(response.ToString());
            return tour;
        }  

        public async Task PostTour(string from, string to, string tourname, string type, string description)
        {
            var url = "https://localhost:7221/api/Tour";
            string content = "{\"name\":\"" + tourname + "\",\"description\":\"" + description + "\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";

            var data = new StringContent(content, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

        }


        public void DeleteTour(string nameTourToDelete)
        {
            string url = "https://localhost:7221/api/Tour/" + nameTourToDelete;
            using var client = new HttpClient();
            var response = client.DeleteAsync(url).Result;
        }

    }
}

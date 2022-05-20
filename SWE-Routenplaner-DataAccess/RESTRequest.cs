using SWEN2_Tourplanner_Models;
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

        public async Task<string> GetImageBase64(String tourname)
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync("https://localhost:7221/api/Tour/image/" + tourname);

            return response.ToString();
        }

        public async Task PostTour(string from, string to, string tourname, string type, string description)
        {
            var url = "https://localhost:7221/api/Tour";
            string content = "{\"name\":\"" + tourname + "\",\"description\":\"" + description + "\",\"from\":\"" + from + ", AT\",\"to\":\"" + to + ", AT\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";

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


        public async Task UpdateTour(string oldTourName, string from, string to, string tourname, string type, string description)
        {
            var url = "https://localhost:7221/api/Tour/" + oldTourName;
            string content = "{\"name\":\"" + tourname + "\",\"description\":\"" + description + "\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";

            var data = new StringContent(content, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PutAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

        }


        public async Task PostTourLog(string tourname, string date, string comment, string difficulty, string time, string rating)
        {

            var url = "https://localhost:7221/api/TourLog/";
            string content = "{\"tourname\":\"" + tourname + "\",\"date\":\"" + date + "\",\"comment\":\"" + comment + "\",\"difficulty\":\"" + difficulty + "\",\"time\":\"" + time + "\",\"rating\":\"" + rating + "\"}";

            var data = new StringContent(content, Encoding.UTF8, "application/json");

            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

        }

    }
}

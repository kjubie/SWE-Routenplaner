using System.Text;
using System.Text.Json;

namespace SWEN2_REST.BL.Models
{
    public class RESTRequestModel
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
        public async static Task<string> PostTour(string from, string to, string tourname, string type, string description)
        {
            string content = "{\"name\":\"" + tourname + "\",\"description\":\"" + description + "\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";

            var data = new StringContent(content, Encoding.UTF8, "application/json");


            var url = "https://localhost:7221/api/Tour";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            return await response.Content.ReadAsStringAsync();

        }
    }
}

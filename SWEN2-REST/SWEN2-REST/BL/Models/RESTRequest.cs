using System.Text.Json;

namespace SWEN2_REST.BL.Models
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
    }
}

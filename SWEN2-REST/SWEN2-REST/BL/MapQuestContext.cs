using Newtonsoft.Json;
using System.Net;

namespace SWEN2_REST.BL {
    public class MapQuestContext {
        private API Api;
        private HttpClient HttpClient;
        private class API {
            public string? Key { get; set; }
            public string? Url { get; set; }
        }
        public MapQuestContext() {
            StreamReader streamReader = new("../../SWEN2-REST/apikey.json");
            string jsonString = streamReader.ReadToEnd();
#pragma warning disable CS8601 // Possible null reference assignment.
            Api = JsonConvert.DeserializeObject<API>(jsonString);
#pragma warning restore CS8601 // Possible null reference assignment.
            HttpClient = new();
        }

        public async Task<string> GetRouteAsync(string from, string to, string type) {
            from = from.Replace(" ", "%20");
            to = to.Replace(" ", "%20");
            Console.WriteLine(Api.Url + "?key=" + Api.Key + "&from=" + from + "&to=" + to + "&routeType=" + type + "&unit=k");
            var result = await HttpClient.GetAsync(Api.Url + "?key=" + Api.Key + "&from=" + from + "&to=" + to + "&routeType=" + type + "&unit=k");
            return await result.Content.ReadAsStringAsync();
        }
    }
}
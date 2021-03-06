using Newtonsoft.Json;
using System.Net;

namespace SWEN2_REST.BL {
    public class MapQuestContext : IMapQuestContext {
        private API Api;
        private HttpClient HttpClient;
        private class API {
            public string? Key { get; set; }
            public string? Url { get; set; }
            public string? MapUrl { get; set; }
        }
        public MapQuestContext() {
            StreamReader streamReader = new("../../SWEN2-REST/apikey.json");
            string jsonString = streamReader.ReadToEnd();
#pragma warning disable CS8601 // Possible null reference assignment.
            Api = JsonConvert.DeserializeObject<API>(jsonString);
#pragma warning restore CS8601 // Possible null reference assignment.
            HttpClient = new();
        }

        public MapQuestContext(string path) {
            StreamReader streamReader = new(path);
            string jsonString = streamReader.ReadToEnd();
#pragma warning disable CS8601 // Possible null reference assignment.
            Api = JsonConvert.DeserializeObject<API>(jsonString);
#pragma warning restore CS8601 // Possible null reference assignment.
            HttpClient = new();
        }

        public async Task<string> GetRouteAsync(string from, string to, string type) {
            from = from.Replace(" ", "%20");
            to = to.Replace(" ", "%20");
            var result = await HttpClient.GetAsync(Api.Url + "?key=" + Api.Key + "&from=" + from + "&to=" + to + "&routeType=" + type + "&unit=k");
            return await result.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> GetMapAsync(string from, string to) {
            from = from.Replace(" ", "%20");
            to = to.Replace(" ", "%20");
            var result = await HttpClient.GetAsync(Api.MapUrl + "?key=" + Api.Key + "&start=" + from + "&end=" + to);
            return await result.Content.ReadAsByteArrayAsync();
        }
    }
}
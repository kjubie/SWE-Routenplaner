using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SWEN2_REST.BL.Models;
using SWEN2_REST.DAL;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWEN2_REST.BL.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase {
        private readonly Tours _tours;
        private readonly MapQuestContext _mapQuestContext;
        private readonly TourContext _tourContext;

        public class Request {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? From { get; set; }
            public string? To { get; set; }
            public string? RouteType { get; set; }
            public string? Info { get; set; }
            public string? ImageLocation { get; set; }
        }

        public TourController(Tours tours, MapQuestContext mapQuestContext, TourContext tourContext) {
            _tours = tours;
            _mapQuestContext = mapQuestContext;
            _tourContext = tourContext;
        }

        [HttpGet]
        public string Get() {
            return JsonSerializer.Serialize(_tours);
        }

        [HttpGet("{name}")]
        public string Get(string name) {
            return JsonSerializer.Serialize(_tours.GetTour(name));
        }

        [HttpPost]
        public string Post(Request request) {
            var task = _mapQuestContext.GetRouteAsync(request.From, request.To, request.RouteType);
            task.Wait();
            var result = task.Result.ToString();

            dynamic res = JObject.Parse(result);

            Console.WriteLine(res.route.formattedTime.ToString());

            Tour t = new(request.Name, request.Description, request.From, request.To, request.RouteType, (double)res.route.distance, res.route.formattedTime.ToString(), request.Info, request.ImageLocation);

            if (_tours.AddTour(t) == 0) {
                _tourContext.SaveTour(t);
                return "Added new tour!";
            } else {
                return "Tour with this name already exists!";
            }
        }

        [HttpPut("{name}")]
        public void Put(string name, [FromBody] string value) {
        }

        [HttpDelete("{name}")]
        public void Delete(string name) {
        }
    }
}

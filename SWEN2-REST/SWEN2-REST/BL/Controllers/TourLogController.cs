using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SWEN2_REST.BL.Models;
using SWEN2_REST.DAL;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWEN2_REST.BL.Controllers {
    [Route("api/Log/[controller]")]
    [ApiController]
    public class TourLogController : ControllerBase {
        private readonly Tours _tours;
        private readonly TourContext _tourContext;

        public class Request {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? From { get; set; }
            public string? To { get; set; }
            public string? RouteType { get; set; }
            public string? Info { get; set; }
        }

        public TourLogController(Tours tours, TourContext tourContext) {
            _tours = tours;
            _tourContext = tourContext;
        }

        [HttpGet]
        public string Get() {
            return JsonSerializer.Serialize(_tours);
        }

        [HttpGet("{name}")]
        public string Get(string name) {
            return JsonSerializer.Serialize(_tours.GetTour(name).Log);
        }

        [HttpPost]
        public string Post(Request request) {
            return "";
        }

        [HttpPut("{name}")]
        public string Put(string name, Request request) {
            return "";
        }

        [HttpDelete("{name}")]
        public string Delete(string name) {
            return "";
        }
    }
}
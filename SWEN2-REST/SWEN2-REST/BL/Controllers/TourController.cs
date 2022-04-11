using Microsoft.AspNetCore.Mvc;

namespace SWEN2_REST.BL.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TourController : ControllerBase {
        private static readonly string[] Summaries = new[] {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TourController> _logger;

        public TourController(ILogger<TourController> logger) {
            _logger = logger;
        }

        [HttpGet(Name = "GetTour")]
        public IEnumerable<Tour> Get() {
            return Enumerable.Range(1, 5).Select(index => new Tour {
                Name = "Best Tour Ever!",
                Description = "Really the best tour ever!",
                From = "Vienna",
                To = "Not Vienna",
                TransportType = "On Foot",
                Distance = 1337,
                Time = DateTime.Now.AddDays(index),
                Info = "None"
            })
            .ToArray();
        }
    }
}

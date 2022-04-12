using Microsoft.AspNetCore.Mvc;
using SWEN2_REST.BL.Models;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWEN2_REST.BL.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase {
        private readonly Tours _tours;

        public TourController(Tours tours) {
            _tours = tours;
        }

        [HttpGet]
        public string Get() {
            return "Hallo";
        }

        [HttpGet("{name}")]
        public string Get(string name) {
            return JsonSerializer.Serialize(_tours.GetTour(name));
        }

        [HttpPost]
        public void Post([FromBody] string value) {
        }

        [HttpPut("{name}")]
        public void Put(string name, [FromBody] string value) {
        }

        [HttpDelete("{name}")]
        public void Delete(string name) {
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SWEN2_REST.BL.Models;
using SWEN2_REST.DAL;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWEN2_REST.BL.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TourLogController : ControllerBase {
        private readonly Tours _tours;
        private readonly TourContext _tourContext;

        public class LogRequest {
            public string? Tourname { get; set; }
            public string? Date { get; set; }
            public string? Comment { get; set; }
            public string? Difficulty { get; set; }
            public string? Time { get; set; }
            public int Rating { get; set; }
        }

        public TourLogController(Tours tours, TourContext tourContext) {
            _tours = tours;
            _tourContext = tourContext;
        }

        [HttpGet("{name}")]
        public string Get(string name) {
            return JsonSerializer.Serialize(_tours.GetTour(name).Log);
        }

        [HttpPost]
        public string Post(LogRequest request) {
            try {
                TourLog tl = new TourLog(request.Tourname, request.Date, request.Comment, request.Difficulty, request.Time, request.Rating);

                _tours.GetTour(request.Tourname).setLog(tl);
                if(_tourContext.SaveTourLog(tl) != 0)
                    return "Error while saving log to database";

                return "Added Tourlog to " + request.Tourname;
            } catch (Exception ex) {
                return "Tour with name " + request.Tourname + " does not exist!";
            }
        }

        [HttpPut("{name}")]
        public string Put(string name, LogRequest request) {
            try {
                TourLog tl = new TourLog(request.Tourname, request.Date, request.Comment, request.Difficulty, request.Time, request.Rating);

                _tours.GetTour(request.Tourname).setLog(tl);
                if (_tourContext.UpdateTourLog(tl) != 0)
                    return "Error while updating log in database";

                return "Updated Tourlog " + request.Tourname;
            } catch (Exception ex) {
                return "Tour with name " + request.Tourname + " does not exist!";
            }
        }

        [HttpDelete("{name}")]
        public string Delete(string name) {
            return "";
        }
    }
}
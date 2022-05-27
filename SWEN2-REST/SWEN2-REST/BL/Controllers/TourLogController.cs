using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SWEN2_Tourplanner_Models;
using SWEN2_REST.DAL;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SWEN2_REST.BL.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class TourLogController : ControllerBase {
        private readonly Tours _tours;
        private readonly TourContext _tourContext;
        private readonly ILogger<TourLogController> _logger;

        public class LogRequest {
            public string? Tourname { get; set; }
            public string? Date { get; set; }
            public string? Comment { get; set; }
            public int Difficulty { get; set; }
            public string? Time { get; set; }
            public int Rating { get; set; }
            public int Id { get; set; }
        }

        public TourLogController(Tours tours, TourContext tourContext, ILogger<TourLogController> logger) {
            _tours = tours;
            _tourContext = tourContext;
            _logger = logger;
        }

        [HttpGet("{name}")]
        public string Get(string name) {
            _logger.LogInformation("Get " + name + " log");
            return JsonSerializer.Serialize(_tours.GetTour(name).Logs);
        }

        [HttpPost]
        public string Post(LogRequest request) {
            try {
                Convert.ToDateTime(request.Date);
            } catch (Exception ex) {
                return "Invalid date format!";
            }

            try {
                _ = TimeSpan.Parse(request.Time).TotalHours;
            } catch (Exception ex) {
                return "Invalid time format!";
            }

            try {
                TourLog tl = new(request.Tourname, request.Date, request.Comment, request.Difficulty, request.Time, request.Rating);

                var ret = _tours.GetTour(request.Tourname).AddLog(tl);

                if (ret == -1) {
                    _logger.LogError("Error while adding log");
                    return "Error while adding log";
                }

                _tours.GetTour(request.Tourname).CalcPopularity();
                _tours.GetTour(request.Tourname).CalcChildfriendliness();

                if (_tourContext.SaveTourLog(tl, ret) != 0) {
                    _logger.LogError("Error while saving log to database");
                    return "Error while saving log to database";
                }

                _logger.LogInformation("Added tourlog to: " + request.Tourname);
                return "Added tourlog to " + request.Tourname;
            } catch (Exception ex) {
                _logger.LogError("Error: " + ex);
                return "Tour with name " + request.Tourname + " does not exist!";
            }
        }

        [HttpPut("{name}/{id}")]
        public string Put(string name, int id, LogRequest request) {
            if (!name.Equals(request.Tourname))
                return "Error: Name of the tour and the log must be the same!";
            try {
                TourLog tl = new(request.Tourname, request.Date, request.Comment, request.Difficulty, request.Time, request.Rating);

                if (_tours.GetTour(request.Tourname).UpdateLog(id, tl) == -1) {
                    _logger.LogError("Error while updating log: You can not change the tourname of the log!");
                    return "Error while updating log: You can not change the tourname of the log!";
                }

                _tours.GetTour(request.Tourname).CalcPopularity();
                _tours.GetTour(request.Tourname).CalcChildfriendliness();

                if (_tourContext.UpdateTourLog(tl, id) != 0) {
                    _logger.LogError("Error while updating log in database");
                    return "Error while updating log in database";
                }

                _logger.LogInformation("Updated tourlog from: " + request.Tourname);
                return "Updated Tourlog " + request.Tourname;
            } catch (Exception ex) {
                _logger.LogError("Error: " + ex);
                return "Tourlog with name " + request.Tourname + " and ID: " + id + "does not exist!";
            }
        }

        [HttpDelete("{name}/{id}")]
        public string Delete(string name, int id) {
            _logger.LogInformation("Delete " + name + " log");
            if (_tours.GetTour(name).RemoveLog(id) == 0)
                if (_tourContext.DeleteTourLog(id) == 0) {
                    _logger.LogInformation("Deleted tourlog: " + id);
                    return "Deleted tourlog with id: " + id;
                } else
                    return "Error while deleting tourlog from database!";
            else
                return "Error while deleting tourlog!";
        }
    }
}
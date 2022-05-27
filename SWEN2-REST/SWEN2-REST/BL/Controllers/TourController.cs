using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SWEN2_Tourplanner_Models;
using SWEN2_REST.DAL;
using System.Text.Json;
using System.Text;

namespace SWEN2_REST.BL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase {
        private readonly Tours _tours;
        private readonly MapQuestContext _mapQuestContext;
        private readonly TourContext _tourContext;
        private readonly FileHandler _fileHandler;
        private readonly ILogger<TourController> _logger;

        public class Request {
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? From { get; set; }
            public string? To { get; set; }
            public string? RouteType { get; set; }
            public string? Info { get; set; }
        }

        public TourController(Tours tours, MapQuestContext mapQuestContext, TourContext tourContext, FileHandler fileHandler, ILogger<TourController> logger) {
            _tours = tours;
            _mapQuestContext = mapQuestContext;
            _tourContext = tourContext;
            _fileHandler = fileHandler;
            _logger = logger;
        }

        [HttpGet]
        public string Get() {
            _logger.LogInformation("Get all tours");
            return JsonSerializer.Serialize(_tours);
        }

        [HttpGet("{name}")]
        public string Get(string name) {
            if (name.Equals("sumreport")) {
                _logger.LogInformation("Get summarized report");
                if (_tours.GenerateSummarizedReport() == -1)
                    return "Error while generating report!";
                return "Report generated!";
            }

            _logger.LogInformation("Get " + name + " tour");
            return JsonSerializer.Serialize(_tours.GetTour(name));
        }

        [HttpGet("recap/{year}")]
        public string GetJSON(int year) {
            _logger.LogInformation("Get recap report");
            if (_tours.GenerateRecap(year) == -1)
                return "Error while generating recap!";
            return "Report generated!";
        }

        [HttpGet("{name}/export")]
        public string GetJSON(string name) {
            _logger.LogInformation("Get " + name + " tour JSON");
            Tour tour = _tours.GetTour(name);
            var base64Image = _fileHandler.LoadFromFile(name);
            tour.ImageLocation = base64Image;

            return JsonSerializer.Serialize(tour);
        }

        [HttpGet("{name}/report")]
        public string GetReport(string name) {
            _logger.LogInformation("Get " + name + " report");
            if (_tours.GetTour(name).GeneratePDF() == -1)
                return "Error while generating report!";
            return "Report generated!";
        }

        [HttpGet("image/{name}")]
        public string GetImage(string name) {
            _logger.LogInformation("Get " + name + " image");
            return _fileHandler.LoadFromFile(name);
        }

        [HttpPost]
        public string Post(Request request) {
            try {
                if (!(request.RouteType.Equals("shortest") || request.RouteType.Equals("fastest") || request.RouteType.Equals("pedestrian") || request.RouteType.Equals("bicycle")))
                    return "Invalid route type!";
            } catch (Exception ex) {
                return "Invalid route type!";
            }

            var task = _mapQuestContext.GetRouteAsync(request.From, request.To, request.RouteType);
            task.Wait();
            var result = task.Result.ToString();
            dynamic res = JObject.Parse(result);

            var mapTask = _mapQuestContext.GetMapAsync(request.From, request.To);
            mapTask.Wait();

            _fileHandler.SaveImage(mapTask.Result, request.Name);

            var mapResultString = Convert.ToBase64String(mapTask.Result);

            Tour t = new(request.Name, request.Description, request.From, request.To, request.RouteType, (double)res.route.distance, res.route.formattedTime.ToString(), request.Info, "../../SWEN2-DB/routeImages/" + request.Name + ".txt");

            Task<int> fileTask;

            if (_tours.AddTour(t) == 0) {
                if (_tourContext.SaveTour(t) == 0) {
                    fileTask = _fileHandler.SaveToFileAsync(request.Name, mapResultString);
                    fileTask.Wait();
                    if (fileTask.Result == -1) {
                        return "Error while saving image!";
                    }
                    _logger.LogInformation("Added new tour: " + request.Name);
                    return "Added new tour!";
                } else {
                    return "Error while saving tour to database!";
                }
            } else {
                _logger.LogError("Tour with this name already exists: " + request.Name);
                return "Tour with this name already exists!";
            }
        }

        [HttpPost("import")]
        public string PostImport(string request) {
            Tour t = JsonSerializer.Deserialize<Tour>(request);
            var image = t.ImageLocation;
            t.ImageLocation = "../../SWEN2-DB/routeImages/" + t.Name + ".txt";

            _fileHandler.SaveImage(Encoding.ASCII.GetBytes(image), t.Name);
            

            Task<int> fileTask;

            if (_tours.AddTour(t) == 0) {
                if (_tourContext.SaveTour(t) == 0) {
                    fileTask = _fileHandler.SaveToFileAsync(t.Name, image);
                    fileTask.Wait();
                    if (fileTask.Result == -1) {
                        return "Error while saving image!";
                    }
                    _logger.LogInformation("Added new tour: " + t.Name);
                    return "Added new tour!";
                } else {
                    return "Error while saving tour to database!";
                }
            } else {
                _logger.LogError("Tour with this name already exists: " + t.Name);
                return "Tour with this name already exists!";
            }

            return "";
        }

        [HttpPut("{name}")]
        public string Put(string name, Request request) {
            if (!name.Equals(request.Name)) {
                return "You cannot change the name of the tour!";
            }

            try {
                if (!(request.RouteType.Equals("shortest") || request.RouteType.Equals("fastest") || request.RouteType.Equals("pedestrian") || request.RouteType.Equals("bicycle")))
                    return "Invalid route type!";
            } catch (Exception ex) {
                return "Invalid route type!";
            }

            var task = _mapQuestContext.GetRouteAsync(request.From, request.To, request.RouteType);
            task.Wait();
            var result = task.Result.ToString();
            dynamic res = JObject.Parse(result);

            var mapTask = _mapQuestContext.GetMapAsync(request.From, request.To);
            mapTask.Wait();
            var mapResultString = Convert.ToBase64String(mapTask.Result);

            Tour t = new(request.Name, request.Description, request.From, request.To, request.RouteType, (double)res.route.distance, res.route.formattedTime.ToString(), request.Info, "../../SWEN2-DB/routeImages/" + request.Name + ".txt");

            Task<int> fileTask;

            if (_tours.UpdateTour(name, t) == 0) {
                if (_tourContext.UpdateTour(name, t) == 0) {
                    fileTask = _fileHandler.SaveToFileAsync(request.Name, mapResultString);
                    fileTask.Wait();
                    if (fileTask.Result == -1)
                        return "Error while saving image!";
                    _logger.LogInformation("Updated tour: " + request.Name);
                    return "Added new tour!";
                } else
                    return "Error while saving tour to database!";
            } else
                return "Error while updating tour!";
        }

        [HttpDelete("{name}")]
        public string Delete(string name) {
            if (_tours.RemoveTour(name) == 0)
                if (_tourContext.DeleteTour(name) == 0) {
                    _logger.LogInformation("Deleted tour: " + name);
                    return "Deleted tour with name: " + name;
                } else
                    return "Error while deleting tour from database!";
            else
                return "Error while deleting tour!";
        }
    }
}
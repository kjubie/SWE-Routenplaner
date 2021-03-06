using Microsoft.AspNetCore.Mvc;
using SWEN2_Tourplanner_Models;
using SWEN2_REST.DAL;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Linq;

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

        public class TRequest {
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
        public string GetRecap(int year) {
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
            try {
                if (_tours.GetTour(name).GeneratePDF() == -1)
                    return "Error while generating report!";
            } catch {
                return "Tour with name: " + name + " does not exist!";
            }
            return "Report generated!";
        }

        [HttpGet("image/{name}")]
        public string GetImage(string name) {
            _logger.LogInformation("Get " + name + " image");
            return _fileHandler.LoadFromFile(name);
        }

        [HttpGet("search/{term}")]
        public string GetSearch(string term) {
            _logger.LogInformation("Searching " + term);
            Tours ts = new();

            int found = 0;

            foreach (Tour tour in _tours.TourList.Values) {
                if(tour.Name.Contains(term) || tour.Description.Contains(term) || tour.From.Contains(term) || tour.To.Contains(term) || tour.TransportType.Contains(term) || tour.Name.Contains(term) || tour.Info.Contains(term)) {
                    ts.AddTour(tour);
                    found = 1;
                    continue;
                }

                foreach (TourLog log in tour.Logs.Values) {
                    if (log.Tourname.Contains(term) || log.Date.Contains(term) || log.Comment.Contains(term)) {
                        ts.AddTour(tour);
                        found = 1;
                    }
                }
            }

            if (found == 0)
                return "Nothing found!";

            return JsonSerializer.Serialize(ts);
        }

        [HttpPost]
        public string Post(TRequest request) {
            if(_tours.TourList.ContainsKey(request.Name))
                return "Tour with this name already exists!";

            try {
                if (!(request.RouteType.Equals("shortest") || request.RouteType.Equals("fastest") || request.RouteType.Equals("pedestrian") || request.RouteType.Equals("bicycle")))
                    return "Invalid route type!";
            } catch (Exception ex) {
                return "Invalid route type exeption!";
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

            Task<string> fileTask;

            if (_tours.AddTour(t) == 0) {
                if (_tourContext.SaveTour(t) == 0) {
                    fileTask = _fileHandler.SaveToFileAsync(request.Name, mapResultString);
                    fileTask.Wait();
                    if (fileTask.Result != "") {
                        return fileTask.Result;
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
        public async Task<string> PostImport() {
            var jsonRequest = await new StreamReader(Request.Body).ReadToEndAsync();
            Tour t = JsonSerializer.Deserialize<Tour>(jsonRequest);

            var image = t.ImageLocation;
            t.ImageLocation = "../../SWEN2-DB/routeImages/" + t.Name + ".txt";

            _fileHandler.SaveImage(Encoding.ASCII.GetBytes(image), t.Name);
            
            Task<string> fileTask;

            if (_tours.AddTour(t) == 0) {
                if (_tourContext.SaveTour(t) == 0) {
                    fileTask = _fileHandler.SaveToFileAsync(t.Name, image);
                    fileTask.Wait();
                    if (fileTask.Result != "") {
                        return fileTask.Result;
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
        }

        [HttpPut("{name}")]
        public string Put(string name, TRequest request) {
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

            Task<string> fileTask;

            if (_tours.UpdateTour(name, t) == 0) {
                if (_tourContext.UpdateTour(name, t) == 0) {
                    fileTask = _fileHandler.SaveToFileAsync(request.Name, mapResultString);
                    fileTask.Wait();
                    if (fileTask.Result != "")
                        return fileTask.Result;
                    _logger.LogInformation("Updated tour: " + request.Name);
                    return "Updated tour!";
                } else
                    return "Error while updating tour to database!";
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
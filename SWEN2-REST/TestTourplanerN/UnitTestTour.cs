using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SWEN2_REST.BL;
using SWEN2_REST.BL.Controllers;
using SWEN2_REST.DAL;
using SWEN2_Tourplanner_Models;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using static SWEN2_REST.BL.Controllers.TourController;
using static SWEN2_REST.BL.Controllers.TourLogController;

namespace TestTourplanerN {
    public class Tests {
        private readonly Tours _tours;
        private readonly TourController _tc;
        private readonly TourLogController _tlc;

        public Tests() {
            var _FLogger = new Mock<ILogger<FileHandler>>();
            var _TCLogger = new Mock<ILogger<TourContext>>();
            var _TConLogger = new Mock<ILogger<TourController>>();
            var _TLCLogger = new Mock<ILogger<TourLogController>>();

            _tours = new Tours();
            _tc = new TourController(_tours, new MapQuestContext("../../../../../SWEN2-REST/apikey.json"), new TourContext(_TCLogger.Object, _tours, "../../../../../SWEN2-DB/dbconn.json"), new FileHandler(_FLogger.Object), _TConLogger.Object);
            _tlc = new TourLogController(_tours, new TourContext(_TCLogger.Object, _tours, "../../../../../SWEN2-DB/dbconn.json"), _TLCLogger.Object);        
        }

        [SetUp]
        public void Setup() {
            _tours.TourList = new();
            _tours.AddTour(new Tour("TestTour", "TestDescription", "TestFrom", "TestTo", "TestTransportType", 3.14, "03:32:45", "TestInfo", "../../../../../SWEN2-DB/routeImages/TestTour.txt"));
            _tours.AddTour(new Tour("Test2Tour", "TestDescription", "TestFrom", "TestTo", "TestTransportType", 3.14, "03:32:45", "TestInfo", "../../../../../SWEN2-DB/routeImages/TestTour.txt"));
            _tours.AddTour(new Tour("Test3Tour", "TestDescription", "TestFrom", "TestTo", "TestTransportType", 3.14, "03:32:45", "TestInfo", "../../../../../SWEN2-DB/routeImages/TestTour.txt"));
            _tours.AddTour(new Tour("TestLog", "TestDescription", "TestFrom", "TestTo", "TestTransportType", 3.14, "03:32:45", "TestInfo", "../../../../../SWEN2-DB/routeImages/TestTour.txt"));
            _tours.AddTour(new Tour("Test2Log", "TestDescription", "TestFrom", "TestTo", "TestTransportType", 3.14, "03:32:45", "TestInfo", "../../../../../SWEN2-DB/routeImages/TestTour.txt"));
            _tours.AddTour(new Tour("Test3Log", "TestDescription", "TestFrom", "TestTo", "TestTransportType", 3.14, "03:32:45", "TestInfo", "../../../../../SWEN2-DB/routeImages/TestTour.txt"));
            _tours.AddTour(new Tour("Test4Log", "TestDescription", "TestFrom", "TestTo", "TestTransportType", 3.14, "03:32:45", "TestInfo", "../../../../../SWEN2-DB/routeImages/TestTour.txt"));
            _tours.GetTour("TestLog").AddLog(new TourLog("TestLog", "28/05/2022", "TestComment", 5, "03:00:00", 5));
            _tours.GetTour("Test3Log").AddLog(new TourLog("Test3Log", "28/05/2022", "TestComment", 5, "03:00:00", 5));
            _tours.GetTour("Test4Log").AddLog(new TourLog("Test4Log", "28/05/2022", "TestComment", 5, "03:00:00", 5));
        }

        [Test]
        public void Get_Tours() {
            var result = _tc.Get();
            var isJSON = false;

            try {
                var obj = JToken.Parse(result);
                isJSON = true;
            } catch (Exception e) {

            }

            Assert.That(isJSON, Is.EqualTo(true));
        }

        [Test]
        public void Get_Tour() {
            var result = _tc.Get("TestTour");

            Assert.That(result, Is.EqualTo("{\"Name\":\"TestTour\",\"Description\":\"TestDescription\",\"From\":\"TestFrom\",\"To\":\"TestTo\",\"TransportType\":\"TestTransportType\",\"Distance\":3.14,\"Time\":\"03:32:45\",\"Info\":\"TestInfo\",\"ImageLocation\":\"../../../../../SWEN2-DB/routeImages/TestTour.txt\",\"Popularity\":1,\"Childfriendliness\":0,\"Logs\":{}}"));
        }

        [Test]
        public void Get_Sum_Report() {
            var result = _tc.Get("sumreport");

            Assert.That(result, Is.EqualTo("Report generated!"));
        }

        [Test]
        public void Get_Recap_Report() {
            var result = _tc.GetRecap(2022);

            Assert.That(result, Is.EqualTo("Report generated!"));
        }

        [Test]
        public void Get_Tour_Report() {
            var result = _tc.GetReport("TestTour");

            Assert.That(result, Is.EqualTo("Report generated!"));
        }

        [Test]
        public void Get_Non_Existent_Tour_Report() {
            var result = _tc.GetReport("NULL");

            Assert.That(result, Is.EqualTo("Tour with name: NULL does not exist!"));
        }

        [Test]
        public void Get_Search() {
            var result = _tc.GetSearch("TestTour");

            Assert.That(result, Is.EqualTo("{\"TourList\":{\"TestTour\":{\"Name\":\"TestTour\",\"Description\":\"TestDescription\",\"From\":\"TestFrom\",\"To\":\"TestTo\",\"TransportType\":\"TestTransportType\",\"Distance\":3.14,\"Time\":\"03:32:45\",\"Info\":\"TestInfo\",\"ImageLocation\":\"../../../../../SWEN2-DB/routeImages/TestTour.txt\",\"Popularity\":1,\"Childfriendliness\":0,\"Logs\":{}}}}"));
        }

        [Test]
        public void Get_Search_Non_Existent_Tour() {
            var result = _tc.GetSearch("NULL");

            Assert.That(result, Is.EqualTo("Nothing found!"));
        }

        [Test]
        public void Get_Image() {
            var result = _tc.GetImage("TestTour");

            Assert.That(result, Is.EqualTo("IMAGE!"));
        }

        [Test]
        public void Post_Tour() {
            var result = _tc.Post(JsonSerializer.Deserialize<Request>(@"{""Name"":""TestT"",""Description"":""desc"",""From"":""Vienna, AT"",""To"":""Graz, AT"",""RouteType"":""bicycle"",""Info"":""info""}"));
            var resultGet = _tc.Get("TestT");

            Assert.That(result, Is.EqualTo("Added new tour!"));
            Assert.That(resultGet, Is.EqualTo("{\"Name\":\"TestT\",\"Description\":\"desc\",\"From\":\"Vienna, AT\",\"To\":\"Graz, AT\",\"TransportType\":\"bicycle\",\"Distance\":194.7339,\"Time\":\"08:41:56\",\"Info\":\"info\",\"ImageLocation\":\"../../SWEN2-DB/routeImages/TestT.txt\",\"Popularity\":1,\"Childfriendliness\":0,\"Logs\":{}}"));
        }

        [Test]
        public void Post_Tour_Invalid_Transport_Type() {
            var result = _tc.Post(JsonSerializer.Deserialize<Request>(@"{""Name"":""TestT"",""Description"":""desc"",""From"":""Vienna, AT"",""To"":""Graz, AT"",""RouteType"":""invalid"",""Info"":""info""}"));

            Assert.That(result, Is.EqualTo("Invalid route type!"));
        }

        [Test]
        public void Post_Tour_Existing_Name() {
            var result = _tc.Post(JsonSerializer.Deserialize<Request>(@"{""Name"":""TestTour"",""Description"":""desc"",""From"":""Vienna, AT"",""To"":""Graz, AT"",""RouteType"":""bicycle"",""Info"":""info""}"));

            Assert.That(result, Is.EqualTo("Tour with this name already exists!"));
        }

        [Test]
        public void Put_Tour() {
            var result = _tc.Put("Test2Tour", JsonSerializer.Deserialize<Request>(@"{""Name"":""Test2Tour"",""Description"":""desc"",""From"":""Vienna, AT"",""To"":""Graz, AT"",""RouteType"":""bicycle"",""Info"":""info""}"));
            var resultGet = _tc.Get("Test2Tour");

            Assert.That(result, Is.EqualTo("Updated tour!"));
            Assert.That(resultGet, Is.EqualTo("{\"Name\":\"Test2Tour\",\"Description\":\"desc\",\"From\":\"Vienna, AT\",\"To\":\"Graz, AT\",\"TransportType\":\"bicycle\",\"Distance\":194.7339,\"Time\":\"08:41:56\",\"Info\":\"info\",\"ImageLocation\":\"../../SWEN2-DB/routeImages/Test2Tour.txt\",\"Popularity\":1,\"Childfriendliness\":0,\"Logs\":{}}"));
        }

        [Test]
        public void Put_Tour_Invalid_Name_Change() {
            var result = _tc.Put("Test2Tour", JsonSerializer.Deserialize<Request>(@"{""Name"":""Something"",""Description"":""desc"",""From"":""Vienna, AT"",""To"":""Graz, AT"",""RouteType"":""bicycle"",""Info"":""info""}"));

            Assert.That(result, Is.EqualTo("You cannot change the name of the tour!"));
        }

        [Test]
        public void Delete() {
            var result = _tc.Delete("Test3Tour");
            var resultGet = _tc.Get("Test3Tour");

            Assert.That(result, Is.EqualTo("Deleted tour with name: Test3Tour"));
            Assert.That(resultGet, Is.EqualTo("null"));
        }

        [Test]
        public void Get_Tour_Log() {
            var result = _tlc.Get("TestLog");

            Assert.That(result, Is.EqualTo("{\"1\":{\"Tourname\":\"TestLog\",\"Date\":\"28/05/2022\",\"Comment\":\"TestComment\",\"Difficulty\":5,\"Time\":\"03:00:00\",\"Rating\":5,\"Id\":1}}"));
        }

        [Test]
        public void Get_Tour_Log_Non_Existent_Tour() {
            var result = _tlc.Get("NULL");

            Assert.That(result, Is.EqualTo("Tour does not exist!"));
        }

        [Test]
        public void Get_Log_Search() {
            var result = _tlc.GetSearch("TestLog");

            Assert.That(result, Is.EqualTo("{\"TestLog log number: 1\":{\"Tourname\":\"TestLog\",\"Date\":\"28/05/2022\",\"Comment\":\"TestComment\",\"Difficulty\":5,\"Time\":\"03:00:00\",\"Rating\":5,\"Id\":1}}"));
        }

        [Test]
        public void Get_Log_Search_Non_Existent() {
            var result = _tlc.GetSearch("NULL");

            Assert.That(result, Is.EqualTo("Nothing found!"));
        }

        [Test]
        public void Post_Tour_Log() {
            var result = _tours.GetTour("Test2Log").AddLog(new TourLog("Test2Log", "27/05/2022", "ok", 3, "2:00:00", 5));
            var resultGet = _tlc.Get("Test2Log");

            Assert.That(result, Is.AtLeast(1));
            Assert.That(resultGet, Is.EqualTo("{\"1\":{\"Tourname\":\"Test2Log\",\"Date\":\"27/05/2022\",\"Comment\":\"ok\",\"Difficulty\":3,\"Time\":\"2:00:00\",\"Rating\":5,\"Id\":1}}"));
        }

        [Test]
        public void Post_Tour_Log_Invalid_Time() {
            var result = _tlc.Post(JsonSerializer.Deserialize<LogRequest>(@"{""Tourname"":""Test2Log"",""Date"":""27/05/2022"",""Comment"":""ok"",""Difficulty"":3,""Time"":""invalid"",""Rating"":5}"));

            Assert.That(result, Is.EqualTo("Invalid time format!"));
        }

        [Test]
        public void Post_Tour_Log_Invalid_Date() {
            var result = _tlc.Post(JsonSerializer.Deserialize<LogRequest>(@"{""Tourname"":""Test2Log"",""Date"":""invalid"",""Comment"":""ok"",""Difficulty"":3,""Time"":""02:00:00"",""Rating"":5}"));

            Assert.That(result, Is.EqualTo("Invalid date format!"));
        }

        [Test]
        public void Put_Tour_Log() {
            var result = _tours.GetTour("Test3Log").UpdateLog(1, new TourLog("Test3Log", "27/05/2022", "OK!", 3, "2:00:00", 5));
            var resultGet = _tlc.Get("Test3Log");

            Assert.That(result, Is.EqualTo(0));
            Assert.That(resultGet, Is.EqualTo("{\"1\":{\"Tourname\":\"Test3Log\",\"Date\":\"27/05/2022\",\"Comment\":\"OK!\",\"Difficulty\":3,\"Time\":\"2:00:00\",\"Rating\":5,\"Id\":1}}"));
        }

        [Test]
        public void Put_Tour_Log_Invalid_Name_Change() {
            var result = _tlc.Put("Test2Log", 1, JsonSerializer.Deserialize<LogRequest>(@"{""Tourname"":""Test5Log"",""Date"":""invalid"",""Comment"":""ok"",""Difficulty"":3,""Time"":""02:00:00"",""Rating"":5}"));

            Assert.That(result, Is.EqualTo("Name of the tour and the log must be the same!"));
        }

        [Test]
        public void Delete_Tour_Log() {
            var result = _tlc.Delete("Test4Log", 1);

            Assert.That(result, Is.EqualTo("Deleted tourlog with id: 1"));
        }

        [Test]
        public void Delete_Tour_Log_Invalid() {
            var result = _tlc.Delete("Test4Log", 10);

            Assert.That(result, Is.EqualTo("Error while deleting tourlog!"));
        }

        [TearDown]
        public void TearDown() {
            _tc.Delete("TestT");
        }
    }
}
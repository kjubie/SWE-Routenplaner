using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using System.Text.Json;

namespace SWEN2_Tourplanner_Models {
    public class Tours {
        public Dictionary<string, Tour> TourList { get; set; }

        public Tours() {
            TourList = new Dictionary<string, Tour>();
        } 
        
        
        public Tours(string urlRESTServer) {
            TourList = new Dictionary<string, Tour>();
        }

        public int AddTour(Tour tour) {
            try {
                TourList.Add(tour.Name, tour);
                return 0;
            } catch (Exception ex) {
                return -1;
            }
        }

        public int RemoveTour(string name) {
            if (TourList.Remove(name))
                return 0;
            else 
                return -1;
        }

        public Tour GetTour(string name) {
            try {
                return TourList[name];
            } catch (Exception ex) {
                return null;
            }
        }

        public int UpdateTour(string name, Tour tour) {
            try {
                if(RemoveTour(name) == 0)
                    if(AddTour(tour) == 0)
                        return 0;
                return 0;
            } catch (Exception ex) {
                return -1;
            }
        }

        internal int GenerateSummarizedReport() {
            Document document = new Document();

            foreach (Tour tour in TourList.Values) {
                var avgTime = 0.0;
                var avgDiff = 0;
                var avgRating = 0;

                if (tour.Logs.Count > 0) {
                    foreach (TourLog log in tour.Logs.Values) {
                        avgTime += TimeSpan.Parse(log.Time).TotalHours;
                        avgDiff += log.Difficulty;
                        avgRating += log.Rating;
                    }

                    avgTime /= tour.Logs.Count;
                    avgDiff /= tour.Logs.Count;
                    avgRating /= tour.Logs.Count;
                }

                Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
                document.Pages.Add(page);

                var text = new TextArea(tour.Name, 0, 0, 504, 100, Font.Helvetica, 16, TextAlign.Center);
                page.Elements.Add(text);

                var formattedString = "\n\n\nLog Count: " + tour.Logs.Count + "\n\nAverage Time (in h): " + Math.Round(avgTime, 2) + "\nAverage Difficulty: " + avgDiff + "\nAverage Rating: " + avgRating;

                text = new TextArea(formattedString, 0, 0, 504, 100, Font.Helvetica, 12, TextAlign.Left);
                page.Elements.Add(text);
            }
            try {
                document.Draw("SummarizedReport.pdf");
            } catch (Exception ex) {
                return -1;
            }
            return 0;
        }
    }
}
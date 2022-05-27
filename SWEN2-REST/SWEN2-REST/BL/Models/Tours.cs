using ceTe.DynamicPDF;
using ceTe.DynamicPDF.PageElements;
using ceTe.DynamicPDF.PageElements.Charting;
using ceTe.DynamicPDF.PageElements.Charting.Series;
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

        public class TourRecapData {
            public double Distance { get; set; } = 0;
            public double TotalDistance { get; set; } = 0;
            public double Time { get; set; } = 0;
            public double TotalTime { get; set; } = 0;
            public int Difficulty { get; set; } = 0;
            public int Car { get; set; } = 0;
            public int Pedestrian { get; set; } = 0;
            public int Bicycle { get; set; } = 0;
            public int TourCount { get; set; } = 0;
            public int LogCount { get; set; } = 0;
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
                document.Draw("../../SummarizedReport.pdf");
            } catch (Exception ex) {
                return -1;
            }
            return 0;
        }

        internal int GenerateRecap(int year) {
            TourRecapData TRD = new TourRecapData();

            foreach (Tour tour in TourList.Values) {
                var logCount = 0;
                foreach (TourLog log in tour.Logs.Values) {
                    if (Convert.ToDateTime(log.Date).Year == year) {
                        logCount++;
                        TRD.Distance += tour.Distance;
                        TRD.TotalDistance += tour.Distance;
                        TRD.Time += TimeSpan.Parse(log.Time).TotalHours;
                        TRD.TotalTime += TimeSpan.Parse(log.Time).TotalHours;
                        TRD.Difficulty += log.Difficulty;

                        switch (tour.TransportType) {
                            case "fastest" or "shortest":
                                TRD.Car++;
                                break;
                            case "pedestrian":
                                TRD.Pedestrian++;
                                break;
                            case "bicycle":
                                TRD.Bicycle++;
                                break;
                            default:
                                break;

                        }

                        TRD.LogCount++;
                    }
                }
                if(logCount > 0)
                    TRD.TourCount++;
            }

            TRD.Distance /= TRD.LogCount;
            TRD.Time /= TRD.LogCount;
            TRD.Difficulty /= TRD.LogCount;

            return GenerateRecapPDF(year, TRD);
        }

        private int GenerateRecapPDF(int year, TourRecapData TRD) {
            Document document = new Document();

            Page page = new Page(PageSize.Letter, PageOrientation.Portrait, 54.0f);
            document.Pages.Add(page);

            if (TRD.TourCount == 0) {

            }

            string Title = "Recap Year: " + year;
            Label label = new Label(Title, 0, 0, 504, 100, Font.Helvetica, 18, TextAlign.Center);
            page.Elements.Add(label);

            if (TRD.TourCount == 0) {
                var textNoTour = new TextArea("\n\n\n\nNo Tours for " + year + " found!", 0, 0, 504, 1000, Font.Helvetica, 12, TextAlign.Left);
                page.Elements.Add(textNoTour);
            } else {
                string formattedString = "\n\n\n\nYou have completed " + TRD.LogCount + " tours across " + TRD.TourCount + " different routes!" + "\n\nAverage distance traveled: " + Math.Round(TRD.Distance, 3) + " km" +
                    "\nTotal distance traveled: " + Math.Round(TRD.TotalDistance, 3) + " km" + "\n\nAverage time traveled: " + Math.Round(TRD.Time, 1) + " h" + "\nTotal time traveled: " + Math.Round(TRD.TotalTime, 1) + 
                    " h" + "\n\nAverage tour difficulty: " + TRD.Difficulty;

                var text = new TextArea(formattedString, 0, 0, 504, 1000, Font.Helvetica, 12, TextAlign.Left);
                page.Elements.Add(text);

                page = new Page(PageSize.A4, PageOrientation.Landscape);
                document.Pages.Add(page);

                Chart chart = new(0, 0, 700, 400);
                PlotArea plotArea = chart.PlotAreas.Add(50, 50, 300, 300);

                Title tTitle = new("Transport types used");
                chart.HeaderTitles.Add(tTitle);

                AutoGradient autogradient1 = new(90f, CmykColor.Red, CmykColor.IndianRed);
                AutoGradient autogradient2 = new(90f, CmykColor.Green, CmykColor.YellowGreen);
                AutoGradient autogradient3 = new(90f, CmykColor.Blue, CmykColor.LightBlue);

                ScalarDataLabel da = new(true, false, false);

                PieSeries pieSeries = new() {
                    DataLabel = da
                };

                plotArea.Series.Add(pieSeries);

                pieSeries.Elements.Add(TRD.Car, "Car");
                pieSeries.Elements.Add(TRD.Pedestrian, "Pedestrian");
                pieSeries.Elements.Add(TRD.Bicycle, "Bicycle");

                pieSeries.Elements[0].Color = autogradient1;
                pieSeries.Elements[1].Color = autogradient2;
                pieSeries.Elements[2].Color = autogradient3;

                page.Elements.Add(chart);
            }

            try {
                document.Draw("../../recapYear" + year + ".pdf");
            } catch (Exception ex) {
                return -1;
            }
            return 0;
        }
    }
}
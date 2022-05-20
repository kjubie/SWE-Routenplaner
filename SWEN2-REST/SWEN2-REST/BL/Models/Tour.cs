using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SWEN2_Tourplanner_Models {
    public class Tour {
        public string Name { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string TransportType { get; set; }
        public double Distance { get; set; }
        public string Time { get; set; }
        public string Info { get; set; }
        public string? ImageLocation { get; set; }
        public int Popularity { get; set; }
        public int Childfriendliness { get; set; }

        public Dictionary<int, TourLog> Logs { get; set; }

        public Tour() {

        }

        public Tour(string Name, string Description, string From, string To, string TransportType, double Distance, string Time, string Info, string ImageLocation) {
            this.Name = Name;
            this.Description = Description;
            this.From = From;
            this.To = To;
            this.TransportType = TransportType;
            this.Distance = Distance;
            this.Time = Time;
            this.Info = Info;
            this.ImageLocation = ImageLocation;
            Logs = new();
            CalcPopularity();
            CalcChildfriendliness();
        }

        public void CalcChildfriendliness() {
            if (Logs.Count > 0) {
                int averageDiff = 0;
                double averageTime = 0;

                foreach (TourLog log in Logs.Values) {
                    averageDiff += log.Difficulty;
                    averageTime += TimeSpan.Parse(log.Time).TotalHours;
                }

                averageDiff /= Logs.Count;
                averageTime /= Logs.Count;

                Console.WriteLine((int)(averageDiff * 5 + averageTime * 2 + Distance));
                Console.WriteLine("Diff: " + averageDiff);
                Console.WriteLine("Time: " + averageTime);
                Console.WriteLine("Dist: " + Distance);

                int sum = (int)(averageDiff * 5 + averageTime * 2 + Distance);

                if (sum <= 15)
                    Childfriendliness = 5;
                else if (sum <= 25)
                    Childfriendliness = 4;
                else if (sum <= 35)
                    Childfriendliness = 3;
                else if (sum <= 45)
                    Childfriendliness = 2;
                else
                    Childfriendliness = 1;
            }
        }

        public void CalcPopularity() {
            if (Logs.Count() <= 5)
                Popularity = 1;
            else if (Logs.Count() <= 10)
                Popularity = 2;
            else if (Logs.Count() <= 15)
                Popularity = 3;
            else if (Logs.Count() <= 20)
                Popularity = 4;
            else if (Logs.Count() <= 25)
                Popularity = 5;
        }

        public int AddLog(TourLog log) {
            if (Logs.TryAdd(Logs.Count() + 1, log))
                return 0;
            return -1;
        }

        public int RemoveLog(int id) {
            if (Logs.Remove(id))
                return 0;
            return -1;
        }

        public int UpdateLog(int id, TourLog log) {
            if (Equals(Logs[id].Tourname, log.Tourname)) {
                Logs[id] = log;
                return 0;
            }
            return -1;
        }
    }
}
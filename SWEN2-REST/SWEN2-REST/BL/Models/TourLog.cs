﻿namespace SWEN2_REST.BL.Models {
    public class TourLog {
        public string Tourname { get; set; }
        public string Date { get; set; }
        public string Comment { get; set; }
        public string Difficulty { get; set; }
        public string Time { get; set; }
        public int Rating { get; set; }

        public TourLog(string tourname) {
            Tourname = tourname;
        }

        public TourLog(string tourname, string date, string comment, string difficulty, string time, int rating) {
            Tourname = tourname;
            Date = date;
            Comment = comment;
            Difficulty = difficulty;
            Time = time;
            Rating = rating;
        }
    }
}
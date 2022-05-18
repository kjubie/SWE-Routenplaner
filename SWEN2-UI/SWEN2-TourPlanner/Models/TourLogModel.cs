using System.ComponentModel;

namespace SWEN2_REST.BL.Models
{
    public class TourLogModel
    {
        public TourLogModel(TourLog tourlog)
        {
            _tourname = tourlog.Tourname;
            _date = tourlog.Date;
            _difficulty = tourlog.Difficulty;
            _time = tourlog.Time;
            _rating = tourlog.Rating;
            _comment = tourlog.Comment;

        }
        public string Tourname
        {
            get
            {
                return _tourname;
            }
            set
            {
                _tourname = value;
                OnPropertyChanged("Description");
            }
        }
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged("Description");
            }
        }
        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                OnPropertyChanged("Description");
            }
        }
        public string Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value;
                OnPropertyChanged("Description");
            }
        }
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("Description");
            }
        }
        public int Rating
        {
            get
            {
                return _rating;
            }
            set
            {
                _rating = value;
                OnPropertyChanged("Description");
            }
        }

        private string _tourname { get; set; }
        private string _date { get; set; }
        private string _comment { get; set; }
        private string _difficulty { get; set; }
        private string _time { get; set; }
        private int _rating { get; set; }

        public TourLogModel(string tourname)
        {
            _tourname = tourname;
        }

        public TourLogModel(string tourname, string date, string comment, string difficulty, string time, int rating)
        {
            Tourname = tourname;
            Date = date;
            Comment = comment;
            Difficulty = difficulty;
            Time = time;
            Rating = rating;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

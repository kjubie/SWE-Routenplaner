using System.ComponentModel;

namespace SWEN2_Tourplanner_Models
{
    public class TourLogModel
    {

        public TourLogModel()
        {

        }

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
                OnPropertyChanged("Tourname");
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
                OnPropertyChanged("Date");
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
                OnPropertyChanged("Comment");
            }
        }
        public int Difficulty
        {
            get
            {
                if (DifficultyString == "very easy")
                {
                    return 1;
                }

                if (DifficultyString == "easy")
                {
                    return 2;
                }

                if (DifficultyString == "medium")
                {
                    return 3;
                }

                if (DifficultyString == "hard")
                {
                    return 4;
                }

                if (DifficultyString == "very hard")
                {
                    return 5;
                }

                return _difficulty;

            }
            set
            {
                _difficulty = value;
                OnPropertyChanged("Difficulty");
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
                OnPropertyChanged("Time");
            }
        }
        public int Rating
        {
            get
            {
                if (RatingString == "very bad")
                {
                    return 1;
                }

                if (RatingString == "bad")
                {
                    return 2;
                }

                if (RatingString == "medium")
                {
                    return 3;
                }

                if (RatingString == "great")
                {
                    return 4;
                }

                if (RatingString == "very great")
                {
                    return 5;
                }

                return _rating;
            }
            set
            {
                _rating = value;
                OnPropertyChanged("Rating");
            }
        }

        private string _tourname { get; set; }
        private string _date { get; set; }
        private string _comment { get; set; }
        private int _difficulty { get; set; }
        private string _time { get; set; }
        private int _rating { get; set; }

      

        private string _difficultyString { get; set; }
        public string DifficultyString
        {
            get
            {
                if (_rating == 1)
                {
                    return "very easy";
                }

                if (_rating == 2)
                {
                    return "easy";
                }

                if (_rating == 3)
                {
                    return "medium";
                }

                if (_rating == 4)
                {
                    return "hard";
                }

                if (_rating == 5)
                {
                    return "very hard";
                }

                return "";
            }
            set
            {
                _difficultyString = value;
                OnPropertyChanged("DifficultyString");
            }
        }

        private string _ratingString { get; set; }
        public string RatingString
        {
            get
            {
                if (_rating == 1)
                {
                    return "very bad";
                }

                if (_rating == 2)
                {
                    return "bad";
                }

                if (_rating == 3)
                {
                    return "medium";
                }

                if (_rating == 4)
                {
                    return "great";
                }

                if (_rating == 5)
                {
                    return "very great";
                }

                return "";
            }
            set
            {
                _ratingString = value;
                OnPropertyChanged("RatingString");
            }

        }



        public TourLogModel(string tourname)
        {
            _tourname = tourname;
        }

        public TourLogModel(string tourname, string date, string comment, int difficulty, string time, int rating)
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

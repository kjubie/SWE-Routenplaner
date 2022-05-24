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
            _id = tourlog.Id;
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

                return _difficulty;

            }
            set
            {
                _difficulty = value;

                if (_difficulty == 1)
                {
                    DifficultyString = "very easy";
                }
                if (_difficulty == 2)
                {
                    DifficultyString = "easy";
                }
                if (_difficulty == 3)
                {
                    DifficultyString = "medium";
                }
                if (_difficulty == 4)
                {
                    DifficultyString = "hard";
                }
                if (_difficulty == 5)
                {
                    DifficultyString = "very hard";
                }

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

            public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged("Time");
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


                if (_rating == 1)
                {
                    RatingString = "very bad";
                }
                if (_rating == 2)
                {
                    RatingString = "bad";
                }
                if (_rating == 3)
                {
                    RatingString = "medium";
                }
                if (_rating == 4)
                {
                    RatingString = "great";
                }
                if (_rating == 5)
                {
                    RatingString = "very great";
                }
                OnPropertyChanged("Rating");
            }
        }

        private string _tourname { get; set; }
        private string _date { get; set; }
        private string _comment { get; set; }
        private int _difficulty { get; set; }
        private string _time { get; set; }
        private int _rating { get; set; }
        private int _id { get; set; }



        private string _difficultyString { get; set; }
        public string DifficultyString
        {
            get
            {
                if (_difficulty == 1)
                {
                    _difficultyString = "very easy";
                }
                if (_difficulty == 2)
                {
                    _difficultyString = "easy";
                }
                if (_difficulty == 3)
                {
                    _difficultyString = "medium";
                }
                if (_difficulty == 4)
                {
                    _difficultyString = "hard";
                }
                if (_difficulty == 5)
                {
                    _difficultyString = "very hard";
                }

                return _difficultyString;
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
                    _ratingString = "very bad";
                }
                if (_rating == 2)
                {
                    _ratingString = "bad";
                }
                if (_rating == 3)
                {
                    _ratingString = "medium";
                }
                if (_rating == 4)
                {
                    _ratingString = "great";
                }
                if (_rating == 5)
                {
                    _ratingString = "very great";
                }

                return _ratingString;
            }
            set
            {
                _ratingString = value;
                OnPropertyChanged("RatingString");
            }

        }


        public int RatingStringToInt()
        {
            if (_ratingString == "very bad")
            {
                return 1;
            }
            if (_ratingString == "bad")
            {
                return 2;
            }
            if (_ratingString == "medium")
            {
                return 3;
            }
            if (_ratingString == "great")
            {
                return 4;
            }
            if (_ratingString == "very great")
            {
                return 5;
            }

            return 0;
        }

        public int DifficultyStringToInt()
        {
            if (_difficultyString == "very easy")
            {
                return 1;
            }
            if (_difficultyString == "easy")
            {
                return 2;
            }
            if (_difficultyString == "medium")
            {
                return 3;
            }
            if (_difficultyString == "hard")
            {
                return 4;
            }
            if (_difficultyString == "very hard")
            {
                return 5;
            }

            return 0;

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

using System;
using System.Collections.Generic;
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

            SetDifficultyStringByInt();
            SetRatingStringByInt();
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
        public string FormatedDate
        {
            get
            {
                DateTime datetime = Convert.ToDateTime(_date);

                string formateddate = datetime.Month + "/" + datetime.Day + "/" + datetime.Year;
                return formateddate;
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
                return _ratingString;
            }
            set
            {
                _ratingString = value;
                OnPropertyChanged("RatingString");
            }

        }


        public Dictionary<string, int> ratingDictionary = new Dictionary<string, int>()
        {
            { "not rated", 0 },
            { "very bad", 1 },
            { "bad", 2},
            { "medium", 3},
            { "great", 4},
            { "very great", 5}

        };
        public Dictionary<string, int> difficultyDictionary = new Dictionary<string, int>()
        {
            { "not rated", 0 },
            { "very easy", 1 },
            { "easy", 2},
            { "medium", 3},
            { "hard", 4},
            { "very hard", 5}
        };


        public Dictionary<int, string> ratingDictionaryinverted = new Dictionary<int, string>()
        {
            { 0,"not rated" },
            { 1, "very bad" },
            { 2,"bad"},
            { 3,"medium"},
            { 4,"great"},
            { 5,"very great"}

        };
        public Dictionary<int, string> difficultyDictionaryinverted = new Dictionary<int, string>()
        {
            { 0,"not rated"},
            { 1,"very easy"},
            { 2,"easy"},
            { 3,"medium"},
            { 4,"hard"},
            { 5,"very hard"}
        };


        public void SetDifficultyByString()
        {
            _difficulty = difficultyDictionary[_difficultyString];
        }

        public void SetRatingByString()
        {
            _rating = ratingDictionary[_ratingString];
        }
        public void SetDifficultyStringByInt()
        {
            _difficultyString = difficultyDictionaryinverted[_difficulty];
        }

        public void SetRatingStringByInt()
        {
            _ratingString = ratingDictionaryinverted[_rating];
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

using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SWEN2_Tourplanner_Models
{
    public class TourModel : INotifyPropertyChanged
    {

        private string name;
        private string description;
        private string from;
        private string to;
        private string transportType;
        private double distance;
        private string time;
        private string info;
        private string imageLocation;
        private string iconLocation;
        private int popularity;
        private int childfriendliness;
        public Dictionary<int, TourLog> Logs { get; set; }

        private BitmapImage image;

        public TourModel()
        {

        }

        public TourModel(Tour tour)
        {
            name = tour.Name;
            description = tour.Description;
            from = tour.From;
            to = tour.To;
            transportType = tour.TransportType;
            distance = tour.Distance;
            time = tour.Time;
            info = tour.Info;
            imageLocation = tour.ImageLocation;
            childfriendliness = tour.Childfriendliness;
            popularity = tour.Popularity;
            Logs = tour.Logs;

            TourLogList = new ObservableCollection<TourLogModel>();

            foreach (KeyValuePair<int, TourLog> entry in Logs)
            {
                TourLogModel newtourLog = new TourLogModel(entry.Value);

                TourLogList.Add(newtourLog);
            }
        }

        public string ChildfriendlinessImgPath
        {
            get
            {
                return "../icons/star" + childfriendliness.ToString() + ".png";
            }
        }
        public string PopularityImgPath
        {
            get
            {
                return "../icons/star" + popularity.ToString() + ".png";
            }
        }

        public string FormatedFromTo
        {
            get
            {
                return from + " - " + to;
            }
        }


        public string IconLocation
        {
            get
            {
                return "../icons/" + transportType + ".png";
            }
        }


        public BitmapImage TourImage
        {
            get
            {
                return new BitmapImage(new Uri(@"../mapImg/" + name + ".png", UriKind.Relative));
            }
        }


        public string FormatedTime
        {
            get
            {
                return "Estimated Time: " + time;
            }
        }

        public int NumberLogs
        {
            get
            {
                return Logs.Count;
            }
        }

        public string FormatedDistance
        {
            get
            {
                return "Distance: " + Math.Round(distance, 2) + " km";
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                ValidateStringIfEmpty(name);
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged("Description");
            }
        }
        public string From
        {
            get
            {
                return from;
            }
            set
            {
                from = value;
                ValidateStringIfEmpty(from);
                ValidateStringOnSpecialCharacters(from);
                OnPropertyChanged("From");
            }
        }
        public string To
        {
            get
            {
                return to;
            }
            set
            {
                to = value;
                ValidateStringIfEmpty(to);
                ValidateStringOnSpecialCharacters(to);
                OnPropertyChanged("To");
            }
        }
        public string TransportType
        {
            get
            {
                return transportType;
            }
            set
            {
                transportType = value;
                ValidateStringIfEmpty(transportType);
                OnPropertyChanged("TransportType");
            }
        }
        public double Distance
        {
            get
            {
                return distance;
            }
            set
            {
                distance = value;
                OnPropertyChanged("Distance");
            }
        }
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                OnPropertyChanged("Time");
            }
        }
        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                OnPropertyChanged("Info");
            }
        }
        public string ImageLocation
        {
            get
            {
                return imageLocation;
            }
            set
            {
                imageLocation = value;
                OnPropertyChanged("ImageLocation");
            }
        }

        public int Popularity
        {
            get
            {
                return popularity;
            }
            set
            {
                popularity = value;
                OnPropertyChanged("Popularity");
            }
        }

        public int Childfriendliness
        {
            get
            {
                return childfriendliness;
            }
            set
            {
                childfriendliness = value;
                OnPropertyChanged("Popularity");
            }
        }

        public BitmapImage Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        private ObservableCollection<TourLogModel> _tourLogList;

        public ObservableCollection<TourLogModel> TourLogList
        {
            get
            {
                return _tourLogList;
            }

            set { _tourLogList = value; }
        }


        void ValidateStringIfEmpty(string? text)
        {
            if (text.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("Required field is empty");
            }
        }


        void ValidateStringOnSpecialCharacters(string? text)
        {
         
            var regexItem = new Regex("^[a-zA-Z]*$");

            if (!regexItem.IsMatch(text))
            {
                throw new ArgumentException("No special Characters allowed!");
            }

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
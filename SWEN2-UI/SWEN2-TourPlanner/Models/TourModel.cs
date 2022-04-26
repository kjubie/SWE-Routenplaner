using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
        private BitmapImage image;

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

        }

        public string FormatedFromTo
        {
            get
            {
                return from + " - " + to;
            }
        }
        public string FormatedTime
        {
            get
            {
                return "Estimated Time: " + time;
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

        public TourModel()
        {

        }



        public TourModel(string Name, string Description, string From, string To, string TransportType, double Distance, string Time, string Info, string ImageLocation)
        {
            this.Name = Name;
            this.Description = Description;
            this.From = From;
            this.To = To;
            this.TransportType = TransportType;
            this.Distance = Distance;
            this.Time = Time;
            this.Info = Info;
            this.ImageLocation = ImageLocation;
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
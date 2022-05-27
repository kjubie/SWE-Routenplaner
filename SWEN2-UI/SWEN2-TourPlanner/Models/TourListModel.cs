using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SWEN2_Tourplanner_Models
{
    public class TourListModel
    {
        private ObservableCollection<TourModel>? _tourlist;
        private Dictionary<string, BitmapImage> _imgList;

        public TourListModel()
        {
            _tourlist = new ObservableCollection<TourModel>();
        }

        public TourListModel(Tours tours)
        {

        }

        public void Add(Tour tour, BitmapImage img)
        {

           

            TourModel tourmodel = new TourModel(tour);

          

            tourmodel.Image = img;
            _tourlist.Add(tourmodel);
        }

        public void AddImg(string tourName, BitmapImage img)
        {
            _imgList.Add(tourName, img);
        }

        public ObservableCollection<TourModel>? TourList
        {
            get { return _tourlist; }
            set { _tourlist = value; }
        }

        public Dictionary<string, BitmapImage>? ImgList
        {
            get { return _imgList; }
            set { _imgList = value; }
        }
    }
}

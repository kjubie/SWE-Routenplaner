using SWEN2_Tourplanner_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWEN2_TourPlanner_Models
{
    public class TourListModel
    {
        private ObservableCollection<TourModel>? _tourlist;

        public TourListModel()
        {
            _tourlist = new ObservableCollection<TourModel>();
        }

        public TourListModel(Tours tours)
        {

        }

        public void Add(Tour tour)
        {
            _tourlist.Add(new TourModel(tour));
        }

        public ObservableCollection<TourModel>? TourList
        {
            get { return _tourlist; }
            set { _tourlist = value; }
        }
    }
}

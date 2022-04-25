
using SWE_Routenplaner_DataAccess;
using SWEN2_Tourplanner_DataAccess;
using SWEN2_Tourplanner_Models;
using SWEN2_TourPlanner_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SWEN2_Tourplanner_ViewModels
{
    public class ToursViewModel
    {
        private TourListModel _tourlist;
        public ObservableCollection<TourModel> TourList
        {
            get { return _tourlist.TourList; }
            set { _tourlist.TourList = value; }
        }
        private TourModel? _selectedTour;
        public TourModel? SelectedTour
        {
            get { return _selectedTour; }
            set { _selectedTour = value; }
        }

        private IQuery _request;

        public ToursViewModel()
        {
            _request = new RESTRequest();
            _tourlist = new TourListModel();
            _selectedTour = null;
            LoadTourList();

        }
        public async void LoadTourList()
        {
            Tours tours = await _request.GetTours();

            Dictionary<string, Tour>.ValueCollection values = tours.TourList.Values;
            foreach (Tour val in values)
            {
                _tourlist.Add(val);
            }
        }
    }

}

/*
public async Task LoadToursAsync()
{
    _tours = await RESTRequest.GetTours();
    _tourlist.Clear();

    Dictionary<string, Tour>.ValueCollection values = _tours.TourList.Values;
    foreach (Tour val in values)
    {
        TourModel t = new TourModel(val);
        if (!(_tourlist.Contains(t)))
        {
            _tourlist.Add(t);
        }
    }
}

public void DeleteTour(string nameTourToDelete)
{

    string url = "https://localhost:7221/api/Tour/" + nameTourToDelete;
    using var client = new HttpClient();
    var response = client.DeleteAsync(url).Result;

    _ = LoadToursAsync();
}



public static event EventHandler SelectedTourChanged;

public static TourModel SelectedTour
{
    get { return _selectedTour; }
    set
    {
        if (value != _selectedTour)
        {
            _selectedTour = value;

            if (SelectedTourChanged != null)
                SelectedTourChanged(null, EventArgs.Empty);
        }
    }
}

public TourListModel TourList
{
    get { return _tourlist; }
    set { _tourlist = value; }
}

public void AddTour(TourModel tour)
{
    //TourList.Add(tour);
}


}
}
*/
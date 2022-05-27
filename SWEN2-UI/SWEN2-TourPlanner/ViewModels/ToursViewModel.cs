
using SWEN2_Tourplanner_Models;
using SWEN2_TourPlanner.Commands;
using SWEN2_Tourplanner_DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace SWEN2_Tourplanner_ViewModels
{
    public class ToursViewModel
    {

        // private TourLogModel _tourLogsSelectedTour;

        /*
        public ObservableCollection<TourLogModel> _tourLogList;

        public ObservableCollection<TourLogModel> TourLogList
        {
            get { return _tourLogList; }
            set { _tourLogList = value; }
        }
        */

        private TourListModel _tourlist;

        public ObservableCollection<TourModel> TourList
        {
            get
            {
                return _tourlist.TourList;
            }

            set
            {
                _tourlist.TourList = value;
            }
        }
        private TourModel? _selectedTour;
        public TourModel? SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                _selectedTour = value;

            }

        }

        private bool _isTourLogSelected;
        public bool IsTourLogSelected
        {
            get { return _isTourLogSelected; }

            set
            {

                _isTourLogSelected = value;
                OnPropertyChanged("IsTourLogSelected");

            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string _searchToursText;
        public string SearchToursText
        {
            get { return _searchToursText; }

            set
            {
                _searchToursText = value;
                OnPropertyChanged("SearchToursText");

            }

        }
        private string _searchTourLogsText;
        public string SearchTourLogsText
        {
            get { return _searchTourLogsText; }

            set
            {
                _searchTourLogsText = value;
                OnPropertyChanged("SearchTourLogsText");

            }

        }


        private TourLogModel? _selectedTourLog;
        public TourLogModel? SelectedTourLog
        {
            get { return _selectedTourLog; }
            set
            {

                this.IsTourLogSelected = true;

                _selectedTourLog = value;
            }

        }

        private IQuery _request;

        public ToursViewModel()
        {
            _request = new RESTRequest();
            _tourlist = new TourListModel();
            _selectedTour = null;
            _selectedTourLog = null;
            LoadTourList();

        }
        public async void LoadTourList()
        {

            try
            {
                _tourlist.TourList.Clear();
                Tours tours = await _request.GetTours();

                Dictionary<string, Tour>.ValueCollection values = tours.TourList.Values;


                DirectoryInfo di = new DirectoryInfo("../../../mapImg");


                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }


                foreach (Tour tour in values)
                {
                    string img = await _request.GetImageBase64(tour.Name);
                    byte[] binaryData = Convert.FromBase64String(img);
                    //File.WriteAllBytes("../../../mapImg/" + val.Name + ".png", binaryData);            

                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(binaryData);
                    bi.EndInit();

                    _tourlist.Add(tour, bi);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }
           public async void LoadTourListBySearch()
        {

            try
            {
                _tourlist.TourList.Clear();
                Tours tours = await _request.GetToursBySearchAll(SearchToursText);

                Dictionary<string, Tour>.ValueCollection values = tours.TourList.Values;


                DirectoryInfo di = new DirectoryInfo("../../../mapImg");


                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }


                foreach (Tour tour in values)
                {
                    string img = await _request.GetImageBase64(tour.Name);
                    byte[] binaryData = Convert.FromBase64String(img);
                    //File.WriteAllBytes("../../../mapImg/" + val.Name + ".png", binaryData);            

                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(binaryData);
                    bi.EndInit();

                    _tourlist.Add(tour, bi);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private ICommand _deleteSelectedCommand;
        public ICommand DeleteSelectedCommand
        {
            get
            {


                if (_deleteSelectedCommand != null)
                {
                    return _deleteSelectedCommand;
                }
                else
                {

                    _deleteSelectedCommand = new Command(() => DeleteTour(), true);
                    return _deleteSelectedCommand;
                }

            }
        }
        private ICommand _deleteSelectedLogCommand;
        public ICommand DeleteSelectedLogCommand
        {
            get
            {


                if (_deleteSelectedLogCommand != null)
                {
                    return _deleteSelectedLogCommand;
                }
                else
                {
                    _deleteSelectedLogCommand = new Command(() => DeleteTourLog(), true);
                    return _deleteSelectedLogCommand;
                }

            }
        }

        private ICommand _generateTourReport;
        public ICommand GenerateTourReport
        {
            get
            {


                if (_generateTourReport != null)
                {
                    return _generateTourReport;
                }
                else
                {
                    _generateTourReport = new Command(() => GeneratePDFTour(), true);
                    return _generateTourReport;
                }

            }
        }


        private ICommand _generateSummarizedTourReport;
        public ICommand GenerateSummarizedTourReport
        {
            get
            {


                if (_generateSummarizedTourReport != null)
                {
                    return _generateSummarizedTourReport;
                }
                else
                {
                    _generateSummarizedTourReport = new Command(() => GeneratePDFSummarizedTour(), true);
                    return _generateSummarizedTourReport;
                }

            }
        }
        
        private ICommand _searchTours;
        public ICommand SearchTours
        {
            get
            {


                if (_searchTours != null)
                {
                    return _searchTours;
                }
                else
                {
                    _searchTours = new Command(() => LoadTourListBySearch(), true);
                    return _searchTours;
                }

            }
        }



        public TourModel GetSelectedTour()
        {
            return _selectedTour;
        }
        public TourLogModel GetSelectedTourLog()
        {
            return _selectedTourLog;
        }

        public void DeleteTour()
        {
            try
            {

                if (_selectedTour != null)
                {
                    _request.DeleteTour(_selectedTour.Name);
                    LoadTourList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void DeleteTourLog()
        {
            try
            {

                if (_selectedTourLog != null)
                {


                    _request.DeleteTourLog(_selectedTourLog.Tourname, _selectedTourLog.Id);

                    LoadTourList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}


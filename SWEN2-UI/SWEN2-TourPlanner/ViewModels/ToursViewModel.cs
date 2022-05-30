
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
using System.Text.Json;

namespace SWEN2_Tourplanner_ViewModels
{
    public class ToursViewModel : INotifyPropertyChanged
    {

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
                OnPropertyChanged("TourList");
            }
        }


        private TourModel? _selectedTour;
        public TourModel? SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if (value != null)
                {
                    IsTourSelected = true;
                    IsTourLogSelected = false;       
                }

                if (value == null)
                {
                    IsTourSelected = false;
                    IsTourLogSelected = false;

                }
                _selectedTour = value;
                OnPropertyChanged("SelectedTour");
            }
        }       
        private bool _isTourSelected;
        public bool IsTourSelected
        {
            get { return _isTourSelected; }

            set
            {
                _isTourSelected = value;
                OnPropertyChanged("IsTourSelected");

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

        private TourLogModel? _selectedTourLog;
        public TourLogModel? SelectedTourLog
        {
            get { return _selectedTourLog; }
            set
            {
                this.IsTourLogSelected = true;            

                if (value == null)
                {

                    IsTourLogSelected = false;

                }

                _selectedTourLog = value;
                OnPropertyChanged("SelectedTourLog");
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
        private IQuery _request;

        public ToursViewModel()
        {
            _request = new RESTRequest();
            _tourlist = new TourListModel();
            _selectedTour = null;
            _selectedTourLog = null;
            LoadAllTours();

        }

        public async Task<Tours> GetAllTours()
        {
            Tours tours = await _request.GetTours();
            return tours;
        }

        public async Task<Tours> GetSearchedTours()
        {
            Tours tours = await _request.GetToursBySearch(SearchToursText);
            return tours;
        }

        public async void LoadAllTours()
        {
            Tours tours = await GetAllTours();
            LoadTourList(tours);
        }
        public async void LoadSearchedTours()
        {
            ErrorMsg = "";

            if (SearchToursText == "")
            {
                LoadAllTours();
                return;
            }


            Tours tours = await GetSearchedTours();
            LoadTourList(tours);
        }

        public async void LoadTourList(Tours tours)
        {
            ErrorMsg = "";

            try
            {
                _tourlist.TourList.Clear();

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
                    File.WriteAllBytes("../../../mapImg/" + tour.Name + ".png", binaryData);

                    BitmapImage bi = new BitmapImage();
                    if (img != "")
                    {
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.EndInit();

                    }

                    _tourlist.Add(tour, bi);
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }

        public async void ExportTour()
        {
            ErrorMsg = "";

            try
            {
                string tourJson = await _request.ExportTour(SelectedTour.Name);
                File.WriteAllText("../../../ExportTours/" + SelectedTour.Name + ".json", tourJson);
                ErrorMsg = SelectedTour.Name + ".json exported to ExportTours folder";


            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }



        public async Task<int> ImportTour(string tourname)
        {
            
            try
            {

                string contents = File.ReadAllText("../../../ExportTours/" + tourname + ".json");
                if (contents == "")
                {
                    return -1;
                }
                else
                {
                    await _request.ImportTour(contents);
                    LoadAllTours();
                    return 0;
                }
                //File.WriteAllText("../../../ExportTours/" + SelectedTour.Name + ".json", tourJson);
                //ErrorMsg = SelectedTour.Name + ".json exported to ExportTours folder";


            }
            catch (Exception ex)
            {
                return -1;
            }

            return 0;
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
                    _searchTours = new Command(() => LoadSearchedTours(), true);
                    return _searchTours;
                }

            }
        }

        private ICommand _searchTourLogs;
        public ICommand SearchTourLogs
        {
            get
            {


                if (_searchTourLogs != null)
                {
                    return _searchTourLogs;
                }
                else
                {
                    _searchTourLogs = new Command(() => LoadSearchedTours(), true);
                    return _searchTourLogs;
                }

            }
        }


        private ICommand _exportTourCommand;
        public ICommand ExportTourCommand
        {
            get
            {


                if (_exportTourCommand != null)
                {
                    return _exportTourCommand;
                }
                else
                {
                    _exportTourCommand = new Command(() => ExportTour(), true);
                    return _exportTourCommand;
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
                    LoadAllTours();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }


        public void DeleteTourLog()
        {
            try
            {

                if (_selectedTourLog != null)
                {


                    _request.DeleteTourLog(_selectedTourLog.Tourname, _selectedTourLog.Id);

                    LoadAllTours();
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }



        public void GeneratePDFTour()
        {
            try
            {

                if (_selectedTour != null)
                {
                    _request.GetPDFTourReport(_selectedTour.Name);
                    ErrorMsg = "PDF for " + _selectedTour.Name + " generated";
                }
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }


        public void GeneratePDFSummarizedTour()
        {
            try
            {
                _request.GetPDFSummarizedTourReport();
                ErrorMsg = "PDF for summarized report generated";
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
            }
        }


     
           


        private string _errorMsg { get; set; }

        public string ErrorMsg
        {
            get
            {
                return _errorMsg;
            }
            set
            {
                _errorMsg = value;
                OnPropertyChanged("ErrorMsg");
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
    }
}


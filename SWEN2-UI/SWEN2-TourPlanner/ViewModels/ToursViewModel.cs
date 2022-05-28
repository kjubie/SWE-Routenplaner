﻿
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

                if (value != null)
                {

                    IsTourLogSelected = true;

                }

                if (value == null)
                {

                    IsTourLogSelected = false;

                }
                _selectedTourLog = value;
                OnPropertyChanged("SelectedTourLog");

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
            ErrorMsg = "";

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


            ErrorMsg = "";

            if (SearchToursText == "")
            {
                LoadTourList();
                return;
            }

            try
            {
                _tourlist.TourList.Clear();
                Tours tours = await _request.GetToursBySearchAll(SearchToursText);

                if (tours != null)
                {

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

            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                ErrorMsg = "No Tours";
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

                    LoadTourList();
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
                    LoadTourList();
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


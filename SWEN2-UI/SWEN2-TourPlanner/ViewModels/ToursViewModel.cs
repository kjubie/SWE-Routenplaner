
using SWEN2_TourPlanner.Commands;
using SWEN2_Tourplanner_DataAccess;
using SWEN2_Tourplanner_Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Windows.Media.Imaging;


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
            _tourlist.TourList.Clear();
            Tours tours = await _request.GetTours();

            Dictionary<string, Tour>.ValueCollection values = tours.TourList.Values;

            /*
            DirectoryInfo di = new DirectoryInfo("../../../mapImg");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
            */

            foreach (Tour val in values)
            {


                string img = await _request.GetImageBase64(val.Name);




                byte[] binaryData = Convert.FromBase64String(img);
               // File.WriteAllBytes("../../../mapImg/" + val.Name + ".png", binaryData);


            

                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(binaryData);
                bi.EndInit();

                

                _tourlist.Add(val, bi);
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

                    _deleteSelectedCommand = new DeleteCommand(param => DeleteTour(param), true);
                    return _deleteSelectedCommand;
                }
            }
        }

        public void DeleteTour(object TourName)
        {
            if (_selectedTour != null)
            {
                _request.DeleteTour(_selectedTour.Name);
                LoadTourList();
            }
        }
    }
}


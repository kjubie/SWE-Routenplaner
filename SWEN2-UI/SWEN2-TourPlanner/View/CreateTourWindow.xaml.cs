using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using SWEN2_REST.BL.Models;
using SWEN2_Tourplanner_ViewModel;

namespace SWEN2_TourPlanner.View
{
    /// <summary>
    /// Interaktionslogik für CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        private static readonly HttpClient client = new HttpClient();
        public CreateTour()
        {
            InitializeComponent();
        }

        async void SaveTour(object sender, RoutedEventArgs e)
        {
            string from = From.Text;
            string to = To.Text;
            string tourname = TourName.Text;
            string type = MeanOfTransport.Text;
            string description = Description.Text;

            await ToursViewModel.PostTourAsync(from, to, tourname, type, description);

            Close();

        }

            /*
        async Task PostTourAsync(string from, string to, string tourname, string type, string description)
        {   
            //string content = "{\"name\":\"" + tourname + ",\"description\":\"" + description + "\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";
           // string content = "{\"name\":\"SuperAwesomeTour\",\"description\":\"desc\",\"from\":\"Vienna, AT\",\"to\":\"Graz, AT\",\"routetype\":\"bicycle\",\"info\":\"info\",\"imagelocation\":\"loc\"}";
            string content = "{\"name\":\"" + tourname + "\",\"description\":\"" + description +"\",\"from\":\"" + from + "\",\"to\":\"" + to + "\",\"routetype\":\"" + type + "\",\"info\":\"info\",\"imagelocation\":\"loc\"}";
           
            var data = new StringContent(content, Encoding.UTF8, "application/json");


            var url = "https://localhost:7221/api/Tour";
            using var client = new HttpClient();

            var response = await client.PostAsync(url, data);

            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
        }
            */





        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

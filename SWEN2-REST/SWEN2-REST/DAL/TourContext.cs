using Newtonsoft.Json;
using Npgsql;
using SWEN2_REST.BL.Models;

namespace SWEN2_REST.DAL {
    public class TourContext {
        private string ConnectionString;
        public NpgsqlConnection SqlConnection;

        class connModel {
            public string? host { get; set; }
            public string? username { get; set; }
            public string? password { get; set; }
            public string? database { get; set; }
        }

        public TourContext() {
            StreamReader streamReader = new StreamReader("../../SWEN2-DB/dbconn.json");
            string jsonString = streamReader.ReadToEnd();
            connModel conn = JsonConvert.DeserializeObject<connModel>(jsonString);
            ConnectionString = "Host=" + conn.host + ";Username=" + conn.username + ";Password=" + conn.password + ";Database=" + conn.database;
            SqlConnection = new NpgsqlConnection(ConnectionString);
            //SqlConnection.Open();
        }

        ~TourContext() {
            //SqlConnection.Close();
        }

        public void LoadTours(Tours tours) {
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("select * from tour", SqlConnection);
                npgsqlCommand.Prepare();
                NpgsqlDataReader reader = npgsqlCommand.ExecuteReader();

                while (reader.Read()) {
                    tours.AddTour(new Tour(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDouble(5), reader.GetString(6), reader.GetString(7), reader.GetString(8)));
                }

                reader.Close();
                SqlConnection.Close();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void SaveTour(Tour tour) {
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("insert into tour(name, description, startpoint, endpoint, transportType, distance, tourTime, info, imageLocation) " +
                                                "values(@name, @description, @startpoint, @endpoint, @transportType, @distance, @tourTime, @info, @imageLocation)", SqlConnection);
                npgsqlCommand.Parameters.AddWithValue("name", tour.Name);
                npgsqlCommand.Parameters.AddWithValue("description", tour.Description);
                npgsqlCommand.Parameters.AddWithValue("startpoint", tour.From);
                npgsqlCommand.Parameters.AddWithValue("endpoint", tour.To);
                npgsqlCommand.Parameters.AddWithValue("transportType", tour.TransportType);
                npgsqlCommand.Parameters.AddWithValue("distance", tour.Distance);
                npgsqlCommand.Parameters.AddWithValue("tourTime", tour.Time);
                npgsqlCommand.Parameters.AddWithValue("info", tour.Info);
                npgsqlCommand.Parameters.AddWithValue("imageLocation", tour.ImageLocation);
                npgsqlCommand.Prepare();
                var result = npgsqlCommand.ExecuteNonQuery();
                Console.WriteLine(result);
                SqlConnection.Close();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

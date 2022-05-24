using Newtonsoft.Json;
using Npgsql;
using SWEN2_Tourplanner_Models;

namespace SWEN2_REST.DAL
{
    public class TourContext {
        private string ConnectionString;
        public NpgsqlConnection SqlConnection;
        private readonly ILogger<TourContext> _logger;

        class connModel {
            public string? host { get; set; }
            public string? username { get; set; }
            public string? password { get; set; }
            public string? database { get; set; }
        }

        public TourContext(ILogger<TourContext> logger, Tours tours) {
            _logger = logger;

            StreamReader streamReader = new StreamReader("../../SWEN2-DB/dbconn.json");
            string jsonString = streamReader.ReadToEnd();
            connModel conn = JsonConvert.DeserializeObject<connModel>(jsonString);
            ConnectionString = "Host=" + conn.host + ";Username=" + conn.username + ";Password=" + conn.password + ";Database=" + conn.database;
            SqlConnection = new NpgsqlConnection(ConnectionString);

            LoadTours(tours);
        }

        public void LoadTours(Tours tours) {
            _logger.LogInformation("Loading tours");
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("select * from tour", SqlConnection);
                npgsqlCommand.Prepare();
                NpgsqlDataReader reader = npgsqlCommand.ExecuteReader();

                while (reader.Read()) {
                    tours.AddTour(new Tour(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetDouble(5), reader.GetString(6), reader.GetString(7), reader.GetString(8)));
                }

                reader.Close();

                npgsqlCommand = new("select * from tourLog", SqlConnection);
                npgsqlCommand.Prepare();
                reader = npgsqlCommand.ExecuteReader();

                while (reader.Read()) {
                    tours.GetTour(reader.GetString(0)).AddLog(new TourLog(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6)));
                }

                reader.Close();
                SqlConnection.Close();
            } catch (Exception ex) {
                _logger.LogError("Error while loading tours from database: " + ex);
                Console.WriteLine(ex.Message);
            }
        }

        public int SaveTour(Tour tour) {
            _logger.LogInformation("Saving tour " + tour.ToString());
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
                SqlConnection.Close();
                return 0;
            } catch (Exception ex) {
                _logger.LogError("Error while saving tours to database: " + ex);
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public int SaveTourLog(TourLog tourLog, int logID) {
            _logger.LogInformation("Saving tour log " + tourLog.ToString());
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("insert into tourLog(tourname, tourdate, tourcomment, difficulty, tourtime, rating, logID) " +
                                                "values(@tourname, @tourdate, @tourcomment, @difficulty, @tourtime, @rating, @logID)", SqlConnection);
                npgsqlCommand.Parameters.AddWithValue("tourname", tourLog.Tourname);
                npgsqlCommand.Parameters.AddWithValue("tourdate", tourLog.Date);
                npgsqlCommand.Parameters.AddWithValue("tourcomment", tourLog.Comment);
                npgsqlCommand.Parameters.AddWithValue("difficulty", tourLog.Difficulty);
                npgsqlCommand.Parameters.AddWithValue("tourtime", tourLog.Time);
                npgsqlCommand.Parameters.AddWithValue("rating", tourLog.Rating);
                npgsqlCommand.Parameters.AddWithValue("logID", logID);
                npgsqlCommand.Prepare();
                var result = npgsqlCommand.ExecuteNonQuery();
                SqlConnection.Close();
                return 0;
            } catch (Exception ex) {
                _logger.LogError("Error while saving tour log to database: " + ex);
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public int UpdateTour(string name, Tour tour) {
            _logger.LogInformation("Updating tour " + name);
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("update tour set (name, description, startpoint, endpoint, transportType, distance, tourTime, info, imageLocation) " +
                                                "= (@name, @description, @startpoint, @endpoint, @transportType, @distance, @tourTime, @info, @imageLocation)" +
                                                "where name = @oldname", SqlConnection);
                npgsqlCommand.Parameters.AddWithValue("name", tour.Name);
                npgsqlCommand.Parameters.AddWithValue("description", tour.Description);
                npgsqlCommand.Parameters.AddWithValue("startpoint", tour.From);
                npgsqlCommand.Parameters.AddWithValue("endpoint", tour.To);
                npgsqlCommand.Parameters.AddWithValue("transportType", tour.TransportType);
                npgsqlCommand.Parameters.AddWithValue("distance", tour.Distance);
                npgsqlCommand.Parameters.AddWithValue("tourTime", tour.Time);
                npgsqlCommand.Parameters.AddWithValue("info", tour.Info);
                npgsqlCommand.Parameters.AddWithValue("imageLocation", tour.ImageLocation);
                npgsqlCommand.Parameters.AddWithValue("oldname", name);
                npgsqlCommand.Prepare();
                var result = npgsqlCommand.ExecuteNonQuery();
                SqlConnection.Close();
                return 0;
            } catch (Exception ex) {
                _logger.LogError("Error while updating tour to database: " + ex);
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public int UpdateTourLog(TourLog tourLog, int logID) {
            _logger.LogInformation("Updating tour log " + tourLog.ToString());
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("update tourLog set (tourdate, tourcomment, difficulty, tourtime, rating) " +
                                                "= (@tourdate, @tourcomment, @difficulty, @tourtime, @rating) where logID = @logID and tourname = @tourname", SqlConnection);
                npgsqlCommand.Parameters.AddWithValue("tourdate", tourLog.Date);
                npgsqlCommand.Parameters.AddWithValue("tourcomment", tourLog.Comment);
                npgsqlCommand.Parameters.AddWithValue("difficulty", tourLog.Difficulty);
                npgsqlCommand.Parameters.AddWithValue("tourtime", tourLog.Time);
                npgsqlCommand.Parameters.AddWithValue("rating", tourLog.Rating);
                npgsqlCommand.Parameters.AddWithValue("logID", logID);
                npgsqlCommand.Parameters.AddWithValue("tourname", tourLog.Tourname);
                npgsqlCommand.Prepare();
                var result = npgsqlCommand.ExecuteNonQuery();
                SqlConnection.Close();
                return 0;
            } catch (Exception ex) {
                _logger.LogError("Error while updating tour log to database: " + ex);
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public int DeleteTour(string name) {
            _logger.LogInformation("Deleting tour " + name);
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("delete from tourLog where tourname = @name", SqlConnection);
                npgsqlCommand.Parameters.AddWithValue("name", name);
                npgsqlCommand.Prepare();
                var result = npgsqlCommand.ExecuteNonQuery();

                npgsqlCommand = new("delete from tour where name = @name", SqlConnection);
                npgsqlCommand.Parameters.AddWithValue("name", name);
                npgsqlCommand.Prepare();
                result = npgsqlCommand.ExecuteNonQuery();
                SqlConnection.Close();
                return 0;
            } catch (Exception ex) {
                _logger.LogError("Error while deleting tour from database: " + ex);
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public int DeleteTourLog(int id) {
            _logger.LogInformation("Deleting tourlog " + id);
            try {
                SqlConnection.Open();
                NpgsqlCommand npgsqlCommand = new("delete from tourLog where logID = @id", SqlConnection);
                npgsqlCommand.Parameters.AddWithValue("id", id);
                npgsqlCommand.Prepare();
                var result = npgsqlCommand.ExecuteNonQuery();

                return 0;
            } catch (Exception ex) {
                _logger.LogError("Error while deleting tourlog from database: " + ex);
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}

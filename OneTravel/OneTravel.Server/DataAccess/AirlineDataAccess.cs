using Microsoft.Data.Sqlite;
using OneTravel.Server.Models;

namespace OneTravel.Server.DataAccess
{
    public class AirlineDataAccess
    {
        private const string ConnectionString = "Data Source=fake_travel.db";

        public List<AirlineModel> GetAirlines()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var getAirlinesCommand = connection.CreateCommand();
                getAirlinesCommand.CommandText = "SELECT id, airline FROM airlines";

                using (var reader = getAirlinesCommand.ExecuteReader())
                {
                    var airlines = new List<AirlineModel>();
                    while (reader.Read())
                    {
                        var airline = new AirlineModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        airlines.Add(airline);
                    }
                    return airlines;
                }
            }
        }

        public List<AirlineModel> SearchAirlines(string airlineName)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var searchCommand = connection.CreateCommand();
                searchCommand.CommandText = @"
                    SELECT id, airline 
                    FROM airlines 
                    WHERE airline LIKE @airline";

                searchCommand.Parameters.AddWithValue("@airline", "%" + airlineName + "%");

                using (var reader = searchCommand.ExecuteReader())
                {
                    var airlines = new List<AirlineModel>();
                    while (reader.Read())
                    {
                        var airline = new AirlineModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        airlines.Add(airline);
                    }
                    return airlines;
                }
            }
        }
    }
}
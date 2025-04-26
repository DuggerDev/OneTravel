using Microsoft.Data.Sqlite;
using OneTravel.Server.Controllers;
using OneTravel.Server.Models;

namespace OneTravel.Server.DataAccess
{
    public class AirportDataAccess
    {
        private const string ConnectionString = "Data Source=fake_travel.db";

        public List<AirportModel> GetAllAirports()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var getAirportsCommand = connection.CreateCommand();
                getAirportsCommand.CommandText = @"
                SELECT a.id,
                       a.name,
                       a.iata_code,
                       c.name AS city,
                       c2.name AS country
                FROM airports a
                JOIN cities c ON c.id = a.city_id
                JOIN countries c2 ON c2.id = c.country_id";

                using (var reader = getAirportsCommand.ExecuteReader())
                {
                    var airports = new List<AirportModel>();
                    while (reader.Read())
                    {
                        var airport = new AirportModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IataCode = reader.GetString(2),
                            City = reader.GetString(3),
                            Country = reader.GetString(4)
                        };
                        airports.Add(airport);
                    }
                    return airports;
                }
            }
        }

        public List<AirportModel> SearchAirports(AirportRequest request)
        {
            request.Name = request.Name?.Replace("*", "%");
            request.IataCode = request.IataCode?.Replace("*", "%");
            request.City = request.City?.Replace("*", "%");
            request.Country = request.Country?.Replace("*", "%");

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var airportsCommand = connection.CreateCommand();

                airportsCommand.CommandText = @"
               SELECT a.id AS id,
                      a.name AS name,
                      a.iata_code AS iata_code,
                      c.name as city,
                      c2.name as country
               FROM airports a
               JOIN cities c ON c.id = a.city_id
               JOIN countries c2 ON c2.id = c.country_id
               WHERE a.name LIKE @name
                 AND a.iata_code LIKE @iata
                 AND c.name LIKE @city
                 AND c2.name LIKE @country";

                airportsCommand.Parameters.AddWithValue("@name", "%" + request.Name + "%");
                airportsCommand.Parameters.AddWithValue("@iata", "%" + request.IataCode + "%");
                airportsCommand.Parameters.AddWithValue("@city", "%" + request.City + "%");
                airportsCommand.Parameters.AddWithValue("@country", "%" + request.Country + "%");

                using (var reader = airportsCommand.ExecuteReader())
                {
                    var airports = new List<AirportModel>();
                    while (reader.Read())
                    {
                        var airportResult = new AirportModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IataCode = reader.GetString(2),
                            City = reader.GetString(3),
                            Country = reader.GetString(4)
                        };
                        airports.Add(airportResult);
                    }
                    return airports;
                }
            }
        }
    }
}
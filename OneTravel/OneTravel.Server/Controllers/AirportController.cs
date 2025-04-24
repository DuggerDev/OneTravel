using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using OneTravel.Server.Models;


namespace OneTravel.Server.Controllers
{
    public class AirportRequest
    {
        public string? Name { get; set; }
        public string? IataCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class AirportController : ControllerBase
    {
        [HttpGet(Name ="GetAirports")]
        public IEnumerable<Airport> Get()
        {
            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
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
                    var airports = new List<Airport>();
                    while (reader.Read())
                    {
                        var airport = new Airport
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IataCode = reader.GetString(2),
                            City = reader.GetString(3),
                            Country = reader.GetString(4)
                        };
                        airports.Add(airport);
                    }
                    connection.Close();
                    return airports;
                }
            }
        }

        [HttpPost(Name = "SearchAirports")]
        public IEnumerable<Airport> Post([FromBody] AirportRequest airportRequest)
        {
            airportRequest.Name = airportRequest.Name?.Replace("*", "%");
            airportRequest.IataCode = airportRequest.IataCode?.Replace("*", "%");
            airportRequest.City = airportRequest.City?.Replace("*", "%");
            airportRequest.Country = airportRequest.Country?.Replace("*", "%");

            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
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
                     AND c2.name LIKE @country
                 ";

                airportsCommand.Parameters.AddWithValue("@name", "%" + airportRequest.Name + "%");
                airportsCommand.Parameters.AddWithValue("@iata", "%" + airportRequest.IataCode + "%");
                airportsCommand.Parameters.AddWithValue("@city", "%" + airportRequest.City + "%");
                airportsCommand.Parameters.AddWithValue("@country", "%" + airportRequest.Country + "%");

                using (var reader = airportsCommand.ExecuteReader())
                {
                    var airports = new List<Airport>();
                    while (reader.Read())
                    {
                        var airportResult = new Airport
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            IataCode = reader.GetString(2),
                            City = reader.GetString(3),
                            Country = reader.GetString(4)
                        };
                        airports.Add(airportResult);
                    }
                    
                    connection.Close();
                    return airports;
                }
            }
        }
    }
}

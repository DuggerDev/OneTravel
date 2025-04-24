using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using OneTravel.Server.Models;

namespace OneTravel.Server.Controllers
{
    public class AirlineRequest
    {
        public string? Airline { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class AirlineController : ControllerBase
    {
        [HttpGet(Name = "GetAirlines")]
        public IEnumerable<Airline> Get()
        {
            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
            {
                connection.Open();
                var getArlinesCommand = connection.CreateCommand();
                getArlinesCommand.CommandText = "SELECT * FROM airlines";
                using (var reader = getArlinesCommand.ExecuteReader())
                {
                    var airlines = new List<Airline>();
                    while (reader.Read())
                    {
                        var airline = new Airline
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        airlines.Add(airline);
                    }
                    connection.Close();
                    return airlines;
                }
            }
        }

        [HttpPost(Name = "SearchAirline")]
        public IEnumerable<Airline> Post([FromBody] AirlineRequest airlineRequest)
        {
            airlineRequest.Airline = airlineRequest.Airline?.Replace("*", "%");
            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
            {
                connection.Open();
                var getArlinesCommand = connection.CreateCommand();
                getArlinesCommand.CommandText = @"
                    SELECT * 
                    FROM airlines 
                    WHERE airline LIKE @airline";
                getArlinesCommand.Parameters.AddWithValue("@search", "%" + airlineRequest.Airline + "%");
                using (var reader = getArlinesCommand.ExecuteReader())
                {
                    var airlines = new List<Airline>();
                    while (reader.Read())
                    {
                        var airline = new Airline
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        airlines.Add(airline);
                    }
                    connection.Close();
                    return airlines;
                }
            }
        }
    }
}

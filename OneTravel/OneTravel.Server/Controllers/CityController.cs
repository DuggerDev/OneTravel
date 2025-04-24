using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using OneTravel.Server.Models;

namespace OneTravel.Server.Controllers
{
    public class CityRequest
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        [HttpGet(Name = "GetCities")]
        public IEnumerable<City> Get()
        {
            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
            {
                connection.Open();
                var getCitiesCommand = connection.CreateCommand();
                getCitiesCommand.CommandText = @"
                    SELECT c.id as id, 
                           c.name as name, 
                           c2.name as country 
                    FROM cities c 
                    JOIN countries c2 on c.country_id = c2.id";
                using (var reader = getCitiesCommand.ExecuteReader())
                {
                    var cities = new List<City>();
                    while (reader.Read())
                    {
                        var city = new City
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Country = reader.GetString(2)
                        };
                        cities.Add(city);
                    }
                    connection.Close();
                    return cities;
                }
            }
        }

        [HttpPost(Name = "SearchCities")]
        public IEnumerable<City> Post([FromBody] CityRequest cityRequest)
        {
            cityRequest.Name = cityRequest.Name?.Replace("*", "%");
            cityRequest.Country = cityRequest.Country?.Replace("*", "%");

            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
            {
                connection.Open();
                var citiesCommand = connection.CreateCommand();

                citiesCommand.CommandText = @"
                    SELECT c.id as id, 
                           c.name as name, 
                           c2.name as country 
                    FROM cities c 
                    JOIN countries c2 on c.country_id = c2.id
                    WHERE c.name LIKE @name
                      and c2.name LIKE @country";

                citiesCommand.Parameters.AddWithValue("@name", "%" + cityRequest.Name + "%");
                citiesCommand.Parameters.AddWithValue("@country", "%" + cityRequest.Country + "%");

                using (var reader = citiesCommand.ExecuteReader())
                {
                    var cities = new List<City>();
                    while (reader.Read())
                    {
                        var cityResult = new City
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Country = reader.GetString(2)
                        };
                        cities.Add(cityResult);
                    }
                    connection.Close();
                    return cities;
                }
            }
        }
    }
}

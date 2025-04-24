using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using OneTravel.Server.Models;

namespace OneTravel.Server.Controllers
{
    
    public class CountryRequest
    {
        public string? Name { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        [HttpGet(Name = "GetCountries")]
        public IEnumerable<Country> Get()
        {
            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
            {
                connection.Open();
                var getCountriesCommand = connection.CreateCommand();
                getCountriesCommand.CommandText = "SELECT * FROM countries";
                using (var reader = getCountriesCommand.ExecuteReader())
                {
                    var countries = new List<Country>();
                    while (reader.Read())
                    {
                        var country = new Country
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        countries.Add(country);
                    }
                    connection.Close();
                    return countries;
                }
            }
        }

        [HttpPost(Name = "SearchCountries")]
        public IEnumerable<Country> Post([FromBody] CountryRequest countryRequest)
        {
            countryRequest.Name = countryRequest.Name?.Replace("*", "%");
            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
            {
                connection.Open();
                var getCountriesCommand = connection.CreateCommand();
                getCountriesCommand.CommandText = @"
                    SELECT * 
                    FROM countries 
                    WHERE id
                    AND name LIKE @name";
                getCountriesCommand.Parameters.AddWithValue("@name", "%" + countryRequest.Name + "%");
                using (var reader = getCountriesCommand.ExecuteReader())
                {
                    var countries = new List<Country>();
                    while (reader.Read())
                    {
                        var country = new Country
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        countries.Add(country);
                    }
                    connection.Close();
                    return countries;
                }
            }
        }
    }
}

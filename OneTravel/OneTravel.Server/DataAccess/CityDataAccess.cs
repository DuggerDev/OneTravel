using Microsoft.Data.Sqlite;
using OneTravel.Server.Controllers;
using OneTravel.Server.Models;

namespace OneTravel.Server.DataAccess
{
    public class CityDataAccess
    {
        private const string ConnectionString = "Data Source=fake_travel.db";

        public List<CityModel> GetAllCities()
        {
            using (var connection = new SqliteConnection(ConnectionString))
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
                    var cities = new List<CityModel>();
                    while (reader.Read())
                    {
                        var city = new CityModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Country = reader.GetString(2)
                        };
                        cities.Add(city);
                    }
                    return cities;
                }
            }
        }

        public List<CityModel> SearchCities(CityRequest request)
        {
            request.Name = request.Name?.Replace("*", "%");
            request.Country = request.Country?.Replace("*", "%");

            using (var connection = new SqliteConnection(ConnectionString))
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

                citiesCommand.Parameters.AddWithValue("@name", "%" + request.Name + "%");
                citiesCommand.Parameters.AddWithValue("@country", "%" + request.Country + "%");

                using (var reader = citiesCommand.ExecuteReader())
                {
                    var cities = new List<CityModel>();
                    while (reader.Read())
                    {
                        var cityResult = new CityModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Country = reader.GetString(2)
                        };
                        cities.Add(cityResult);
                    }
                    return cities;
                }
            }
        }
    }
}
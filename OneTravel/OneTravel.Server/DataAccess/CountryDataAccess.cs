using Microsoft.Data.Sqlite;
using OneTravel.Server.Models;
using OneTravel.Server.Controllers;

namespace OneTravel.Server.DataAccess
{
    public class CountryDataAccess
    {
        private const string ConnectionString = "Data Source=fake_travel.db";

        public List<CountryModel> GetAllCountries()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var getCountriesCommand = connection.CreateCommand();
                getCountriesCommand.CommandText = "SELECT * FROM countries";

                using (var reader = getCountriesCommand.ExecuteReader())
                {
                    var countries = new List<CountryModel>();
                    while (reader.Read())
                    {
                        var country = new CountryModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        countries.Add(country);
                    }
                    return countries;
                }
            }
        }

        public List<CountryModel> SearchCountries(CountryRequest request)
        {
            request.Name = request.Name?.Replace("*", "%");

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var getCountriesCommand = connection.CreateCommand();
                getCountriesCommand.CommandText = @"
                    SELECT * 
                    FROM countries 
                    WHERE name LIKE @name";

                getCountriesCommand.Parameters.AddWithValue("@name", "%" + request.Name + "%");

                using (var reader = getCountriesCommand.ExecuteReader())
                {
                    var countries = new List<CountryModel>();
                    while (reader.Read())
                    {
                        var country = new CountryModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };
                        countries.Add(country);
                    }
                    return countries;
                }
            }
        }
    }
}
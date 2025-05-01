using Microsoft.Data.Sqlite;
using OneTravel.Server.Controllers;
using OneTravel.Server.Models;

namespace OneTravel.Server.DataAccess
{
    public class HotelDataAccess
    {
        private const string ConnectionString = "Data Source=fake_travel.db";

        public List<HotelModel> SearchHotels(HotelRequest request)
        {
            request.Name = request.Name?.Replace("*", "%");
            request.City = request.City?.Replace("*", "%");
            request.Country = request.Country?.Replace("*", "%");

            if (request.CheckInDate == null)
            {
                request.CheckInDate = DateTime.Now;
            }
            if (request.CheckOutDate == null)
            {
                request.CheckOutDate = request.CheckInDate?.AddDays(14);
            }

            if (request.Pages == null)
            {
                request.Pages = 0;
            }

            // Price left to frontend filtering

            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var hotelCommand = connection.CreateCommand();

                hotelCommand.CommandText = @"
                    SELECT h.id,
                           h.name,
                           c.name,
                           c2.name,
                           h.check_in_date,
                           h.check_out_date,
                           h.price
                    FROM hotels h
                    JOIN cities c ON c.id = h.city_id
                    JOIN countries c2 ON c2.id = c.country_id
                    WHERE h.name LIKE @name
                      AND c.name LIKE @city
                      AND c2.name LIKE @country
                      AND h.check_in_date >= @checkin
                      AND h.check_out_date <= @checkout
                    ORDER BY h.check_in_date
                    limit 25 offset @pages
                ";

                hotelCommand.Parameters.AddWithValue("@name", "%" + request.Name + "%");
                hotelCommand.Parameters.AddWithValue("@city", "%" + request.City + "%");
                hotelCommand.Parameters.AddWithValue("@country", "%" + request.Country + "%");
                hotelCommand.Parameters.AddWithValue("@checkin", request.CheckInDate);
                hotelCommand.Parameters.AddWithValue("@checkout", request.CheckOutDate);
                hotelCommand.Parameters.AddWithValue("@pages", request.Pages * 25);

                using (var reader = hotelCommand.ExecuteReader())
                {
                    var hotels = new List<HotelModel>();
                    while (reader.Read())
                    {
                        var hotelResult = new HotelModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            City = reader.GetString(2),
                            Country = reader.GetString(3),
                            CheckInDate = reader.GetDateTime(4),
                            CheckOutDate = reader.GetDateTime(5),
                            Price = reader.GetFloat(6)
                        };
                        hotels.Add(hotelResult);
                    }
                    return hotels;
                }
            }
        }
    }
}
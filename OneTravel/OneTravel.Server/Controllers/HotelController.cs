using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using OneTravel.Server.Models;

namespace OneTravel.Server.Controllers
{

    public class HotelRequest
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public float? Price { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        [HttpPost(Name = "SearchHotels")]
        public IEnumerable<Hotel> Post([FromBody] HotelRequest hotelRequest)
        {
            hotelRequest.Name = hotelRequest.Name?.Replace("*", "%");
            hotelRequest.City = hotelRequest.City?.Replace("*", "%");
            hotelRequest.Country = hotelRequest.Country?.Replace("*", "%");
            if ( hotelRequest.CheckInDate == null)
            {
                hotelRequest.CheckInDate = DateTime.Now;
            }
            if ( hotelRequest.CheckOutDate == null)
            {
                hotelRequest.CheckOutDate = hotelRequest.CheckInDate?.AddDays(14);
            }
            // Leave Price to frontend too much work

            using (var connection = new SqliteConnection("Data Source=fake_travel.db"))
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
                    JOIN cities c on c.id = h.city_id
                    JOIN countries c2 on c2.id = c.country_id
                    WHERE h.name LIKE @name
                      AND c.name LIKE @city
                      AND c2.name LIKE @country
                      AND h.check_in_date >= @checkin
                      AND h.check_out_date <= @checkout
                    ORDER BY h.check_in_date
                ";

                hotelCommand.Parameters.AddWithValue("@name", "%" + hotelRequest.Name + "%");
                hotelCommand.Parameters.AddWithValue("@city", "%" + hotelRequest.City + "%");
                hotelCommand.Parameters.AddWithValue("@country", "%" + hotelRequest.Country + "%");
                hotelCommand.Parameters.AddWithValue("@checkin", hotelRequest.CheckInDate);
                hotelCommand.Parameters.AddWithValue("@checkout", hotelRequest.CheckOutDate);

                using (var reader = hotelCommand.ExecuteReader())
                {
                    var hotels = new List<Hotel>();
                    while (reader.Read())
                    {
                        var hotelResult = new Hotel
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

                    connection.Close();
                    return hotels;
                }

            }
        }
    }
}

using Microsoft.Data.Sqlite;
using OneTravel.Server.Controllers;
using OneTravel.Server.Models;

namespace OneTravel.Server.DataAccess
{
    public class FlightDataAccess
    {
        private const string ConnectionString = "Data Source=fake_travel.db";

        public List<FlightModel> SearchFlights(FlightRequest request)
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                var getFlightsCommand = connection.CreateCommand();

                request.Airline = request.Airline?.Replace("*", "%");
                request.Origin = request.Origin?.Replace("*", "%");
                request.OriginIata = request.OriginIata?.Replace("*", "%");
                request.OriginCity = request.OriginCity?.Replace("*", "%");
                request.OriginCountry = request.OriginCountry?.Replace("*", "%");
                request.Destination = request.Destination?.Replace("*", "%");
                request.DestinationIata = request.DestinationIata?.Replace("*", "%");
                request.DestinationCity = request.DestinationCity?.Replace("*", "%");
                request.DestinationCountry = request.DestinationCountry?.Replace("*", "%");


                if (request.DepartureTime == null)
                {
                    request.DepartureTime = DateTime.Now;
                }

                if (request.ArrivalTime == null)
                {
                    request.ArrivalTime = DateTime.Now.AddDays(30);
                }

                if (request.Pages == null)
                {
                    request.Pages = 0;
                }

                getFlightsCommand.CommandText = @"
                    SELECT f.id as id,
                           a1.airline as airline,
                           a2.name as origin,
                           a2.iata_code as origin_iata,
                           c1.name as origin_city,
                           c3.name as origin_country,
                           a3.name as destination,
                           a3.iata_code as destination_iata,
                           c2.name as destination_city,
                           c4.name as destination_country,
                           f.departure_time,
                           f.arrival_time,
                           f.stops,
                           f.price
                    FROM flights f
                    JOIN airlines a1 ON a1.id = f.airline_id
                    JOIN airports a2 ON a2.id = f.origin_id
                    JOIN airports a3 ON a3.id = f.destination_id
                    JOIN cities c1 ON c1.id = a2.city_id
                    JOIN cities c2 ON c2.id = a3.city_id
                    JOIN countries c3 ON c3.id = c1.country_id
                    JOIN countries c4 ON c4.id = c2.country_id
                    WHERE a1.airline LIKE @airline
                      AND a2.name LIKE @origin
                      AND a2.iata_code LIKE @origin_iata
                      AND c1.name LIKE @origin_city
                      AND c3.name LIKE @origin_country
                      AND a3.name LIKE @destination
                      AND a3.iata_code LIKE @destination_iata
                      AND c2.name LIKE @destination_city
                      AND c4.name LIKE @destination_country
                      AND f.departure_time >= @departing
                      AND f.arrival_time <= @arrival
                    ORDER BY f.departure_time
                    LIMIT 25 offset @pages";

                getFlightsCommand.Parameters.AddWithValue("@airline", "%" + request.Airline + "%");
                getFlightsCommand.Parameters.AddWithValue("@origin", "%" + request.Origin + "%");
                getFlightsCommand.Parameters.AddWithValue("@origin_iata", "%" + request.OriginIata + "%");
                getFlightsCommand.Parameters.AddWithValue("@origin_city", "%" + request.OriginCity + "%");
                getFlightsCommand.Parameters.AddWithValue("@origin_country", "%" + request.OriginCountry + "%");
                getFlightsCommand.Parameters.AddWithValue("@destination", "%" + request.Destination + "%");
                getFlightsCommand.Parameters.AddWithValue("@destination_iata", "%" + request.DestinationIata + "%");
                getFlightsCommand.Parameters.AddWithValue("@destination_city", "%" + request.DestinationCity + "%");
                getFlightsCommand.Parameters.AddWithValue("@destination_country", "%" + request.DestinationCountry + "%");
                getFlightsCommand.Parameters.AddWithValue("@departing", request.DepartureTime);
                getFlightsCommand.Parameters.AddWithValue("@arrival", request.ArrivalTime);
                getFlightsCommand.Parameters.AddWithValue("@pages", request.Pages * 25);

                using (var reader = getFlightsCommand.ExecuteReader())
                {
                    var flights = new List<FlightModel>();
                    while (reader.Read())
                    {
                        var flightResult = new FlightModel
                        {
                            Id = reader.GetInt32(0),
                            Airline = reader.GetString(1),
                            Origin = reader.GetString(2),
                            OriginIata = reader.GetString(3),
                            OriginCity = reader.GetString(4),
                            OriginCountry = reader.GetString(5),
                            Destination = reader.GetString(6),
                            DestinationIata = reader.GetString(7),
                            DestinationCity = reader.GetString(8),
                            DestinationCountry = reader.GetString(9),
                            Departing = reader.GetDateTime(10),
                            Arrival = reader.GetDateTime(11),
                            Stops = reader.GetInt32(12),
                            Price = reader.GetFloat(13)
                        };
                        flights.Add(flightResult);
                    }
                    return flights;
                }
            }
        }
    }
}

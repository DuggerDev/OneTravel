using Microsoft.AspNetCore.Mvc;
using OneTravel.Server.Models;
using OneTravel.Server.DataAccess;

namespace OneTravel.Server.Controllers
{

    public class FlightRequest
    {
        public string? Airline { get; set; }
        public string? Origin { get; set; }
        public string? OriginIata { get; set; }
        public string? OriginCity { get; set; }
        public string? OriginCountry { get; set; }
        public string? Destination { get; set; }
        public string? DestinationIata { get; set; }
        public string? DestinationCity { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime? DepartureTime { get; set; }
        public DateTime? ArrivalTime { get; set; }

        public int? Stops { get; set; }
        public float? Price { get; set; }
        public int? Pages { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class FlightController
    {
        private readonly FlightDataAccess _flightDataAccess;

        public FlightController()
        {
            _flightDataAccess = new FlightDataAccess();
        }

        [HttpPost(Name = "SearchFlights")]
        public IEnumerable<FlightModel> Post([FromBody] FlightRequest flightRequest)
        {
            return _flightDataAccess.SearchFlights(flightRequest);
        }
    }
}

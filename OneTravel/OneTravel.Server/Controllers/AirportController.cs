using Microsoft.AspNetCore.Mvc;
using OneTravel.Server.DataAccess;
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
        private readonly AirportDataAccess _airportDataAccess;

        public AirportController()
        {
            _airportDataAccess = new AirportDataAccess();
        }

        [HttpGet(Name = "GetAirports")]
        public List<AirportModel> GetAirports()
        {
            return _airportDataAccess.GetAllAirports();
        }

        [HttpPost(Name = "SearchAirports")]
        public List<AirportModel> SearchAirports([FromBody] AirportRequest airportRequest)
        {
            return _airportDataAccess.SearchAirports(airportRequest);
        }
    }
}
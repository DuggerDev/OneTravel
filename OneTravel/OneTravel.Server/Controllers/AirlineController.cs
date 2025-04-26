using Microsoft.AspNetCore.Mvc;
using OneTravel.Server.DataAccess;
using OneTravel.Server.Models;

namespace OneTravel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirlineController : ControllerBase
    {
        private readonly AirlineDataAccess _airlineDataAccess;

        public AirlineController()
        {
            _airlineDataAccess = new AirlineDataAccess();
        }

        [HttpGet(Name = "GetAirlines")]
        public List<AirlineModel> GetAirlines()
        {
            return _airlineDataAccess.GetAirlines();
        }

        [HttpPost(Name = "SearchAirline")]
        public List<AirlineModel> SearchAirlines([FromBody] string? requestedAirline)
        {
            if (string.IsNullOrWhiteSpace(requestedAirline))
            {
                return new List<AirlineModel>();
            }

            requestedAirline = requestedAirline.Replace("*", "%");
            return _airlineDataAccess.SearchAirlines(requestedAirline);
        }
    }
}
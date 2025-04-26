using Microsoft.AspNetCore.Mvc;
using OneTravel.Server.Models;
using OneTravel.Server.DataAccess;

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
        private readonly CityDataAccess _cityDataAccess;

        public CityController()
        {
            _cityDataAccess = new CityDataAccess();
        }

        [HttpGet(Name = "GetCities")]
        public IEnumerable<CityModel> GetCities()
        {
            return _cityDataAccess.GetAllCities();
        }

        [HttpPost(Name = "SearchCities")]
        public IEnumerable<CityModel> SearchCities([FromBody] CityRequest cityRequest)
        {
            return _cityDataAccess.SearchCities(cityRequest);
        }
    }
}
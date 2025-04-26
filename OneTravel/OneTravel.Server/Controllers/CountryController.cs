using Microsoft.AspNetCore.Mvc;
using OneTravel.Server.Models;
using OneTravel.Server.DataAccess;

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
        private readonly CountryDataAccess _countryDataAccess;

        public CountryController()
        {
            _countryDataAccess = new CountryDataAccess();
        }

        [HttpGet(Name = "GetCountries")]
        public IEnumerable<CountryModel> GetCountries()
        {
            return _countryDataAccess.GetAllCountries();
        }

        [HttpPost(Name = "SearchCountries")]
        public IEnumerable<CountryModel> SearchCountries([FromBody] CountryRequest countryRequest)
        {
            return _countryDataAccess.SearchCountries(countryRequest);
        }
    }
}
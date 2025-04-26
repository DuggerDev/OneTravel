using Microsoft.AspNetCore.Mvc;
using OneTravel.Server.Models;
using OneTravel.Server.DataAccess;

namespace OneTravel.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastDataAccess _weatherDataAccess;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            _weatherDataAccess = new WeatherForecastDataAccess();
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastModel> Get()
        {
            return _weatherDataAccess.GetWeatherForecasts();
        }
    }
}
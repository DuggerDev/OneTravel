using Microsoft.AspNetCore.Mvc;
using OneTravel.Server.Models;
using OneTravel.Server.DataAccess;

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

        public int? Pages { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly HotelDataAccess _hotelDataAccess;

        public HotelController()
        {
            _hotelDataAccess = new HotelDataAccess();
        }

        [HttpPost(Name = "SearchHotels")]
        public IEnumerable<HotelModel> Post([FromBody] HotelRequest hotelRequest)
        {
            return _hotelDataAccess.SearchHotels(hotelRequest);
        }
    }
}
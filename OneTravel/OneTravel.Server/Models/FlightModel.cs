namespace OneTravel.Server.Models
{
    public class FlightModel
    {
        public int Id { get; set; }
        public string? Airline { get; set; }
        public string? Origin { get; set; }
        public string? OriginIata { get; set; }
        public string? OriginCity { get; set; }
        public string? OriginCountry { get; set; }

        public string? Destination { get; set; }
        public string? DestinationIata { get; set; }
        public string? DestinationCity { get; set; }
        public string? DestinationCountry { get; set; }
        public DateTime? Departing { get; set; }
        public DateTime? Arrival { get; set; }

        public int? Stops { get; set; }
        public float? Price { get; set; }
    }
}
﻿namespace OneTravel.Server.Models
{
    public class AirportModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? IataCode { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

    }
}
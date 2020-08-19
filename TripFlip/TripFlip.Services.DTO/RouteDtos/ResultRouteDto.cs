using System;

namespace TripFlip.Services.DTO.RouteDtos
{
    public class ResultRouteDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int TripId { get; set; }
    }
}

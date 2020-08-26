using System;

namespace TripFlip.Services.Dto.RouteDtos
{
    public class RouteDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int TripId { get; set; }
    }
}

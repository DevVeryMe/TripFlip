﻿namespace TripFlip.Services.Dto.RouteDtos
{
    public class UpdateRouteDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int TripId { get; set; }
    }
}

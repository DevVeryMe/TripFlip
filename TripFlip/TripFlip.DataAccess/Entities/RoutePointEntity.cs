﻿using System;

namespace TripFlip.DataAccess.Entities
{
    public class RoutePointEntity
    {
        public int Id { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public int Order { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int RouteId { get; set; }

    }
}
using System;
using System.Collections.Generic;

namespace TripFlip.DataAccess.Entities
{
    public class RouteEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int TripId { get; set; }

        public TripEntity Trip { get; set; }

        public ICollection<RoutePointEntity> RoutePoints { get; set; } 
    }
}

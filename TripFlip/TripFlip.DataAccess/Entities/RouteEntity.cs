using System;

namespace TripFlip.DataAccess.Entities
{
    public class RouteEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int TripId { get; set; }
    }
}

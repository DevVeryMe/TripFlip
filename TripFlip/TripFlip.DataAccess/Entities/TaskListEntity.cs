using System;

namespace TripFlip.DataAccess.Entities
{
    public class TaskListEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int RouteId { get; set; }

        public DateTimeOffset DateCreated { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TripFlip.Domain.Entities.Entities
{
    public class RouteEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int TripId { get; set; }

        public TripEntity Trip { get; set; }

        public ICollection<RoutePointEntity> RoutePoints { get; set; }

        public ICollection<ItemListEntity> ItemLists { get; set; }

        public ICollection<TaskListEntity> TaskLists { get; set; }
    }
}

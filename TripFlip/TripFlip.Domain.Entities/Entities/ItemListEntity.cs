using System;
using System.Collections.Generic;

namespace TripFlip.Domain.Entities.Entities
{
    public class ItemListEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int RouteId { get; set; }

        public RouteEntity Route { get; set; }

        public ICollection<ItemEntity> Items { get; set; }
    }
}

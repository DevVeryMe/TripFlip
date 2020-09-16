using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class RouteRoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<RouteSubscriberRoleEntity> RouteSubscribers { get; set; }
    }
}

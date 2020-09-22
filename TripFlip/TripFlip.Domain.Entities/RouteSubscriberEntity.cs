using System;
using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class RouteSubscriberEntity
    {
        public int Id { get; set; }

        public int RouteId { get; set; }
        public RouteEntity Route { get; set; }

        public DateTimeOffset DateSubscribed { get; set; }

        public int TripSubscriberId { get; set; }
        public TripSubscriberEntity TripSubscriber { get; set; }

        public ICollection<RouteSubscriberRoleEntity> RouteRoles { get; set; }

        public ICollection<TaskAssigneeEntity> AssignedTasks { get; set; }

        public ICollection<ItemAssigneeEntity> AssignedItems { get; set; }
    }
}

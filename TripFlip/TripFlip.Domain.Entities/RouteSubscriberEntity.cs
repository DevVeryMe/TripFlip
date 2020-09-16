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

        // todo: add navigational property for ItemAssigneeEntity that is still to-be-implemented

        // todo: add navigational property for TaskAssigneeEntity that is still to-be-implemented
    }
}

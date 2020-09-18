using System;
using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class TripSubscriberEntity
    {
        public int Id { get; set; }

        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public int TripId { get; set; }
        public TripEntity Trip { get; set; }

        public DateTimeOffset DateSubscribed { get; set; }

        public ICollection<TripSubscriberRoleEntity> TripRoles { get; set; }

        public ICollection<RouteSubscriberEntity> RouteSubscriptions { get; set; }
    }
}

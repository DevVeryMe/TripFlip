using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class TripRoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TripSubscriberRoleEntity> TripSubscribers { get; set; }
    }
}

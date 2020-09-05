using System;

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
    }
}

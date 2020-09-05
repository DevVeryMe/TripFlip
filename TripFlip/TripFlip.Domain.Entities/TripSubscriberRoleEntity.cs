namespace TripFlip.Domain.Entities
{
    public class TripSubscriberRoleEntity
    {
        public int TripSubscriberId { get; set; }
        public TripSubscriberEntity TripSubscriber { get; set; }

        public int TripRoleId { get; set; }
        public TripRoleEntity TripRole { get; set; }
    }
}

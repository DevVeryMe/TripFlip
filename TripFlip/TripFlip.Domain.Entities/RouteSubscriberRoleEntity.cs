namespace TripFlip.Domain.Entities
{
    public class RouteSubscriberRoleEntity
    {
        public int RouteRoleId { get; set; }
        public RouteRoleEntity RouteRole { get; set; }

        public int RouteSubscriberId { get; set; }
        public RouteSubscriberEntity RouteSubscriber { get; set; }
    }
}

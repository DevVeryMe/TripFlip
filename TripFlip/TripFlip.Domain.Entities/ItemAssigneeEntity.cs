namespace TripFlip.Domain.Entities
{
    public class ItemAssigneeEntity
    {
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }

        public int RouteSubscriberId { get; set; }
        public RouteSubscriberEntity RouteSubscriber { get; set; }
    }
}

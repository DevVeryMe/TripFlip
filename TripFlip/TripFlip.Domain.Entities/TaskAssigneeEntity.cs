namespace TripFlip.Domain.Entities
{
    public class TaskAssigneeEntity
    {
        public int TaskId { get; set; }
        public TaskEntity Task { get; set; }

        public int RouteSubscriberId { get; set; }
        public RouteSubscriberEntity RouteSubscriber { get; set; }
    }
}

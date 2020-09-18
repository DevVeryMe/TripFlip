using System.Collections.Generic;

namespace TripFlip.Services.Dto.TaskDtos
{
    public class TaskAssigneesDto
    {
        public int TaskId { get; set; }

        public IEnumerable<int> RouteSubscriberIds { get; set; }
    }
}

using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.RouteSubscriberDtos;

namespace TripFlip.Services.Dto.TaskDtos
{
    public class TaskWithAssigneesDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool IsCompleted { get; set; }

        public ICollection<RouteSubscriberDto> TaskAssignees { get; set; }
    }
}

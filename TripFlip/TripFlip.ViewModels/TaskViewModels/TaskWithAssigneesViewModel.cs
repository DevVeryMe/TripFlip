using System;
using System.Collections.Generic;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.RouteSubscriberViewModels;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class TaskWithAssigneesViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool IsCompleted { get; set; }

        public ICollection<RouteSubscriberViewModel> TaskAssignees { get; set; }
    }
}

using System;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class TaskWithoutListIdViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool IsCompleted { get; set; }
    }
}

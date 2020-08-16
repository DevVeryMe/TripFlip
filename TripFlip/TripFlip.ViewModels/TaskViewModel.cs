using System;
using System.Collections.Generic;
using System.Text;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public bool isCompleted { get; set; }

        public int TaskListId { get; set; }
    }
}

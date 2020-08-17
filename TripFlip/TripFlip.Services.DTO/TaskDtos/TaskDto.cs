using System;
using System.Collections.Generic;
using System.Text;
using TripFlip.Services.DTO.Enums;

namespace TripFlip.Services.DTO.TaskDtos
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public bool isCompleted { get; set; }

        public int TaskListId { get; set; }
    }
}

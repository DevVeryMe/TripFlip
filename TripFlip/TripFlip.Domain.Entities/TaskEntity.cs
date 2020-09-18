using System;
using System.Collections.Generic;
using TripFlip.Domain.Entities.Enums;

namespace TripFlip.Domain.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool IsCompleted { get; set; }

        public int TaskListId { get; set; }

        public TaskListEntity TaskList { get; set; }

        public ICollection<TaskAssigneeEntity> TaskAssignees { get; set; }
    }
}

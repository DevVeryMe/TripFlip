using System;
using TripFlip.Domain.Entities.Entities.Enums;

namespace TripFlip.Domain.Entities.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool isCompleted { get; set; }

        public int TaskListId { get; set; }

        public TaskListEntity TaskList { get; set; }
    }
}

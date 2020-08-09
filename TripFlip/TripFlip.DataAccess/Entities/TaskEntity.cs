using System;
using TripFlip.DataAccess.Entities.Enums;

namespace TripFlip.DataAccess.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public int TaskListId { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool isCompleted { get; set; }
    }
}

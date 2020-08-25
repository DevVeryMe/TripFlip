using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public bool IsCompleted { get; set; }

        public int TaskListId { get; set; }
    }
}

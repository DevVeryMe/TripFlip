using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class FullTaskViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public bool isCompleted { get; set; }

        public int TaskListId { get; set; }
    }
}

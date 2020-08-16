using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels
{
    public class CreateTaskViewModel
    {
        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public int TaskListId { get; set; }
    }
}

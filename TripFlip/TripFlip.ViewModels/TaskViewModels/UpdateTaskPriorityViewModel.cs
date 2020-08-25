using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Enums;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class UpdateTaskPriorityViewModel
    {
        [Required(ErrorMessage = ErrorConstants.EmptyIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyTaskPriorityFieldError)]
        [EnumDataType(typeof(TaskPriorityLevel), ErrorMessage = ErrorConstants.NotMatchAnyTaskPriorityLevelError)]
        public TaskPriorityLevel PriorityLevel { get; set; }
    }
}

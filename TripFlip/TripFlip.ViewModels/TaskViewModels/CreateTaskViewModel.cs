using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.TaskViewModels
{
    public class CreateTaskViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyDescriptionError)]
        [StringLength(500, ErrorMessage = ErrorConstants.DescriptionLengthError)]
        public string Description { get; set; }

        [EnumDataType(typeof(TaskPriorityLevel), ErrorMessage = ErrorConstants.NotMatchAnyTaskPriorityLevelError)]
        public TaskPriorityLevel PriorityLevel { get; set; }
    }
}

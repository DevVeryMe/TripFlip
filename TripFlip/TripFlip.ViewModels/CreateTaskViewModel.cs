using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyDescriptionError)]
        [StringLength(500, ErrorMessage = ErrorConstants.DescriptionLengthError)]
        public string Description { get; set; }

        [ValidateOptionalEnumField(ErrorMessage = ErrorConstants.NotMatchAnyTaskPriorityLevelError)]
        public TaskPriorityLevel PriorityLevel { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int TaskListId { get; set; }
    }
}

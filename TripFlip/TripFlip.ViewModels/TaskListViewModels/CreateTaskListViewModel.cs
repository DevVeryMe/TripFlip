using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.TaskListViewModels
{
    public class CreateTaskListViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int RouteId { get; set; }
    }
}

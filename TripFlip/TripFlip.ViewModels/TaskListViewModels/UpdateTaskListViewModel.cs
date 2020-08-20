using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.TaskListViewModels
{
    public class UpdateTaskListViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }
    }
}

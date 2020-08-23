using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.ItemViewModels
{
    public class UpdateItemCompletenessViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyIsCompletedFieldError)]
        public bool IsCompleted { get; set; }
    }
}

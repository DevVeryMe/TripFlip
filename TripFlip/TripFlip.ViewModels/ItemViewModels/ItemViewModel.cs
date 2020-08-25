using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.ItemViewModels
{
    public class ItemViewModel
    {
        [Required(ErrorMessage = ErrorConstants.EmptyIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [StringLength(250, ErrorMessage = ErrorConstants.CommentLengthError)]
        public string Comment { get; set; }

        [StringLength(50, ErrorMessage = ErrorConstants.QuantityLengthError)]
        public string Quantity { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyIsCompletedFieldError)]
        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int ItemListId { get; set; }
    }
}

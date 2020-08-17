using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.ItemViewModels
{
    public class CreateItemViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [StringLength(250, ErrorMessage = ErrorConstants.CommentLengthError)]
        public string Comment { get; set; }

        [StringLength(250, ErrorMessage = ErrorConstants.QuantityLengthError)]
        public string Quantity { get; set; }

        [Required(ErrorMessage =  ErrorConstants.ItemWithoutItemListError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int ItemListId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.ItemListViewModels
{
    public class UpdateItemListViewModel
    {
        [Required(ErrorMessage = ErrorConstants.EmptyIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }
    }
}

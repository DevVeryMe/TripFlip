using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels
{
    public class PaginationViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.PageNumberLessThanOneError)]
        public int? PageNumber { get; set; }

        [Range(1, 50, ErrorMessage = ErrorConstants.InvalidPageSizeError)]
        public int? PageSize { get; set; }
    }
}

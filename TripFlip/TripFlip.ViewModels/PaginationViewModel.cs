using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels
{
    public class PaginationViewModel
    {
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.PageNumberLessOneError)]
        public int? PageNumber { get; set; }

        [Range(1, 50, ErrorMessage = ErrorConstants.PageSizeLessOneError)]
        public int? PageSize { get; set; }
    }
}

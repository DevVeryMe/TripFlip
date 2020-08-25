using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.RouteViewModels
{
    /// <summary>
    /// ViewModel that represents the Route to be created
    /// </summary>
    public class CreateRouteViewModel
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleFieldError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyIdFieldError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int TripId { get; set; }
    }
}

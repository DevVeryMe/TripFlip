using System;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.RouteViewModels
{
    /// <summary>
    /// ViewModel that represents the Route to be created
    /// </summary>
    public class CreateRouteViewModel
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [Required(ErrorMessage = ErrorConstants.EmptyIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int TripId { get; set; }
    }
}

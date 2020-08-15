using System;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels
{
    public class CreateRouteViewModel
    {
        [Required (AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [DateIsLessThanNow (ErrorMessage = ErrorConstants.DateIsGreaterThanNowError)]
        public DateTimeOffset DateCreated { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredFieldIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdFieldLessThanOneError)]
        public int TripId { get; set; }
    }
}

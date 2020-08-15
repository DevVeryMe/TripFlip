using System;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels
{
    public class TripViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredFieldIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.RequiredFieldIdError)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyDescriptionError)]
        [StringLength(500, ErrorMessage = ErrorConstants.DescriptionLengthError)]
        public string Description { get; set; }

        [ValidateGreaterThanNow(ErrorMessage = ErrorConstants.StartDateEarlierThanNowError)]
        public DateTimeOffset? StartsAt { get; set; }

        [ValidateGreaterThanStartAt("StartsAt", ErrorMessage = ErrorConstants.EndDateLessThanStartDateError)]
        [ValidateGreaterThanNow(ErrorMessage = ErrorConstants.EndDateEarlierThanNowError)]
        public DateTimeOffset? EndsAt { get; set; }
    }
}

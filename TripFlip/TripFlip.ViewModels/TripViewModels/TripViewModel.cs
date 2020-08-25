using System;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.TripViewModels
{
    public class TripViewModel
    {
        [Required(ErrorMessage = ErrorConstants.EmptyIdFieldError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyTitleFieldError)]
        [StringLength(100, ErrorMessage = ErrorConstants.TitleLengthError)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyDescriptionFieldError)]
        [StringLength(500, ErrorMessage = ErrorConstants.DescriptionLengthError)]
        public string Description { get; set; }

        [ValidateLaterThanNow(ErrorMessage = ErrorConstants.StartDateEarlierThanNowError)]
        public DateTimeOffset? StartsAt { get; set; }

        [ValidateLaterThan("StartsAt", ErrorMessage = ErrorConstants.EndDateEarlierThanStartDateError)]
        [ValidateLaterThanNow(ErrorMessage = ErrorConstants.EndDateEarlierThanNowError)]
        public DateTimeOffset? EndsAt { get; set; }
    }
}

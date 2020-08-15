using System;
using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels
{
    public class TripViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [ValidateGreaterThanNow]
        public DateTimeOffset? StartsAt { get; set; }

        [ValidateGreaterThanStartAt("StartsAt")]
        public DateTimeOffset? EndsAt { get; set; }
    }
}

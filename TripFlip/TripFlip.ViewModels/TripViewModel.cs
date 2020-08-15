using System;

namespace TripFlip.ViewModels
{
    public class TripViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? StartsAt { get; set; }
        public DateTimeOffset? EndsAt { get; set; }
    }
}

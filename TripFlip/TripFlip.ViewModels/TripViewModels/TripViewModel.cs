using System;

namespace TripFlip.ViewModels.TripViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }
    }
}

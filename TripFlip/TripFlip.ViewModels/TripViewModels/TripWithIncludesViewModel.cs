using System;
using System.Collections.Generic;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.ViewModels.UserViewModels;

namespace TripFlip.ViewModels.TripViewModels
{
    public class TripWithIncludesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }

        public ICollection<RouteWithIncludesViewModel> Routes { get; set; }
    }
}

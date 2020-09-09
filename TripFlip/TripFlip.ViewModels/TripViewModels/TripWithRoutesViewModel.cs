﻿using System;
using System.Collections.Generic;
using TripFlip.ViewModels.RouteViewModels;
using TripFlip.ViewModels.TripRoleViewModels;
using TripFlip.ViewModels.UserViewModels;

namespace TripFlip.ViewModels.TripViewModels
{
    public class TripWithRoutesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }

        public ICollection<RouteWithPointsItemAndTaskListsViewModel> Routes { get; set; }

        public ICollection<TripRoleViewModel> TripRoles { get; set; }
    }
}

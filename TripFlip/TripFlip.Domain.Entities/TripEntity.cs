﻿using System;
using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class TripEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }

        public ICollection<RouteEntity> Routes { get; set; }

        public ICollection<TripFileEntity> TripFiles { get; set; }

        public ICollection<TripSubscriberEntity> TripSubscribers { get; set; }
    }
}

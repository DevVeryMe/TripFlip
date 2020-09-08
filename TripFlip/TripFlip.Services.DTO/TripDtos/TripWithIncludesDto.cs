﻿using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.UserDtos;

namespace TripFlip.Services.Dto.TripDtos
{
    public class TripWithIncludesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }

        public ICollection<RouteWithIncludesDto> Routes { get; set; }

        public ICollection<SubscriberWithRolesDto> TripSubscribers { get; set; }
    }
}
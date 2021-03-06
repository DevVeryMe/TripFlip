﻿using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.RouteDtos;
using TripFlip.Services.Dto.TripRoleDtos;

namespace TripFlip.Services.Dto.TripDtos
{
    public class TripWithRoutesAndUserRolesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }

        public ICollection<RouteWithPointsItemAndTaskListsDto> Routes { get; set; }

        public ICollection<TripRoleDto> TripRoles { get; set; }
    }
}
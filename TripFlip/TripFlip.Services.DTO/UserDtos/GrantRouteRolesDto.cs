using System;
using System.Collections.Generic;

namespace TripFlip.Services.Dto.UserDtos
{
    public class GrantRouteRolesDto
    {
        public Guid UserId { get; set; }

        public int RouteId { get; set; }

        public IEnumerable<int> RouteRoleIds { get; set; }
    }
}

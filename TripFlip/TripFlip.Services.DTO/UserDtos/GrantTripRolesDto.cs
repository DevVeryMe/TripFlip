using System;
using System.Collections.Generic;

namespace TripFlip.Services.Dto.UserDtos
{
    public class GrantTripRolesDto
    {
        public int TripId { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<int> TripRoleIds { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace TripFlip.Services.Dto.UserDtos
{
    public class GrantApplicationRolesDto
    {
        public Guid UserId { get; set; }

        public IEnumerable<int> ApplicationRoleIds { get; set; }
    }
}

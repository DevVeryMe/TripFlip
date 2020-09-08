using System.Collections.Generic;
using TripFlip.Services.Dto.TripRoleDtos;

namespace TripFlip.Services.Dto.TripDtos
{
    public class TripWithRolesDto
    {
        public TripWithIncludesDto Trip { get; set; }

        public ICollection<TripRoleDto> TripRoles { get; set; }
    }
}

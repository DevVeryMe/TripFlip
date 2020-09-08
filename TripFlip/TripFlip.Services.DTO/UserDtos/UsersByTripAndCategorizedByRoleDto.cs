using System.Collections.Generic;

namespace TripFlip.Services.Dto.UserDtos
{
    public class UsersByTripAndCategorizedByRoleDto
    {
        public int TripId { get; set; }

        public IEnumerable<UserDto> TripAdmins { get; set; }

        public IEnumerable<UserDto> TripEditors { get; set; }

        public IEnumerable<UserDto> TripGuests { get; set; }
    }
}

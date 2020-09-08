using System;

namespace TripFlip.Domain.Entities
{
    public class ApplicationUserRoleEntity
    {
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }

        public int ApplicationRoleId { get; set; }
        public ApplicationRoleEntity ApplicationRole { get; set; }
    }
}

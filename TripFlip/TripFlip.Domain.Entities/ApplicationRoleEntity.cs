using System.Collections.Generic;

namespace TripFlip.Domain.Entities
{
    public class ApplicationRoleEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ApplicationUserRoleEntity> Users { get; set; }
    }
}
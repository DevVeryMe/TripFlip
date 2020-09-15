using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.UserViewModels
{
    public class GrantApplicationRolesViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredUserIdError)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredRolesArray)]
        public IEnumerable<int> ApplicationRoleIds { get; set; }
    }
}

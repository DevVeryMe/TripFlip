﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.UserViewModels
{
    public class GrantTripRolesViewModel
    {
        [Required(ErrorMessage = ErrorConstants.EmptyIdFieldError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.TripIdLessThanOneError)]
        public int TripId { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredUserIdError)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredRolesArray)]
        public IEnumerable<int> TripRoleIds { get; set; }
    }
}

﻿using System;
using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.UserViewModels
{
    public class GrantSubscriberRoleViewModel
    {
        [Required(ErrorMessage = ErrorConstants.EmptyIdFieldError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.TripIdLessThanOneError)]
        public int TripId { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredUserIdError)]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredRoleIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.RoleIdLessThanOneError)]
        public int TripRoleId { get; set; }
    }
}
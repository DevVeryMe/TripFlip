using System;
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

        [Required(ErrorMessage = ErrorConstants.RequiredRolesArray)]
        [MinLength(1, ErrorMessage = ErrorConstants.PassedArrayIsEmpty)]
        public int[] TripRoleIds { get; set; }
    }
}

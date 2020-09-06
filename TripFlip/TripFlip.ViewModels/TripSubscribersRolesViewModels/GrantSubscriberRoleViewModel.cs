using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.TripSubscribersRolesViewModels
{
    public class GrantSubscriberRoleViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredTripSubscriberIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.TripSubscriberIdLessThanOneError)]
        public int TripSubscriberId { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredRoleIdError)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.RoleIdLessThanOneError)]
        public int RoleId { get; set; }
    }
}

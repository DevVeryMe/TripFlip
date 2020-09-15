using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.UserViewModels
{
    public class ChangeUserPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordFieldError)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = ErrorConstants.PasswordLengthError)]
        [ValidatePasswordHasCorrectFormat(ErrorMessage = ErrorConstants.InvalidPasswordFormatError)]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordFieldError)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = ErrorConstants.PasswordLengthError)]
        [ValidatePasswordHasCorrectFormat(ErrorMessage = ErrorConstants.InvalidPasswordFormatError)]
        public string NewPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordConfirmationFieldError)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = ErrorConstants.PasswordLengthError)]
        [Compare("NewPassword", ErrorMessage = ErrorConstants.MissmatchPasswordConfirmationError)]
        public string NewPasswordConfirmation { get; set; }
    }
}

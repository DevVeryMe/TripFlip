using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.UserViewModels
{
    public class RegisterUserViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyEmailFieldError)]
        [StringLength(320, ErrorMessage = ErrorConstants.EmailLengthError, MinimumLength = 6)]
        [EmailAddress(ErrorMessage = ErrorConstants.EmailNotCorrectFormatError)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordFieldError)]
        [StringLength(100, ErrorMessage = ErrorConstants.PasswordLengthError, MinimumLength = 8)]
        [ValidatePasswordHasCorrectFormat(ErrorMessage = ErrorConstants.PasswordNotCorrectFormatError)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordConfirmationFieldError)]
        [StringLength(100, ErrorMessage = ErrorConstants.PasswordLengthError, MinimumLength = 8)]
        [Compare("Password", ErrorMessage = ErrorConstants.MissmatchPasswordConfirmationError)]
        public string PasswordConfirmation { get; set; }
    }
}

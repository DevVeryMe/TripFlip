using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.UserViewModels
{
    public class RegisterUserViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyEmailFieldError)]
        [StringLength(320, MinimumLength = 6, ErrorMessage = ErrorConstants.EmailLengthError)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$",
            ErrorMessage = ErrorConstants.InvalidEmailFormatError)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordFieldError)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = ErrorConstants.PasswordLengthError)]
        [ValidatePasswordHasCorrectFormat(ErrorMessage = ErrorConstants.InvalidPasswordFormatError)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordConfirmationFieldError)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = ErrorConstants.PasswordLengthError)]
        [Compare("Password", ErrorMessage = ErrorConstants.MissmatchPasswordConfirmationError)]
        public string PasswordConfirmation { get; set; }
    }
}

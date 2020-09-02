using System.ComponentModel.DataAnnotations;

namespace TripFlip.ViewModels.UserViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyEmailFieldError)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,4})+)$", 
            ErrorMessage = ErrorConstants.EmailNotCorrectFormatError)]
        [StringLength(320, MinimumLength = 6, ErrorMessage = ErrorConstants.EmailLengthError)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordFieldError)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = ErrorConstants.PasswordLengthError)]
        public string Password { get; set; }
    }
}

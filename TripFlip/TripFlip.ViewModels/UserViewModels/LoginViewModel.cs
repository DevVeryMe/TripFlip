using System.ComponentModel.DataAnnotations;
using TripFlip.ViewModels.Attributes;

namespace TripFlip.ViewModels.UserViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyEmailFieldError)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", 
            ErrorMessage = ErrorConstants.EmailNotCorrectFormatError)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorConstants.EmptyPasswordFieldError)]
        public string Password { get; set; }
    }
}

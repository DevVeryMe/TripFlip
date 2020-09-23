using System;
using System.Collections.Generic;
using System.Text;
using TripFlip.ViewModels.Enums;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.RegisterUserViewModelTests
{
    public abstract class RegisterUserViewModelTestsBase
    {
        /// <summary>
        /// Creates RegisterUserViewModel object with given
        /// set of parameters.
        /// </summary>
        /// <param name="email">User email.</param>
        /// <param name="password">User password.</param>
        /// <param name="passwordConfirmation">Password confirmation.</param>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="aboutMe">Short information about user.</param>
        /// <param name="gender">User gender.</param>
        /// <param name="birthDate">User birth date.</param>
        /// <returns>Created RegisterUserViewModel object.</returns>
        protected RegisterUserViewModel BuildRegisterUserViewModel(
            string email = "mail@mail.com",
            string password = "TestPassword@1",
            string passwordConfirmation = "TestPassword@1",
            string firstName = null,
            string lastName = null,
            string aboutMe = null,
            int? gender = null,
            DateTimeOffset? birthDate = null)
        {
            var registerUserViewModel = new RegisterUserViewModel()
            {
                Email = email,
                Password = password,
                PasswordConfirmation = passwordConfirmation,
                FirstName = firstName,
                LastName = lastName,
                AboutMe = aboutMe,
                Gender = (UserGender?)gender,
                BirthDate = birthDate
            };

            return registerUserViewModel;
        }
    }
}

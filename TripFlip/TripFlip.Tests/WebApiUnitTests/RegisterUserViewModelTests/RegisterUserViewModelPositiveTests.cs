using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using WebApiUnitTests.Helpers;

namespace WebApiUnitTests.RegisterUserViewModelTests
{
    [TestClass]
    public class RegisterUserViewModelPositiveTests
        : RegisterUserViewModelTestsBase
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestValidEmailData), DynamicDataSourceType.Method)]
        public void Email_Validation_Successful(string displayName,
            string validEmail)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(email: validEmail);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestValidPasswordData), DynamicDataSourceType.Method)]
        public void Password_Validation_Successful(string displayName,
            string validPassword)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(
                    password: validPassword,
                    passwordConfirmation: validPassword);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestValidPasswordConfirmationData),
            DynamicDataSourceType.Method)]
        public void PasswordConfirmation_Validation_Successful(string displayName,
            string validPasswordConfirmation)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(
                    password: validPasswordConfirmation,
                    passwordConfirmation: validPasswordConfirmation);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestValidFirstNameData), DynamicDataSourceType.Method)]
        public void FirstName_Validation_Successful(string displayName,
            string validFirstName)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(firstName: validFirstName);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestValidLastNameData), DynamicDataSourceType.Method)]
        public void LastName_Validation_Successful(string displayName,
            string validLastName)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(lastName: validLastName);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestValidAboutMeData), DynamicDataSourceType.Method)]
        public void AboutMe_Validation_Successful(string displayName,
            string validAboutMe)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(aboutMe: validAboutMe);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidGenderData), DynamicDataSourceType.Method)]
        public void Gender_Validation_Successful(string displayName,
            int? validGender)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(gender: validGender);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetValidBirthDateData), DynamicDataSourceType.Method)]
        public void BirthDate_Validation_Successful(string displayName,
            DateTimeOffset? validBirthDate)
        {
            // Arrange.
            var registerUserViewModel =
                BuildRegisterUserViewModel(birthDate: validBirthDate);

            // Act.
            bool modelIsValid = ModelValidator.IsValid(registerUserViewModel);

            // Assert.
            Assert.IsTrue(modelIsValid, displayName);
        }

        private static IEnumerable<object[]> GetTestValidEmailData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if Email has valid format and average length." +
                " Validation successful.",
                "mail@mail.com"
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if Email has valid format and MAX allowed length." +
                " Validation successful.",
                new string('a', 315) + "@a.ua"
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if Email has valid format and MIN allowed length." +
                " Validation successful.",
                "a@a.ua"
            };
        }

        private static IEnumerable<object[]> GetTestValidPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if Password has valid format and average length." +
                " Validation successful.",
                "TestPassword@1"
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if Password has valid format and MAX allowed length." +
                " Validation successful.",
                new string('A', 98) + "@1"
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if Password has valid format and MIN allowed length." +
                " Validation successful.",
                new string('A', 6) + "@1"
            };
        }

        private static IEnumerable<object[]> GetTestValidPasswordConfirmationData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if PasswordConfirmation has average length and matches Password." +
                " Validation successful.",
                "TestPassword@1"
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if PasswordConfirmation has MAX allowed length and matches Password." +
                " Validation successful.",
                new string('A', 98) + "@1"
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if PasswordConfirmation has MIN allowed length and matches Password." +
                " Validation successful.",
                new string('A', 6) + "@1"
            };
        }

        private static IEnumerable<object[]> GetTestValidFirstNameData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if FirstName has average length." +
                " Validation successful.",
                new string('*', 20)
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if FirstName has MAX allowed length." +
                " Validation successful.",
                new string('*', 50)
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if FirstName has MIN allowed length." +
                " Validation successful.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 4: Test RegisterUserViewModel validation" +
                " if FirstName is null" +
                " Validation successful.",
                null
            };
        }

        private static IEnumerable<object[]> GetTestValidLastNameData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if LastName has average length." +
                " Validation successful.",
                new string('*', 20)
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if LastName has MAX allowed length." +
                " Validation successful.",
                new string('*', 50)
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if LastName has MIN allowed length." +
                " Validation successful.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 4: Test RegisterUserViewModel validation" +
                " if LastName is null" +
                " Validation successful.",
                null
            };
        }

        private static IEnumerable<object[]> GetTestValidAboutMeData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if AboutMe has average length." +
                " Validation successful.",
                new string('*', 20)
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if AboutMe has MAX allowed length." +
                " Validation successful.",
                new string('*', 300)
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if AboutMe has MIN allowed length." +
                " Validation successful.",
                string.Empty
            };

            yield return new object[]
            {
                "Test case 4: Test RegisterUserViewModel validation" +
                " if AboutMe is null" +
                " Validation successful.",
                null
            };
        }

        private static IEnumerable<object[]> GetValidGenderData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if Gender has MIN allowed value." +
                " Validation successful.",
                1
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if Gender has MAX allowed value." +
                " Validation successful.",
                1
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if Gender is null." +
                " Validation successful.",
                null
            };
        }

        private static IEnumerable<object[]> GetValidBirthDateData()
        {
            yield return new object[]
            {
                "Test case 1: Test RegisterUserViewModel validation" +
                " if BirthDate has MAX allowed value." +
                " Validation successful.",
                new DateTimeOffset(DateTime.Now)
            };

            yield return new object[]
            {
                "Test case 2: Test RegisterUserViewModel validation" +
                " if BirthDate has average value." +
                " Validation successful.",
                new DateTimeOffset(DateTime.Now.AddYears(-10))
            };

            yield return new object[]
            {
                "Test case 3: Test RegisterUserViewModel validation" +
                " if BirthDate is null." +
                " Validation successful.",
                null
            };
        }
    }
}

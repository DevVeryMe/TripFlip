using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.ViewModels.UserViewModels;

namespace WebApiUnitTests.ChangeUserPasswordViewModelTests
{
    [TestClass]
    public class ChangeUserPasswordViewModelPositiveTests
    {
        [DataTestMethod]
        [DynamicData(nameof(GetTestOldPasswordData), DynamicDataSourceType.Method)]
        public void TestOldPasswordValidation(
            string testCaseDisplayName,
            string oldPassword)
        {
            // Arrange
            var changeUserPasswordViewModel =
                GetChangeUserPasswordViewModel(oldPassword: oldPassword);

            // Act
            var result = Validator.TryValidateObject(changeUserPasswordViewModel,
                new ValidationContext(changeUserPasswordViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestNewPasswordData), DynamicDataSourceType.Method)]
        public void TestNewPasswordValidation(
            string testCaseDisplayName,
            string newPassword,
            string newPasswordConfirmation)
        {
            // Arrange
            var changeUserPasswordViewModel =
                GetChangeUserPasswordViewModel(newPassword: newPassword, 
                    newPasswordConfirmation: newPasswordConfirmation);

            // Act
            var result = Validator.TryValidateObject(changeUserPasswordViewModel,
                new ValidationContext(changeUserPasswordViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetTestNewPasswordConfirmationData), DynamicDataSourceType.Method)]
        public void TestNewPasswordConfirmationValidation(
            string testCaseDisplayName,
            string newPassword,
            string newPasswordConfirmation)
        {
            // Arrange
            var changeUserPasswordViewModel =
                GetChangeUserPasswordViewModel(newPassword: newPassword,
                    newPasswordConfirmation: newPasswordConfirmation);

            // Act
            var result = Validator.TryValidateObject(changeUserPasswordViewModel,
                new ValidationContext(changeUserPasswordViewModel, null, null),
                null,
                true);

            // Assert
            Assert.IsTrue(result, testCaseDisplayName);
        }

        static IEnumerable<object[]> GetTestOldPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Test_ChangeUserPassword_Validation_Given_valid_OldPassword_equals" +
                "_minimum_value_8_and_correct_format_Validation_should_be_successful.",
                "Aaaaaa1@"
            };

            yield return new object[]
            {
                "Test case 2: Test_ChangeUserPassword_Validation_Given_valid_OldPassword_" +
                "equals_maximum_value_100_and_correct_format_Validation_should_be_successful.",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaa"
            };

            yield return new object[]
            {
                "Test case 3: Test_ChangeUserPassword_Validation_Given_valid_OldPassword_" +
                "equals_average_value_50_and_correct_format_Validation_should_be_successful.",
                "AaaaaaaaaaAaaaaaaa1@AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaA"
            };
        }

        static IEnumerable<object[]> GetTestNewPasswordData()
        {
            yield return new object[]
            {
                "Test case 1: Test_ChangeUserPassword_Validation_Given_valid_NewPassword_equals" +
                "_minimum_value_8_and_correct_format_Validation_should_be_successful.",
                "Aaaaaa1@",
                "Aaaaaa1@"
            };

            yield return new object[]
            {
                "Test case 2: Test_ChangeUserPassword_Validation_Given_valid_NewPassword_" +
                "equals_maximum_value_100_and_correct_format_Validation_should_be_successful.",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaa",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaa"
            };

            yield return new object[]
            {
                "Test case 3: Test_ChangeUserPassword_Validation_Given_valid_NewPassword_" +
                "equals_average_value_50_and_correct_format_Validation_should_be_successful.",
                "AaaaaaaaaaAaaaaaaa1@AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaA",
                "AaaaaaaaaaAaaaaaaa1@AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaA"
            };
        }

        static IEnumerable<object[]> GetTestNewPasswordConfirmationData()
        {
            yield return new object[]
            {
                "Test case 1: Test_ChangeUserPassword_Validation_Given_valid_NewPasswordConfirmation_equals" +
                "_minimum_value_8_and_matches_NewPassword_Validation_should_be_successful.",
                "Aaaaaa1@",
                "Aaaaaa1@"
            };

            yield return new object[]
            {
                "Test case 2: Test_ChangeUserPassword_Validation_Given_valid_NewPasswordConfirmation_" +
                "equals_maximum_value_100_and_and_matches_NewPassword_Validation_should_be_successful.",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaa",
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaaaaAaaaaaaa1@" +
                "AaaaaaaaaaAaaaaaaaaaAaaaaaaaaa"
            };

            yield return new object[]
            {
                "Test case 3: Test_ChangeUserPassword_Validation_Given_valid_NewPasswordConfirmation_" +
                "equals_average_value_50_and_matches_NewPassword_Validation_should_be_successful.",
                "AaaaaaaaaaAaaaaaaa1@AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaA",
                "AaaaaaaaaaAaaaaaaa1@AaaaaaaaaaAaaaaaaaaaAaaaaaaaaaA"
            };
        }

        private ChangeUserPasswordViewModel GetChangeUserPasswordViewModel(
            string oldPassword = "String1@string", string newPassword = "string1@String",
            string newPasswordConfirmation = "string1@String")
        {
            return new ChangeUserPasswordViewModel()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                NewPasswordConfirmation = newPasswordConfirmation
            };
        }
    }
}

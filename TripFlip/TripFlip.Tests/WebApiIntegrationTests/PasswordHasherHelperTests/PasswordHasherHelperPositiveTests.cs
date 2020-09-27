using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.Domain.Entities;
using TripFlip.Services.Helpers;

namespace WebApiIntegrationTests.PasswordHasherHelperTests
{
    [TestClass]
    public class PasswordHasherHelperPositiveTests
    {
        [TestMethod]
        public void VerifyPassword_GivenValidData_Successful()
        {
            // Arrange.
            var password = "ValidPass1@";
            var passwordHash = GetPasswordHash();

            // Act.
            var result = PasswordHasherHelper.VerifyPassword(password, passwordHash);

            // Assert.
            Assert.IsTrue(result);
        }

        private string GetPasswordHash(string password = "ValidPass1@")
        {
            var passwordHasher = new PasswordHasher<UserEntity>();
            var hashedPassword = passwordHasher.HashPassword(user: null, password: password);

            return hashedPassword;
        }
    }
}

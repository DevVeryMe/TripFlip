using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TripFlip.Services.Helpers;

namespace WebApiUnitTests.PasswordHasherHelperTests
{
    [TestClass]
    public class PasswordHasherHelperNegativeTests
    {
        [TestMethod]
        public void HashPassword_GivenNullParameter_Successful()
        {
            // Arrange
            string password = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
               PasswordHasherHelper.HashPassword(password));
        }
    }
}

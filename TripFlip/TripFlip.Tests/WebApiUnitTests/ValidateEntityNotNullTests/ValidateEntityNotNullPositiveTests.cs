using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TripFlip.Services.Helpers;

namespace WebApiUnitTests.ValidateEntityNotNullTests
{
    [TestClass]
    public class ValidateEntityNotNullPositiveTests
    {
        [TestMethod]
        public void ValidateEntityNotNull_EntityIsNotNull_ValidationSuccessful()
        {
            // Arrange.
            object validEntity = new object();
            string nullErrorMessage = null;

            // Act + Assert.
            try
            {
                EntityValidationHelper.ValidateEntityNotNull(validEntity, nullErrorMessage);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}

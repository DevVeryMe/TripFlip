using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Helpers;

namespace WebApiUnitTests.ValidateEntityNotNullTests
{
    [TestClass]
    public class ValidateEntityNotNullNegativeTests
    {
        [TestMethod]
        public void ValidateEntityNotNull_EntityIsNull_ExceptionThrown()
        {
            // Arrange.
            object nullEntity = null;
            string nullErrorMessage = null;

            // Act + Assert.
            Assert.ThrowsException<NotFoundException>(() =>
               EntityValidationHelper.ValidateEntityNotNull(nullEntity, nullErrorMessage));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Helpers;

namespace WebApiIntegrationTests.EntityValidationHelperTests
{
    [TestClass]
    public class EntityValidationHelperNegativeTests : TestEntityValidationHelperBase
    {
        [TestInitialize]
        public void Initialize()
        {
            TripFlipDbContext = CreateDbContext();
        }

        [TestCleanup]
        public void Cleanup()
        {
            TripFlipDbContext.Dispose();
        }

        [TestMethod]
        public async Task ValidateCurrentUserIsSuperAdminAsync_GivenNonExistentCurrentUser_ExceptionThrown()
        {
            // Arrange.
            CurrentUserService = CreateCurrentUserService(NonExistentUser.Id,
                NonExistentUser.Email);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserIsSuperAdminAsync(
                    CurrentUserService, TripFlipDbContext));
        }

        [TestMethod]
        public async Task ValidateCurrentUserIsSuperAdminAsync_GivenNotApplicationSuperAdmin_ExceptionThrown()
        {
            // Arrange.
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            Seed(TripFlipDbContext, ValidUser);

            // Act + Assert.
            await Assert.ThrowsExceptionAsync<ArgumentException>(async () =>
                await EntityValidationHelper.ValidateCurrentUserIsSuperAdminAsync(
                    CurrentUserService, TripFlipDbContext));
        }
    }
}

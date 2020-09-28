using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services.Helpers;

namespace WebApiIntegrationTests.EntityValidationHelperTests
{
    [TestClass]
    public class EntityValidationHelperPositiveTests : TestEntityValidationHelperBase
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
        public async Task ValidateCurrentUserIsSuperAdminAsync_GivenValidData_Successful()
        {
            // Arrange.
            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, ApplicationRoleEntitiesToSeed);
            Seed(TripFlipDbContext, ApplicationUserRoleEntityToSeed);

            // Act + Assert.
            await EntityValidationHelper.ValidateCurrentUserIsSuperAdminAsync(
                CurrentUserService, TripFlipDbContext);
        }
    }
}

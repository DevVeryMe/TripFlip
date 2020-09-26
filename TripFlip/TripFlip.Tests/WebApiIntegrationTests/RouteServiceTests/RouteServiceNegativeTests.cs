using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;

namespace WebApiIntegrationTests.RouteServiceTests
{
    [TestClass]
    public class RouteServiceNegativeTests : TestRouteServiceBase
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
        public async Task GetByIdAsync_GivenNotValidId_ExceptionThrown()
        {
            // Arrange
            var invalidId = 2;

            var routeService = new RouteService(TripFlipDbContext, Mapper);

            // Act + Assert
            await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
                await routeService.GetByIdAsync(invalidId));
        }
    }
}

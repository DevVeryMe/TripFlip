using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Services;
using TripFlip.Services.Dto.RouteDtos;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.RouteServiceTests
{
    [TestClass]
    public class RouteServicePositiveTests : TestRouteServiceBase
    {
        private readonly RouteDto _expectedGotByIdRouteDto = new RouteDto()
        {
            Id = 1,
            TripId = 1,
            Title = "Route"
        };

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
        public async Task GetById_GivenValidId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, RouteEntityToSeed);

            var validId = 1;

            var routeService = new RouteService(TripFlipDbContext, Mapper);
            var compaper = new RouteDtoComparer();

            // Act
            var resultRouteDto = await routeService.GetByIdAsync(validId);

            // Assert
            Assert.AreEqual(0, compaper.Compare(_expectedGotByIdRouteDto, resultRouteDto));
        }
    }
}

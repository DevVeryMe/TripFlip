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
        public async Task CreateAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripRoleEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var createRouteDto = GetCreateRouteDto();
            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var routeDtoComparer = new RouteDtoComparer();

            var expectedRouteDto = new RouteDto()
            {
                Id = 1,
                Title = createRouteDto.Title,
                TripId = createRouteDto.TripId
            };

            // Act.
            var resultItemDto = await routeService.CreateAsync(createRouteDto);

            // Assert.
            Assert.AreEqual(0,
                routeDtoComparer.Compare(expectedRouteDto, resultItemDto));
        }

        [TestMethod]
        public async Task GetById_GivenValidId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, RouteEntityToSeed);

            var validId = 1;

            var routeService = new RouteService(TripFlipDbContext, Mapper, CurrentUserService);
            var compaper = new RouteDtoComparer();

            // Act
            var resultRouteDto = await routeService.GetByIdAsync(validId);

            // Assert
            Assert.AreEqual(0, compaper.Compare(_expectedGotByIdRouteDto, resultRouteDto));
        }
    }
}

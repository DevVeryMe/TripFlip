using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
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
        public async Task GetAllByTripIdAsync_ExistentTripId_Successful()
        {
            // Arrange.
            var routeEntitiesToSeed = RouteEntitiesToSeed;

            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, routeEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var routeService = new RouteService(TripFlipDbContext, Mapper,
                CurrentUserService);

            var existentTripId = 1;
            var paginationDto = GetPaginationDto();
            string searchString = null;

            var routeDtoComparer = new RouteDtoComparer();

            var expectedRouteDtoList = Mapper.Map<List<RouteDto>>(routeEntitiesToSeed);

            // Act.
            var resultRouteDtoPagedList = await routeService.GetAllByTripIdAsync(existentTripId,
                searchString, paginationDto);

            var resultRouteDtoList = resultRouteDtoPagedList.Items.ToList();

            var expectedRouteDtosCount = expectedRouteDtoList.Count;

            // Assert.
            Assert.AreEqual(expectedRouteDtosCount, resultRouteDtoList.Count);

            for (var i = 0; i < expectedRouteDtosCount; i++)
            {
                Assert.AreEqual(0,
                    routeDtoComparer.Compare(resultRouteDtoList[i], expectedRouteDtoList[i]));
            }
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
        public async Task GetByIdAsync_GivenValidId_Successful()
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

        [TestMethod]
        public async Task UpdateAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);

            var comparer = new RouteDtoComparer();

            var updateRouteDto = Get_UpdateRouteDto(tripId: TripEntityToSeed.Id);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var routeService = new RouteService(TripFlipDbContext, Mapper, CurrentUserService);

            var expectedRouteDto = Mapper.Map<RouteDto>(
                RouteEntitiesToSeed.First(route => route.Id == updateRouteDto.Id));
            expectedRouteDto.Title = updateRouteDto.Title;
            expectedRouteDto.TripId = updateRouteDto.TripId;

            // Act.
            var resultRouteDto = await routeService.UpdateAsync(updateRouteDto);

            // Assert.
            Assert.AreEqual(0, comparer.Compare(expectedRouteDto, resultRouteDto));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id, ValidUser.Email);

            var routeService = new RouteService(TripFlipDbContext, Mapper, CurrentUserService);

            int validRouteId = RouteEntityToSeed.Id;

            // Act.
            await routeService.DeleteByIdAsync(validRouteId);

            // Assert.
            bool routeIsDeleted = TripFlipDbContext
                .Routes
                .Any(route => route.Id == validRouteId) == false;

            Assert.IsTrue(routeIsDeleted);
        }
    }
}

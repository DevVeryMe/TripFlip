using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto.TripDtos;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.TripServiceTests
{
    [TestClass]
    public class TripServicePositiveTests : TestTripServiceBase
    {
        private TripDto _expectedReturnTripDto = new TripDto()
        {
            Id = 1,
            Title = "Trip",
            Description = "Description",
            StartsAt = DateTimeOffset.Parse("28/08/2030 14:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
            EndsAt = DateTimeOffset.Parse("30/11/2030 19:00:00",
                CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
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
        public async Task Test_CreateAsync_Given_Valid_Data_validation_should_be_successful()
        {
            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            Seed(TripFlipDbContext, UserEntityToSeed);
            Seed(TripFlipDbContext, TripRolesToSeed);

            var createTripDto = GetCreateTripDtoData();
            var resultTripDto = await TripService.CreateAsync(createTripDto);
            var tripDtoComparer = new TripDtoComparer();

            Assert.AreEqual(0, tripDtoComparer.Compare(resultTripDto, _expectedReturnTripDto));
        }

        [TestMethod]
        public async Task GetByIdAsync_ValidTripId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);

            int validTripId = 1;

            TripService = new TripService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: null);

            var expectedTripDto = Mapper.Map<TripDto>(TripEntityToSeed);
            expectedTripDto.Id = validTripId;

            // Act
            var resultTripDto = await TripService.GetByIdAsync(validTripId);

            // Assert
            var tripDtoComparer = new TripDtoComparer();
            Assert.AreEqual(0, tripDtoComparer.Compare(resultTripDto, expectedTripDto));
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ValidUserAndTripId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);

            Seed(TripFlipDbContext, UserEntityToSeed);
            CurrentUserService = CreateCurrentUserServiceWithExistentUser();

            Seed(TripFlipDbContext, TripSubscriberEntityToSeed);

            Seed(TripFlipDbContext, TripRolesToSeed);
            Seed(TripFlipDbContext, TripSubscriberRoleEntityToSeed);

            TripService = new TripService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: CurrentUserService);

            int validTripId = 1;

            // Act
            await TripService.DeleteByIdAsync(validTripId);

            // Assert
            bool tripIsDeleted = TripFlipDbContext
                .Trips
                .Any(trip => trip.Id == validTripId) == false;
            Assert.IsTrue(tripIsDeleted);
        }

        private static CreateTripDto GetCreateTripDtoData()
        {
            return new CreateTripDto()
            {
                    Title = "Trip",
                    Description = "Description",
                    StartsAt = DateTimeOffset.Parse("28/08/2030 14:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat),
                    EndsAt = DateTimeOffset.Parse("30/11/2030 19:00:00",
                        CultureInfo.GetCultureInfo("en-GB").DateTimeFormat)
            };
        }
    }
}

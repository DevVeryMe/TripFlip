using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
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

        [TestMethod]
        public async Task Test_CreateAsync_Given_Valid_Data_validation_should_be_successful()
        {
            var tripFlipDbContext = CreateDbContext();

            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            TripService = new TripService(tripFlipDbContext, Mapper, CurrentUserService);

            Seed(tripFlipDbContext, UserEntityToSeed);
            Seed(tripFlipDbContext, TripRolesToSeed);

            var createTripDto = GetCreateTripDtoData();
            var resultTripDto = await TripService.CreateAsync(createTripDto);
            var tripDtoComparer = new TripDtoComparer();

            Assert.AreEqual(0, tripDtoComparer.Compare(resultTripDto, _expectedReturnTripDto));
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

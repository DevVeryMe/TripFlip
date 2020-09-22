using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto.TripDtos;

namespace WebApiIntegrationTests.TripServiceTests
{
    [TestClass]
    public class TripServicePositiveTests : TestTripServiceBase
    {
        [TestMethod]
        public async Task Test_CreateAsync_Given_Valid_Data_validation_should_be_successful()
        {
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            Seed(UserEntityToSeed);
            Seed(TripRolesToSeed);

            var createTripDto = GetCreateTripDtoData();
            var resultTripDto = await TripService.CreateAsync(createTripDto);

            var result = CompareOutputData(createTripDto, resultTripDto);

            Assert.IsTrue(result);
    }

        private bool CompareOutputData(CreateTripDto input, TripDto output)
        {
            var isEqual = !(output == null || 
                          input == null || 
                          output.Id == default || 
                          input.Description != output.Description || 
                          input.Title != output.Title || 
                          input.EndsAt != output.EndsAt || 
                          input.StartsAt != output.StartsAt);

            return isEqual;
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

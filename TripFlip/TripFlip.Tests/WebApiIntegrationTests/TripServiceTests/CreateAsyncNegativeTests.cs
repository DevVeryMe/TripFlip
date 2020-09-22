﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.Services;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto.TripDtos;

namespace WebApiIntegrationTests.TripServiceTests
{
    [TestClass]
    public class CreateAsyncNegativeTests : TestTripServiceBase
    {
        [TestMethod]
        public async Task Test_CreateAsync_Given_Not_valid_CurrentUser_Data_Validation_should_be_failed()
        {
            var createTripDto = GetCreateTripDtoData();

            // Reset CurrentUserService and TripService with non existent user.
            CurrentUserService = CreateCurrentUserServiceWithNonExistentUser();
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            await Assert.ThrowsExceptionAsync<NotFoundException>(
                () => TripService.CreateAsync(createTripDto));
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

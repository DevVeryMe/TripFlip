using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto;
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
        public async Task GetAllTripsAsync_SeededValidData_RecievedDataMatchesSeeded()
        {
            // Arrange.
            var tripEntitiesToSeed = TripEntitiesToSeed;

            Seed(TripFlipDbContext, tripEntitiesToSeed);
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            var paginationDto = GetPaginationDto();

            // Act.
            var returnedTripDtosPagedList =
                await TripService.GetAllTripsAsync(
                    searchString: null,
                    paginationDto: paginationDto);

            var returnedTripDtosList = returnedTripDtosPagedList.Items.ToList();
            var expectedTripDtosList = Mapper.Map<List<TripDto>>(tripEntitiesToSeed);

            var tripDtoComparer = new TripDtoComparer();

            // Assert.
            Assert.AreEqual(expectedTripDtosList.Count, returnedTripDtosList.Count);

            for (int i = 0; i < expectedTripDtosList.Count; i++)
            {
                Assert.AreEqual(0,
                    tripDtoComparer.Compare(expectedTripDtosList[i], returnedTripDtosList[i]));
            }

        }

        [TestMethod]
        public async Task UpdateAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, UserEntityToSeed);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberAdminRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserServiceWithExistentUser();
            TripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);

            var updateTripDto = GetUpdateTripDto();

            // Act.
            var resultTripDto = await TripService.UpdateAsync(updateTripDto);

            var expectedTripDto = new TripDto()
            {
                Id = updateTripDto.Id,
                Title = updateTripDto.Title,
                Description = updateTripDto.Description,
                StartsAt = updateTripDto.StartsAt,
                EndsAt = updateTripDto.EndsAt
            };

            var tripDtoComparer = new TripDtoComparer();

            // Assert.
            Assert.AreEqual(0,
                tripDtoComparer.Compare(expectedTripDto, resultTripDto));
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

        protected PaginationDto GetPaginationDto(int? pageNumber = null,
            int? pageSize = null)
        {
            return new PaginationDto()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
}

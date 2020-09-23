using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.Services;
using TripFlip.Services.Dto.ItemListDtos;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.ItemListServiceTests
{
    [TestClass]
    public class ItemListServicePositiveTests : TestItemListServiceBase
    {
        private readonly ItemListDto _expectedReturnItemListDto = new ItemListDto()
        {
            Id = 1,
            Title = "Title"
        };

        [TestMethod]
        public async Task Test_CreateItemList_Valid_Data_should_be_successful()
        {
            var tripFlipDbContext = CreateDbContext();

            Seed(tripFlipDbContext, CorrectUser);
            Seed(tripFlipDbContext, TripEntityToSeed);
            Seed(tripFlipDbContext, RouteEntityToSeed);
            Seed(tripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(tripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(tripFlipDbContext, RouteRoleEntityToSeed);
            Seed(tripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(CorrectUser.Id,
                CorrectUser.Email);

            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(tripFlipDbContext, Mapper, 
                CurrentUserService);
            var resultItemListDto = await itemListService.CreateAsync(createItemListDto);
            var comparer = new ItemListDtoComparer();

            Assert.AreEqual(0, 
                comparer.Compare(_expectedReturnItemListDto, resultItemListDto));
        }

        private CreateItemListDto GetCreateItemListDto(int routeId = 1,
            string title = "Title")
        {
            return new CreateItemListDto()
            {
                RouteId = routeId,
                Title = title
            };
        }
    }
}

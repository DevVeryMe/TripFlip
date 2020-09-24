using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
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
            Title = "Title",
            RouteId = 1
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
        public async Task GetByIdAsync_ExistingItemListId_Successful()
        {
            // Arrange.
            var itemListEntityToSeed = ItemListEntityToSeed;

            Seed(TripFlipDbContext, itemListEntityToSeed);

            var existingItemListId = itemListEntityToSeed.Id;

            var itemListService = new ItemListService(TripFlipDbContext,
                Mapper,
                CurrentUserService);

            var recievedItemListDto =
                await itemListService.GetByIdAsync(existingItemListId);

            var seededItemListDto = Mapper.Map<ItemListDto>(itemListEntityToSeed);

            var comparer = new ItemListDtoComparer();

            // Act + Assert.
            Assert.AreEqual(0,
                comparer.Compare(recievedItemListDto, seededItemListDto));
        }

        [TestMethod]
        public async Task Test_CreateItemList_Valid_Data_should_be_successful()
        {
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var createItemListDto = GetCreateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, 
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

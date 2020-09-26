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
        public async Task UpdateAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEditorRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var updateItemListDto = GetUpdateItemListDto();
            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            // Act. 
            var resultItemListDto =
                await itemListService.UpdateAsync(updateItemListDto);

            var expectedItemListDto = new ItemListDto()
            {
                Id = updateItemListDto.Id,
                Title = updateItemListDto.Title,
                RouteId = resultItemListDto.RouteId // UpdateItemListDto doesn't contain this field, so it initialized like that.
            };

            var comparer = new ItemListDtoComparer();

            // Assert.
            Assert.AreEqual(0,
                comparer.Compare(resultItemListDto, expectedItemListDto));
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
        public async Task CreateAsync_ValidData_Successful()
        {
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

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
    }
}

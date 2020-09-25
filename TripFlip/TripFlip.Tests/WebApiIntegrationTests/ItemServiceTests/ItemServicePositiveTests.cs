using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TripFlip.Services;
using TripFlip.Services.Dto.ItemDtos;
using WebApiIntegrationTests.CustomComparers;

namespace WebApiIntegrationTests.ItemServiceTests
{
    [TestClass]
    public class ItemServicePositiveTests : TestItemServiceBase
    {
        private readonly ItemDto _expectedReturnItemDto = new ItemDto()
        {
            Id = 1,
            Title = "Title",
            ItemListId = 1,
            Quantity = "Quantity",
            IsCompleted = false,
            Comment = "Comment"
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
        public async Task CreateItem_ValidData_Successful()
        {
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntityToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var createItemDto = GetCreateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);
            var resultItemDto = await itemService.CreateAsync(createItemDto);
            var comparer = new ItemDtoComparer();

            Assert.AreEqual(0,
                comparer.Compare(_expectedReturnItemDto, resultItemDto));
        }
    }
}

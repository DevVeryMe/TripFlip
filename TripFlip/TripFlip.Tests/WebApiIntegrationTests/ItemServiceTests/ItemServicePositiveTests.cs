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
        private readonly ItemDto _expectedCreatedItemDto = new ItemDto()
        {
            Id = 1,
            Title = "Title",
            ItemListId = 1,
            Quantity = "Quantity",
            IsCompleted = false,
            Comment = "Comment"
        };

        private readonly ItemDto _expectedUpdatedItemDto = new ItemDto()
        {
            Id = 1,
            Title = "Updated title",
            ItemListId = 1,
            Quantity = "Updated quantity",
            IsCompleted = true,
            Comment = "Updated comment"
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
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var updateItemDto = GetUpdateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);
            var resultItemDto = await itemService.UpdateAsync(updateItemDto);
            var comparer = new ItemDtoComparer();

            Assert.AreEqual(0,
                comparer.Compare(_expectedUpdatedItemDto, resultItemDto));
        }

        private CreateItemDto GetCreateItemDto(int itemListId = 1, string title = "Title",
            string comment = "Comment", string quantity = "Quantity")
        {
            return new CreateItemDto()
            {
                ItemListId = itemListId,
                Title = title,
                Comment = comment,
                Quantity = quantity
            };
        }

        private UpdateItemDto GetUpdateItemDto(int itemId = 1, bool isCompleted = true,
            string title = "Updated title", string comment = "Updated comment",
            string quantity = "Updated quantity")
        {
            return new UpdateItemDto()
            {
                Id = itemId,
                IsCompleted = isCompleted,
                Comment = comment,
                Title = title,
                Quantity = quantity
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
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
        public async Task GetAllByItemListIdAsync_ExistentItemListId_Successful()
        {
            // Arrange.
            var itemEntitiesToSeed = ItemEntitiesToSeed;

            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, itemEntitiesToSeed);

            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);

            string searchString = null;
            var existentItemListId = 1;
            var paginationDto = GetPaginationDto();

            var expectedItemDtoList = Mapper.Map<List<ItemDto>>(itemEntitiesToSeed);

            var itemDtoComparer = new ItemDtoComparer();

            // Act.
            var resultItemDtoPagedList = await itemService.GetAllByItemListIdAsync(existentItemListId,
                searchString, paginationDto);

            var resultItemDtoList = resultItemDtoPagedList.Items.ToList();

            var expectedItemsCount = expectedItemDtoList.Count;

            // Assert.
            Assert.AreEqual(expectedItemsCount, resultItemDtoList.Count);

            for (var i = 0; i < expectedItemsCount; i++)
            {
                Assert.AreEqual(0,
                    itemDtoComparer.Compare(resultItemDtoList[i], expectedItemDtoList[i]));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ValidData_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var createItemDto = GetCreateItemDto();
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);
            var comparer = new ItemDtoComparer();

            //Act
            var resultItemDto = await itemService.CreateAsync(createItemDto);

            // Assert
            Assert.AreEqual(0,
                comparer.Compare(_expectedCreatedItemDto, resultItemDto));
        }

        [TestMethod]
        public async Task UpdateAsync_ValidData_Successful()
        {
            // Arrange
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
            var comparer = new ItemDtoComparer();

            // Act
            var resultItemDto = await itemService.UpdateAsync(updateItemDto);
            
            // Assert
            Assert.AreEqual(0,
                comparer.Compare(_expectedUpdatedItemDto, resultItemDto));
        }

        [TestMethod]
        public async Task SetItemAssigneesAsync_ValidData_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, ValidUser);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEditorRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var itemAssigneesDto = GetItemAssigneesDto(routeSubscriberIds:
                ValidRouteSubscribersToAssignToItem);
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);

            // Act.
            await itemService.SetItemAssigneesAsync(itemAssigneesDto);
            
            // Assert.
            var item = TripFlipDbContext.Items
                .Include(item => item.ItemAssignees)
                .FirstOrDefault(item => item.Id == ItemEntityToSeed.Id);
            Assert.IsNotNull(item);
            Assert.AreEqual(1, item.ItemAssignees.Count);
        }

        [TestMethod]
        public async Task DeleteById_ExistentItemId_Successful()
        {
            // Arrange.
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, ItemEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberRoleEntitiesToSeed);
            Seed(TripFlipDbContext, ValidUser);

            CurrentUserService = CreateCurrentUserService(ValidUser.Id,
                ValidUser.Email);

            var existentItemId = 1;
            var itemService = new ItemService(Mapper, TripFlipDbContext,
                CurrentUserService);

            // Act.
            await itemService.DeleteByIdAsync(existentItemId);

            // Assert.
            var taskEntity = await TripFlipDbContext
                .Items
                .FindAsync(existentItemId);

            Assert.IsNull(taskEntity);
        }
    }
}

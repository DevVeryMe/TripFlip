using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
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

            var expectedItemListDto = Mapper.Map<ItemListDto>(itemListEntityToSeed);

            var comparer = new ItemListDtoComparer();

            // Act.
            var resultItemListDto =
                await itemListService.GetByIdAsync(existingItemListId);

            // Assert.
            Assert.AreEqual(0,
                comparer.Compare(resultItemListDto, expectedItemListDto));
        }

        [TestMethod]
        public async Task CreateAsync_ValidData_Successful()
        {
            // Arrange
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

            var comparer = new ItemListDtoComparer();

            // Act
            var resultItemListDto = await itemListService.CreateAsync(createItemListDto);

            // Assert
            Assert.AreEqual(0,
                comparer.Compare(_expectedReturnItemListDto, resultItemListDto));
        }

        [TestMethod]
        public async Task GetAllByRouteIdAsync_ExistingRouteId_Successful()
        {
            // Arrange
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);

            var validRouteId = 1;

            var itemListService = new ItemListService(
                tripFlipDbContext: TripFlipDbContext,
                mapper: Mapper,
                currentUserService: null);

            var expectedItemListDto = Mapper.Map<ItemListDto>(ItemListEntityToSeed);
            var expectedItemListDtos = new List<ItemListDto> { expectedItemListDto };

            var paginationDto = GetPaginationDto();

            var comparer = new ItemListDtoComparer();

            // Act
            var resultPagedList = await itemListService.GetAllByRouteIdAsync(
                routeId: validRouteId,
                searchString: null,
                paginationDto: paginationDto);

            var resultItemListDtos = resultPagedList.Items.ToList();

            // Assert
            int resultItemListDtosCount = resultItemListDtos.Count;
            Assert.AreEqual(resultItemListDtosCount, expectedItemListDtos.Count);

            for (int i = 0; i < resultItemListDtosCount; i++)
            {
                Assert.AreEqual(0,
                    comparer.Compare(resultItemListDtos[i], expectedItemListDtos[i]));
            }
        }

        [TestMethod]
        public async Task DeleteByIdAsync_ValidItemListIdAndCurrentUser_Successful()
        {
            // Arrange
            var validUserThatIsRouteAdmin = ValidUser;
            Seed(TripFlipDbContext, validUserThatIsRouteAdmin);
            Seed(TripFlipDbContext, TripEntityToSeed);
            Seed(TripFlipDbContext, RouteEntityToSeed);
            Seed(TripFlipDbContext, ItemListEntityToSeed);
            Seed(TripFlipDbContext, TripSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberEntitiesToSeed);
            Seed(TripFlipDbContext, RouteRoleEntitiesToSeed);
            Seed(TripFlipDbContext, RouteSubscriberAdminRoleEntityToSeed);

            CurrentUserService = CreateCurrentUserService(
                validUserThatIsRouteAdmin.Id, validUserThatIsRouteAdmin.Email);

            var itemListService = new ItemListService(TripFlipDbContext, Mapper, CurrentUserService);

            int existingItemListId = 1;

            // Act
            await itemListService.DeleteByIdAsync(existingItemListId);

            // Assert
            bool itemListIsDeleted = TripFlipDbContext
                .ItemLists
                .Any(itemList => itemList.Id == existingItemListId) == false;

            Assert.IsTrue(itemListIsDeleted);
        }
    }
}

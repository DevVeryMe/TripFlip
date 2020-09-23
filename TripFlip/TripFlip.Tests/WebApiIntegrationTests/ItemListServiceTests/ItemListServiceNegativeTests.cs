using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApiIntegrationTests.ItemListServiceTests
{
    [TestClass]
    public class ItemListServiceNegativeTests : TestItemListServiceBase
    {
        [TestMethod]
        public async Task Test_CreateItemList_Given_Non_existent_RouteId_should_be_failed()
        {

        }
    }
}

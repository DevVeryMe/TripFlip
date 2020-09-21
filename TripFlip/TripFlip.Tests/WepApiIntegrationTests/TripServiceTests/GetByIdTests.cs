using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripFlip.Services;

namespace WepApiIntegrationTests.TripServiceTests
{
    [TestClass]
    public class GetByIdTests : TestTripServiceBase
    {
        [TestMethod]
        public async Task Test()
        {
            var tripService = new TripService(TripFlipDbContext, Mapper, CurrentUserService);
            var result = await tripService.GetByIdAsync(1);

            Assert.IsNotNull(result);
        }
    }
}

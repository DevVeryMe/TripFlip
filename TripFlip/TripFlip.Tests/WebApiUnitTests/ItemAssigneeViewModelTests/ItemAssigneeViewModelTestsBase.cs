using System.Collections.Generic;
using TripFlip.ViewModels.ItemViewModels;

namespace WebApiUnitTests.ItemAssigneeViewModelTests
{
    public abstract class ItemAssigneeViewModelTestsBase
    {
        /// <summary>
        /// Creates ItemAssigneesViewModel object with given Item id
        /// and Route subscribers' ids.
        /// </summary>
        /// <param name="routeSubscriberIds">Route subscribers' ids.</param>
        /// <param name="itemId">Item id.</param>
        /// <returns>Created ItemAssigneesViewModel object.</returns>
        protected static ItemAssigneesViewModel BuildItemAssigneesViewModel(
            IEnumerable<int> routeSubscriberIds,
            int itemId = 3)
        {
            var itemAssigneesViewModel = new ItemAssigneesViewModel()
            {
                ItemId = itemId,
                RouteSubscriberIds = routeSubscriberIds
            };

            return itemAssigneesViewModel;
        }
    }
}

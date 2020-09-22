using System.Collections.Generic;
using TripFlip.ViewModels.RouteSubscriberViewModels;

namespace TripFlip.ViewModels.ItemViewModels
{
    public class ItemWithAssigneesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string Quantity { get; set; }

        public bool IsCompleted { get; set; }

        public ICollection<RouteSubscriberViewModel> ItemAssignees { get; set; }
    }
}

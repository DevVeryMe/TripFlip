using System.Collections.Generic;
using TripFlip.ViewModels.ItemListViewModels;
using TripFlip.ViewModels.RoutePointViewModels;
using TripFlip.ViewModels.TaskListViewModels;

namespace TripFlip.ViewModels.RouteViewModels
{
    public class RouteWithIncludesViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<RoutePointViewModel> RoutePoints { get; set; }

        public ICollection<ItemListWithIncludesViewModel> ItemLists { get; set; }

        public ICollection<TaskListWithIncludesViewModel> TaskLists { get; set; }
    }
}

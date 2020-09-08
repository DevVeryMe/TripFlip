using System.Collections.Generic;
using TripFlip.ViewModels.ItemListViewModels;
using TripFlip.ViewModels.RoutePointViewModels;
using TripFlip.ViewModels.TaskListViewModels;

namespace TripFlip.ViewModels.RouteViewModels
{
    public class RouteWithPointsItemAndTaskListsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<RoutePointViewModel> RoutePoints { get; set; }

        public ICollection<ItemListWithItemsViewModel> ItemLists { get; set; }

        public ICollection<TaskListWithTasksViewModel> TaskLists { get; set; }
    }
}

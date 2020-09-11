using System.Collections.Generic;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RoutePointDtos;
using TripFlip.Services.Dto.TaskListDtos;

namespace TripFlip.Services.Dto.RouteDtos
{
    public class RouteWithPointsItemAndTaskListsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<RoutePointDto> RoutePoints { get; set; }

        public ICollection<ItemListWithItemsDto> ItemLists { get; set; }

        public ICollection<TaskListWithTasksDto> TaskLists { get; set; }
    }
}

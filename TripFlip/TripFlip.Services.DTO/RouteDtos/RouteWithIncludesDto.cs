using System.Collections.Generic;
using TripFlip.Services.Dto.ItemListDtos;
using TripFlip.Services.Dto.RoutePointDtos;
using TripFlip.Services.Dto.TaskListDtos;

namespace TripFlip.Services.Dto.RouteDtos
{
    public class RouteWithIncludesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<RoutePointDto> RoutePoints { get; set; }

        public ICollection<ItemListWithIncludesDto> ItemLists { get; set; }

        public ICollection<TaskListWithIncludesDto> TaskLists { get; set; }
    }
}

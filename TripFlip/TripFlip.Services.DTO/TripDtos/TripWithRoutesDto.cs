using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.RouteDtos;

namespace TripFlip.Services.Dto.TripDtos
{
    public class TripWithRoutesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }

        public ICollection<RouteWithPointsItemAndTaskListsDto> Routes { get; set; }
    }
}
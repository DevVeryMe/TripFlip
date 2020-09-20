using System.Collections.Generic;
using TripFlip.Services.Dto.RouteSubscriberDtos;

namespace TripFlip.Services.Dto.ItemDtos
{
    public class ItemWithAssigneesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Comment { get; set; }

        public string Quantity { get; set; }

        public bool IsCompleted { get; set; }

        public ICollection<RouteSubscriberDto> ItemAssignees { get; set; }
    }
}

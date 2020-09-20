using System.Collections.Generic;

namespace TripFlip.Services.Dto.ItemDtos
{
    public class ItemAssigneesDto
    {
        public int ItemId { get; set; }

        public IEnumerable<int> RouteSubscriberIds { get; set; }
    }
}

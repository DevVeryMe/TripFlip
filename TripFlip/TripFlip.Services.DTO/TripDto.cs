using System;

namespace TripFlip.Services.DTO
{
    public class TripDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? StartsAt { get; set; }
        public DateTimeOffset? EndsAt { get; set; }
    }
}

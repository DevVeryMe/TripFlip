using System;

namespace TripFlip.Services.Dto.TripDtos
{
    public class TripDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset? StartsAt { get; set; }
        public DateTimeOffset? EndsAt { get; set; }
    }
}

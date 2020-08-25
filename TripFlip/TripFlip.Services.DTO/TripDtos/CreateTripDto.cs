using System;

namespace TripFlip.Services.Dto.TripDtos
{
    public class CreateTripDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTimeOffset? StartsAt { get; set; }

        public DateTimeOffset? EndsAt { get; set; }
    }
}

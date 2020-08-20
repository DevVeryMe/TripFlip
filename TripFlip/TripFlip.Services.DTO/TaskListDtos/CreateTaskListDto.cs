using System;

namespace TripFlip.Services.DTO.TaskListDtos
{
    public class CreateTaskListDto
    {
        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int RouteId { get; set; }
    }
}

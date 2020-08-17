using System;
using System.Collections.Generic;
using System.Text;

namespace TripFlip.Services.DTO.TaskListDtos
{
    public class TaskListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int RouteId { get; set; }
    }
}

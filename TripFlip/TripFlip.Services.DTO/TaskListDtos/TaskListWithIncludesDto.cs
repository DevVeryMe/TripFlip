using System.Collections.Generic;
using TripFlip.Services.Dto.TaskDtos;

namespace TripFlip.Services.Dto.TaskListDtos
{
    public class TaskListWithIncludesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<TaskDto> Tasks { get; set; }
    }
}
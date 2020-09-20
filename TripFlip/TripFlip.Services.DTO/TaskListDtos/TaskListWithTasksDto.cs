using System.Collections.Generic;
using TripFlip.Services.Dto.TaskDtos;

namespace TripFlip.Services.Dto.TaskListDtos
{
    public class TaskListWithTasksDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<TaskWithAssigneesDto> Tasks { get; set; }
    }
}
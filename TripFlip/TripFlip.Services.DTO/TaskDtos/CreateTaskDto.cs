using TripFlip.Services.Dto.Enums;

namespace TripFlip.Services.Dto.TaskDtos
{
    public class CreateTaskDto
    {
        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public int TaskListId { get; set; }
    }
}

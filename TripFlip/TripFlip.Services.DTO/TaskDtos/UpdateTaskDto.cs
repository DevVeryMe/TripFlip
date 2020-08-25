using TripFlip.Services.Dto.Enums;

namespace TripFlip.Services.Dto.TaskDtos
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public bool IsCompleted { get; set; }
    }
}

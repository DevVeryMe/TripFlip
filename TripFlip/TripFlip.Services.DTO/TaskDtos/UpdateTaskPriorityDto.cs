using TripFlip.Services.DTO.Enums;

namespace TripFlip.Services.DTO.TaskDtos
{
    public class UpdateTaskPriorityDto
    {
        public int Id { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }
    }
}

using TripFlip.Services.Dto.Enums;

namespace TripFlip.Services.Dto.TaskDtos
{
    public class UpdateTaskPriorityDto
    {
        public int Id { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }
    }
}

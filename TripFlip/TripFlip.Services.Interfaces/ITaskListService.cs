using System.Threading.Tasks;
using TripFlip.Services.DTO.TaskListDtos;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskListService
    {
        /// <summary>
        /// Creates new task list.
        /// </summary>
        /// <param name="taskDto">task list data</param>
        /// <returns>created task list</returns>
        Task<TaskListDto> CreateAsync(TaskListDto taskListDto);
    }
}

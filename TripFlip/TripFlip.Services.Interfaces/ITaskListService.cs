using System.Threading.Tasks;
using TripFlip.Services.DTO.TaskListDtos;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskListService
    {
        /// <summary>
        /// Gets task list by id.
        /// </summary>
        /// <param name="id">task list id</param>
        Task<TaskListDto> GetByIdAsync(int id);

        /// <summary>
        /// Creates new task list.
        /// </summary>
        /// <param name="taskDto">Task list data.</param>
        /// <returns>Created task list DTO.</returns>
        Task<TaskListDto> CreateAsync(CreateTaskListDto taskListDto);
    }
}

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
        /// <param name="taskDto">task list data</param>
        /// <returns>created task list</returns>
        Task<TaskListDto> CreateAsync(TaskListDto taskListDto);

        /// <summary>
        /// Updates existing task list.
        /// </summary>
        /// <param name="taskListDto">new task list data</param>
        /// <returns>updated task list</returns>
        Task<TaskListDto> UpdateAsync(TaskListDto taskListDto);

        /// <summary>
        /// Deletes task list.
        /// </summary>
        /// <param name="id">task list to delete id</param>
        Task DeleteAsync(int id);
    }
}

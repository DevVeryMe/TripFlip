using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Gets all tasks from certain task list.
        /// </summary>
        /// <param name="taskListId">task list id</param>
        Task<IEnumerable<TaskDto>> GetAllByTaskListIdAsync(int taskListId);

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">task id</param>
        /// <returns>task DTO</returns>
        Task<TaskDto> GetByIdAsync(int id);

        /// <summary>
        /// Creates new task.
        /// </summary>
        /// <param name="taskDto">task data</param>
        /// <returns>created task</returns>
        Task<TaskDto> CreateAsync(TaskDto taskDto);

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskDto">new task data</param>
        /// <returns>updated task DTO</returns>
        Task<TaskDto> UpdateAsync(TaskDto taskDto);

        /// <summary>
        /// Deletes task by id.
        /// </summary>
        /// <param name="id">task to delete id</param>
        Task DeleteByIdAsync(int id);
    }
}

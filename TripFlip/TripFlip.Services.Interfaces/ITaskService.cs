using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Gets all tasks.
        /// </summary>
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">task id</param>
        Task<TaskDto> GetTaskAsync(int id);

        /// <summary>
        /// Creates new task.
        /// </summary>
        /// <param name="taskDto">task data</param>
        Task CreateTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Updates existing task.
        /// </summary>
        /// <param name="taskDto">new task data</param>
        Task UpdateTaskAsync(TaskDto taskDto);

        /// <summary>
        /// Deletes task.
        /// </summary>
        /// <param name="id">task to delete id</param>
        Task DeleteTaskAsync(int id);
    }
}

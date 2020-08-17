using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO.TaskDtos;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Gets all tasks.
        /// </summary>
        Task<IEnumerable<TaskDto>> GetAllAsync();

        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">task id</param>
        Task<TaskDto> GetAsync(int id);

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
        /// <returns>updated task</returns>
        Task<TaskDto> UpdateAsync(TaskDto taskDto);

        /// <summary>
        /// Deletes task.
        /// </summary>
        /// <param name="id">task to delete id</param>
        Task DeleteAsync(int id);
    }
}

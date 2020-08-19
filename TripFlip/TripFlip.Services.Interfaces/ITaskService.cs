using System;
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
        /// Creates new task.
        /// </summary>
        /// <param name="taskDto">task data</param>
        /// <returns>Created task</returns>
        Task<TaskDto> CreateAsync(TaskDto taskDto);
    }
}

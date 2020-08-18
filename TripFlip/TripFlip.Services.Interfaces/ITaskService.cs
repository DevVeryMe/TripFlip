using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskService
    {
        /// <summary>
        /// Creates new task.
        /// </summary>
        /// <param name="taskDto">task data</param>
        /// <returns>Created task</returns>
        Task<TaskDto> CreateAsync(TaskDto taskDto);
    }
}

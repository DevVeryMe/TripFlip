using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO.TaskListDtos;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskListService
    {
        /// <summary>
        /// Gets all task lists from certain route.
        /// </summary>
        /// <param name="routeId">Route id.</param>
        /// <returns>Task list DTOs of route specified by id.</returns>
        Task<IEnumerable<TaskListDto>> GetAllByRouteIdAsync(int routeId);

        /// <summary>
        /// Gets task list by id.
        /// </summary>
        /// <param name="id">Task list id.</param>
        /// <returns>Task list DTO with specified id.</returns>
        Task<TaskListDto> GetByIdAsync(int id);

        /// <summary>
        /// Creates new task list.
        /// </summary>
        /// <param name="taskDto">Task list data.</param>
        /// <returns>Created task list DTO.</returns>
        Task<TaskListDto> CreateAsync(CreateTaskListDto taskListDto);

        /// <summary>
        /// Updates existing task list.
        /// </summary>
        /// <param name="taskListDto">New task list data.</param>
        /// <returns>Updated task list DTO.</returns>
        Task<TaskListDto> UpdateAsync(UpdateTaskListDto taskListDto);

        /// <summary>
        /// Deletes task list by id.
        /// </summary>
        /// <param name="id">Task list to delete id.</param>
        Task DeleteAsync(int id);
    }
}

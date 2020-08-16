using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripFlip.Services.DTO;

namespace TripFlip.Services.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        TaskDto GetTask(int id);
        void CreateTask(TaskDto task);
        void UpdateTask(TaskDto task);
        void DeleteTask(int id);
    }
}

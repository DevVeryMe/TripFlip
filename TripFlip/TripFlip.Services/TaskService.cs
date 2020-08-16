using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    public class TaskService : ITaskService
    {
        private readonly FlipTripDbContext _flipTripDbContext;
        private readonly IMapper _mapper;

        public TaskService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _flipTripDbContext = flipTripDbContext;
            _mapper = mapper;
        }

        public async Task CreateTaskAsync(TaskDto task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);

            await _flipTripDbContext.Tasks.AddAsync(taskEntity);
            await _flipTripDbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
        }

        public async Task<TaskDto> GetTaskAsync(int id)
        {
            return null;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            return null;
        }

        public async Task UpdateTaskAsync(TaskDto task)
        {
        }
    }
}

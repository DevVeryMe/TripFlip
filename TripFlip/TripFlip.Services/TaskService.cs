using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        /// <inheritdoc />
        public async Task CreateTaskAsync(TaskDto taskDto)
        {
            var taskEntity = _mapper.Map<TaskEntity>(taskDto);

            await _flipTripDbContext.Tasks.AddAsync(taskEntity);
            await _flipTripDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteTaskAsync(int id)
        {
        }

        /// <inheritdoc />
        public async Task<TaskDto> GetTaskAsync(int id)
        {
            return null;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _flipTripDbContext.Tasks.AsNoTracking().ToListAsync();
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);

            return taskDtos;
        }

        /// <inheritdoc />
        public async Task UpdateTaskAsync(TaskDto taskDto)
        {
        }
    }
}

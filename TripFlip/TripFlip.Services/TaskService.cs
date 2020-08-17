using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<TaskDto> CreateAsync(TaskDto taskDto)
        {
            var taskEntity = _mapper.Map<TaskEntity>(taskDto);
            taskEntity.DateCreated = DateTimeOffset.Now;

            await _flipTripDbContext.Tasks.AddAsync(taskEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var taskToReturn = _mapper.Map<TaskDto>(taskEntity);

            return taskToReturn;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var taskToDelete = await _flipTripDbContext.Tasks.FindAsync(id);

            if (taskToDelete is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            _flipTripDbContext.Tasks.Remove(taskToDelete);
            await _flipTripDbContext.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var task = await _flipTripDbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if (task is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            var taskDto = _mapper.Map<TaskDto>(task);

            return taskDto;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TaskDto>> GetAllByTaskListIdAsync(int id)
        {
            var tasks = await _flipTripDbContext.Tasks.Where(t => t.TaskListId == id).AsNoTracking().ToListAsync();
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);

            return taskDtos;
        }

        /// <inheritdoc />
        public async Task<TaskDto> UpdateAsync(TaskDto taskDto)
        {
            var taskToUpdate = await _flipTripDbContext.Tasks.FindAsync(updatedTask.Id);

            if (taskToUpdate is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            taskToUpdate.Description = updatedTask.Description;
            taskToUpdate.PriorityLevel = updatedTask.PriorityLevel;
            taskToUpdate.IsCompleted = updatedTask.IsCompleted;

            await _flipTripDbContext.SaveChangesAsync();
            var taskToReturn = _mapper.Map<TaskDto>(taskToUpdate);

            return taskToReturn;
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    /// <inheritdoc />
    public class TaskService : ITaskService
    {
        private readonly FlipTripDbContext _flipTripDbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor. Initializes _flipTripDbContext and _mapper fields.
        /// </summary>
        /// <param name="flipTripDbContext">FlipTripDbContext instance</param>
        /// <param name="mapper">IMapper instance</param>
        public TaskService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _flipTripDbContext = flipTripDbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> CreateAsync(TaskDto taskDto)
        {
            var taskList = await _flipTripDbContext.TaskLists.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == taskDto.TaskListId);

            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.AddingTaskToNotExistingTaskList);
            }

            var taskEntity = _mapper.Map<TaskEntity>(taskDto);
            taskEntity.DateCreated = DateTimeOffset.Now;

            await _flipTripDbContext.Tasks.AddAsync(taskEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var taskToReturn = _mapper.Map<TaskDto>(taskEntity);

            return taskToReturn;
        }

        public async Task<IEnumerable<TaskDto>> GetAllByTaskListIdAsync(int id)
        {
            var taskList = await _flipTripDbContext.TaskLists.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            var tasks = await _flipTripDbContext.Tasks.Where(t => t.TaskListId == id).AsNoTracking().ToListAsync();
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);

            return taskDtos;
        }

        public async Task<TaskDto> UpdateAsync(TaskDto taskDto)
        {
            var updatedTask = _mapper.Map<TaskEntity>(taskDto);
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

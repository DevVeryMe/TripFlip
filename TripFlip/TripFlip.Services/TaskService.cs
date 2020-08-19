using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Domain.Entities.Enums;
using TripFlip.Services.DTO.TaskDtos;
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

        public async Task<IEnumerable<TaskDto>> GetAllByTaskListIdAsync(int taskListId)
        {
            var taskList = await _flipTripDbContext.TaskLists.Include(t => t.Tasks).AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == taskListId);

            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            var tasks = taskList.Tasks.ToList();
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);

            return taskDtos;
        }

        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var taskEntity = await _flipTripDbContext.Tasks.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdateAsync(UpdateTaskDto taskDto)
        {
            var taskToUpdateEntity = await _flipTripDbContext.Tasks.FindAsync(taskDto.Id);

            if (taskToUpdateEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            taskToUpdateEntity.Description = taskDto.Description;
            taskToUpdateEntity.PriorityLevel = _mapper.Map<TaskPriorityLevel>(taskDto.PriorityLevel);

            taskToUpdateEntity.IsCompleted = taskDto.IsCompleted;

            await _flipTripDbContext.SaveChangesAsync();
            var updatedTaskDto = _mapper.Map<TaskDto>(taskToUpdateEntity);

            return updatedTaskDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var taskToDelete = await _flipTripDbContext.Tasks.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskToDelete is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            _flipTripDbContext.Tasks.Remove(taskToDelete);
            await _flipTripDbContext.SaveChangesAsync();
        }
    }
}

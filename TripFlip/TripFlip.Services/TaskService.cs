using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.DTO.TaskDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class TaskService : ITaskService
    {
        private readonly TripFlipDbContext _tripFlipDbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor. Initializes _tripFlipDbContext and _mapper fields.
        /// </summary>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="mapper">IMapper instance.</param>
        public TaskService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> CreateAsync(TaskDto taskDto)
        {
            var taskList = await _tripFlipDbContext.TaskLists.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == taskDto.TaskListId);

            ValidateTaskListEntityNotNull(taskList);

            var taskEntity = _mapper.Map<TaskEntity>(taskDto);

            await _tripFlipDbContext.Tasks.AddAsync(taskEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var taskToReturn = _mapper.Map<TaskDto>(taskEntity);

            return taskToReturn;
        }

        public async Task<PagedList<TaskDto>> GetAllByTaskListIdAsync(
            int taskListId, string searchString,
            PaginationDto paginationDto)
        {
            var taskListExists = await _tripFlipDbContext
                .TaskLists
                .AnyAsync(taskListEntity => taskListEntity.Id == taskListId);

            if (!taskListExists)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            var taskEntitiesQuery = _tripFlipDbContext
                .Tasks
                .AsNoTracking()
                .Where(taskEntity => taskEntity.TaskListId == taskListId);

            if (!string.IsNullOrEmpty(searchString))
            {
                taskEntitiesQuery =
                    taskEntitiesQuery
                        .Where(taskEntity => taskEntity.Description
                        .Contains(searchString));
            }

            var pageNumber = paginationDto.PageNumber ?? 1;
            var pageSize = paginationDto.PageSize ?? await taskEntitiesQuery.CountAsync();

            var tasksPagedList = taskEntitiesQuery.ToPagedList(pageNumber, pageSize);

            var taskDtos = _mapper.Map< PagedList<TaskDto> >(tasksPagedList);

            return taskDtos;
        }

        public async Task<TaskDto> GetByIdAsync(int id)
        {
            var taskEntity = await _tripFlipDbContext.Tasks.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            ValidateTaskEntityNotNull(taskEntity);

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdateAsync(UpdateTaskDto taskDto)
        {
            var taskToUpdateEntity = await _tripFlipDbContext.Tasks.FindAsync(taskDto.Id);

            ValidateTaskEntityNotNull(taskToUpdateEntity);

            taskToUpdateEntity.Description = taskDto.Description;
            taskToUpdateEntity.PriorityLevel = _mapper.Map<Domain.Entities.Enums.TaskPriorityLevel>(taskDto.PriorityLevel);
            taskToUpdateEntity.IsCompleted = taskDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var updatedTaskDto = _mapper.Map<TaskDto>(taskToUpdateEntity);

            return updatedTaskDto;
        }

        public async Task<TaskDto> UpdatePriorityAsync(UpdateTaskPriorityDto updateTaskPriorityDto)
        {
            var taskToUpdateEntity = await _tripFlipDbContext.Tasks.FindAsync(updateTaskPriorityDto.Id);

            ValidateTaskEntityNotNull(taskToUpdateEntity);

            taskToUpdateEntity.PriorityLevel = _mapper
                .Map<Domain.Entities.Enums.TaskPriorityLevel>(updateTaskPriorityDto.PriorityLevel);

            await _tripFlipDbContext.SaveChangesAsync();
            var updatedTaskDto = _mapper.Map<TaskDto>(taskToUpdateEntity);

            return updatedTaskDto;
        }

        public async Task<TaskDto> UpdateCompletenessAsync(UpdateTaskCompletenessDto updateTaskCompletenessDto)
        {
            var taskToUpdateEntity = await _tripFlipDbContext.Tasks
                .FindAsync(updateTaskCompletenessDto.Id);

            ValidateTaskEntityNotNull(taskToUpdateEntity);

            taskToUpdateEntity.IsCompleted = updateTaskCompletenessDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var updatedTaskDto = _mapper.Map<TaskDto>(taskToUpdateEntity);

            return updatedTaskDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var taskToDelete = await _tripFlipDbContext.Tasks.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskToDelete is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            _tripFlipDbContext.Tasks.Remove(taskToDelete);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        private void ValidateTaskEntityNotNull(TaskEntity task)
        {
            if (task is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }
        }

        private void ValidateTaskListEntityNotNull(TaskListEntity taskList)
        {
            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }
        }
    }
}

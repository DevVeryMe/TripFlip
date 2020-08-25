using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskDtos;
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
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public TaskService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto createTaskDto)
        {
            var taskList = await _tripFlipDbContext.TaskLists
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == createTaskDto.TaskListId);

            ValidateTaskListEntityNotNull(taskList);

            var taskEntity = _mapper.Map<TaskEntity>(createTaskDto);

            await _tripFlipDbContext.Tasks.AddAsync(taskEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var taskToReturn = _mapper.Map<TaskDto>(taskEntity);

            return taskToReturn;
        }

        public async Task<PagedList<TaskDto>> GetAllByTaskListIdAsync(int taskListId,
            string searchString,
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
            var taskEntity = await _tripFlipDbContext.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            ValidateTaskEntityNotNull(taskEntity);

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdateAsync(UpdateTaskDto updateTaskDto)
        {
            var taskToUpdateEntity = await _tripFlipDbContext.Tasks
                .FindAsync(updateTaskDto.Id);

            ValidateTaskEntityNotNull(taskToUpdateEntity);

            taskToUpdateEntity.Description = updateTaskDto.Description;
            taskToUpdateEntity.PriorityLevel = _mapper.Map<Domain.Entities.Enums.TaskPriorityLevel>(updateTaskDto.PriorityLevel);
            taskToUpdateEntity.IsCompleted = updateTaskDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var updatedTaskDto = _mapper.Map<TaskDto>(taskToUpdateEntity);

            return updatedTaskDto;
        }

        public async Task<TaskDto> UpdatePriorityAsync(UpdateTaskPriorityDto updateTaskPriorityDto)
        {
            var taskToUpdateEntity = await _tripFlipDbContext.Tasks
                .FindAsync(updateTaskPriorityDto.Id);

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
            var taskToDelete = await _tripFlipDbContext.Tasks
                .AsNoTracking()
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

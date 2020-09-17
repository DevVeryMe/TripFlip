﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Helpers;
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

        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="currentUserService">ICurrentUserService instance.</param>
        public TaskService(TripFlipDbContext tripFlipDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto createTaskDto)
        {
            var taskListEntity = await _tripFlipDbContext.TaskLists
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == createTaskDto.TaskListId);

            ValidateTaskListEntityNotNull(taskListEntity);

            var taskEntity = _mapper.Map<TaskEntity>(createTaskDto);

            await _tripFlipDbContext.Tasks.AddAsync(taskEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
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
                throw new NotFoundException(ErrorConstants.TaskListNotFound);
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

            var pagedTaskEntities = taskEntitiesQuery.ToPagedList(pageNumber, pageSize);

            var pagedTaskDtos = _mapper.Map< PagedList<TaskDto> >(pagedTaskEntities);

            return pagedTaskDtos;
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
            var taskEntity = await _tripFlipDbContext.Tasks
                .FindAsync(updateTaskDto.Id);

            ValidateTaskEntityNotNull(taskEntity);

            taskEntity.Description = updateTaskDto.Description;
            taskEntity.PriorityLevel = _mapper.Map<Domain.Entities.Enums.TaskPriorityLevel>(updateTaskDto.PriorityLevel);
            taskEntity.IsCompleted = updateTaskDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdatePriorityAsync(UpdateTaskPriorityDto updateTaskPriorityDto)
        {
            var taskEntity = await _tripFlipDbContext.Tasks
                .FindAsync(updateTaskPriorityDto.Id);

            ValidateTaskEntityNotNull(taskEntity);

            taskEntity.PriorityLevel = _mapper
                .Map<Domain.Entities.Enums.TaskPriorityLevel>(updateTaskPriorityDto.PriorityLevel);

            await _tripFlipDbContext.SaveChangesAsync();
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdateCompletenessAsync(UpdateTaskCompletenessDto updateTaskCompletenessDto)
        {
            var taskEntity = await _tripFlipDbContext.Tasks
                .FindAsync(updateTaskCompletenessDto.Id);

            ValidateTaskEntityNotNull(taskEntity);

            taskEntity.IsCompleted = updateTaskCompletenessDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var taskEntity = await _tripFlipDbContext.Tasks
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskEntity is null)
            {
                throw new NotFoundException(ErrorConstants.TaskNotFound);
            }

            _tripFlipDbContext.Tasks.Remove(taskEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task SetTaskAssignees(TaskAssigneesDto taskAssigneesDto)
        {
            // Validate task to set assignees exists.
            var taskToSetAssignees = await _tripFlipDbContext
                .Tasks
                .AsNoTracking()
                .Include(task => task.TaskList)
                .Include(task => task.TaskAssignees)
                .SingleOrDefaultAsync(task => task.Id == taskAssigneesDto.TaskId);

            EntityValidationHelper
                .ValidateEntityNotNull(taskToSetAssignees, ErrorConstants.TaskNotFound);

            // Get current route id.
            var currentRouteId = taskToSetAssignees.TaskList.RouteId;

            // Validate current user has route editor role.
            await EntityValidationHelper.ValidateCurrentUserIsRouteEditorAsync(
                _currentUserService, _tripFlipDbContext, currentRouteId);

            // Validate route subscribers exist and has same route id as task.
            var currentRouteSubscriberIds = _tripFlipDbContext
                .RouteSubscribers
                .AsNoTracking()
                .Where(subscriber => subscriber.RouteId == currentRouteId)
                .Select(subscriber => subscriber.Id);

            var currentRouteSubscribersNotExist = taskAssigneesDto
                .RouteSubscriberIds
                .Any(id => !currentRouteSubscriberIds.Contains(id));

            if (currentRouteSubscribersNotExist)
            {
                throw new NotFoundException(ErrorConstants.RouteSubscribersNotFound);
            }

            // Remove task's current set of assignees.
            _tripFlipDbContext.TaskAssignees.RemoveRange(
                taskToSetAssignees.TaskAssignees);

            // Add requested set of assignees to task.
            bool collectionHasAssigneesToAdd = taskAssigneesDto.RouteSubscriberIds.Any();
            if (collectionHasAssigneesToAdd)
            {
                var assigneesToAdd = taskAssigneesDto
                    .RouteSubscriberIds
                    .Select(subscriberId => new TaskAssigneeEntity()
                    {
                        Task = taskToSetAssignees,
                        RouteSubscriberId = subscriberId
                    });

                _tripFlipDbContext.TaskAssignees.AddRange(assigneesToAdd);
            }

            await _tripFlipDbContext.SaveChangesAsync();
        }

        private void ValidateTaskEntityNotNull(TaskEntity taskEntity)
        {

            if (taskEntity is null)
            {
                throw new NotFoundException(ErrorConstants.TaskNotFound);
            }

        }

        private void ValidateTaskListEntityNotNull(TaskListEntity taskListEntity)
        {

            if (taskListEntity is null)
            {
                throw new NotFoundException(ErrorConstants.TaskListNotFound);
            }

        }
    }
}

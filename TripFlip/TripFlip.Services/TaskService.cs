using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskDtos;
using TripFlip.Services.Enums;
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

            EntityValidationHelper
                .ValidateEntityNotNull(taskListEntity, ErrorConstants.TaskListNotFound);

            // Validate current user has route 'Admin' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: taskListEntity.RouteId,
                routeRoleToValidate: RouteRoles.Admin,
                errorMessage: ErrorConstants.NotRouteAdmin);

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

            EntityValidationHelper
                .ValidateEntityNotNull(taskEntity, ErrorConstants.TaskNotFound);

            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdateAsync(UpdateTaskDto updateTaskDto)
        {
            var taskEntity = await _tripFlipDbContext
                .Tasks
                .Include(task => task.TaskList)
                .SingleOrDefaultAsync(task => task.Id == updateTaskDto.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(taskEntity, ErrorConstants.TaskNotFound);

            // Validate current user has route 'Editor' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: taskEntity.TaskList.RouteId,
                routeRoleToValidate: RouteRoles.Editor,
                errorMessage: ErrorConstants.NotRouteEditor);

            taskEntity.Description = updateTaskDto.Description;
            taskEntity.PriorityLevel = _mapper.Map<Domain.Entities.Enums.TaskPriorityLevel>(updateTaskDto.PriorityLevel);
            taskEntity.IsCompleted = updateTaskDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdatePriorityAsync(UpdateTaskPriorityDto updateTaskPriorityDto)
        {
            var taskEntity = await _tripFlipDbContext
                .Tasks
                .Include(task => task.TaskList)
                .SingleOrDefaultAsync(task => task.Id == updateTaskPriorityDto.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(taskEntity, ErrorConstants.TaskNotFound);

            // Validate current user has route 'Editor' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: taskEntity.TaskList.RouteId,
                routeRoleToValidate: RouteRoles.Editor,
                errorMessage: ErrorConstants.NotRouteEditor);

            taskEntity.PriorityLevel = _mapper
                .Map<Domain.Entities.Enums.TaskPriorityLevel>(updateTaskPriorityDto.PriorityLevel);

            await _tripFlipDbContext.SaveChangesAsync();
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task<TaskDto> UpdateCompletenessAsync(UpdateTaskCompletenessDto updateTaskCompletenessDto)
        {
            var taskEntity = await _tripFlipDbContext
                .Tasks
                .Include(task => task.TaskList)
                .SingleOrDefaultAsync(task => task.Id == updateTaskCompletenessDto.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(taskEntity, ErrorConstants.TaskNotFound);

            // Validate current user has route 'Editor' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: taskEntity.TaskList.RouteId,
                routeRoleToValidate: RouteRoles.Editor,
                errorMessage: ErrorConstants.NotRouteEditor);

            taskEntity.IsCompleted = updateTaskCompletenessDto.IsCompleted;

            await _tripFlipDbContext.SaveChangesAsync();
            var taskDto = _mapper.Map<TaskDto>(taskEntity);

            return taskDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var taskEntity = await _tripFlipDbContext
                .Tasks
                .Include(task => task.TaskList)
                .SingleOrDefaultAsync(task => task.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(taskEntity, ErrorConstants.TaskNotFound);

            // Validate current user has route 'Admin' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: taskEntity.TaskList.RouteId,
                routeRoleToValidate: RouteRoles.Admin,
                errorMessage: ErrorConstants.NotRouteAdmin);

            _tripFlipDbContext.Tasks.Remove(taskEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        public async Task SetTaskAssignees(TaskAssigneesDto taskAssigneesDto)
        {
            // Validate task to set assignees exists.
            var taskToSetAssignees = await _tripFlipDbContext
                .Tasks
                .AsNoTracking()
                .Include(task => task.TaskAssignees)
                .Include(task => task.TaskList)
                .ThenInclude(taskList => taskList.Route)
                .ThenInclude(route => route.RouteSubscribers)
                .SingleOrDefaultAsync(task => task.Id == taskAssigneesDto.TaskId);

            EntityValidationHelper
                .ValidateEntityNotNull(taskToSetAssignees, ErrorConstants.TaskNotFound);

            // Get current task route.
            var currentTaskRoute = taskToSetAssignees.TaskList.Route;

            // Validate current user has route 'Editor' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: currentTaskRoute.Id,
                routeRoleToValidate: RouteRoles.Editor,
                errorMessage: ErrorConstants.NotRouteEditor);

            // Validate route subscribers exist and has same route id as task.
            var currentRouteSubscriberIds = currentTaskRoute
                .RouteSubscribers
                .Select(subscriber => subscriber.Id);

            var allGivenCurrentRouteSubscribersExist = taskAssigneesDto
                .RouteSubscriberIds
                .All(id => currentRouteSubscriberIds.Contains(id));

            if (!allGivenCurrentRouteSubscribersExist)
            {
                throw new ArgumentException(ErrorConstants.RouteSubscribersNotFound);
            }

            // Validate new task assignees set is not the same with old one.
            var currentTaskAssigneeIds = taskToSetAssignees
                .TaskAssignees
                .Select(assignee => assignee.RouteSubscriberId);

            var currentTaskHasSameTaskAssignees = taskAssigneesDto
                .RouteSubscriberIds
                .All(id => currentTaskAssigneeIds.Contains(id));

            if (currentTaskHasSameTaskAssignees)
            {
                return;
            }

            // Remove task's current set of assignees.
            _tripFlipDbContext.TaskAssignees.RemoveRange(
                taskToSetAssignees.TaskAssignees);

            // Add requested set of assignees to task.
            var assigneesToAdd = taskAssigneesDto
                .RouteSubscriberIds
                .Select(subscriberId => new TaskAssigneeEntity()
                {
                    TaskId = taskToSetAssignees.Id,
                    RouteSubscriberId = subscriberId
                });

            await _tripFlipDbContext.TaskAssignees.AddRangeAsync(assigneesToAdd);

            await _tripFlipDbContext.SaveChangesAsync();
        }
    }
}

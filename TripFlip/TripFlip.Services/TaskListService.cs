using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.CustomExceptions;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskListDtos;
using TripFlip.Services.Enums;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.Services.Interfaces.Helpers.Extensions;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class TaskListService : ITaskListService
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
        public TaskListService(TripFlipDbContext tripFlipDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<TaskListDto> CreateAsync(CreateTaskListDto createTaskListDto)
        {
            await ValidateRouteExistsAsync(createTaskListDto.RouteId);

            // Validate current user has route 'Admin' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: createTaskListDto.RouteId,
                routeRoleToValidate: RouteRoles.Admin,
                errorMessage: ErrorConstants.NotRouteAdmin);

            var taskListEntity = _mapper.Map<TaskListEntity>(createTaskListDto);

            await _tripFlipDbContext.TaskLists.AddAsync(taskListEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var taskListDto = _mapper.Map<TaskListDto>(taskListEntity);

            return taskListDto;
        }

        public async Task<PagedList<TaskListDto>> GetAllByRouteIdAsync(int routeId,
            string searchString,
            PaginationDto paginationDto)
        {
            var routeExists = await _tripFlipDbContext
                .Routes
                .AnyAsync(routeEntity => routeEntity.Id == routeId);

            if (!routeExists)
            {
                throw new NotFoundException(ErrorConstants.TaskListNotFound);
            }

            var taskListEntitiesQuery = _tripFlipDbContext
                .TaskLists
                .AsNoTracking()
                .Where(taskListEntity => taskListEntity.RouteId == routeId);

            if (!string.IsNullOrEmpty(searchString))
            {
                taskListEntitiesQuery =
                    taskListEntitiesQuery
                        .Where(taskEntity => taskEntity.Title
                            .Contains(searchString));
            }

            var pageNumber = paginationDto.PageNumber ?? 1;
            var pageSize = paginationDto.PageSize ?? await taskListEntitiesQuery.CountAsync();

            var pagedTaskListEntities = taskListEntitiesQuery.ToPagedList(pageNumber, pageSize);

            var pagedTaskListDtos = _mapper.Map< PagedList<TaskListDto> >(pagedTaskListEntities);

            return pagedTaskListDtos;
        }

        public async Task<TaskListDto> GetByIdAsync(int id)
        {
            var taskListEntity = await _tripFlipDbContext.TaskLists
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(taskListEntity, ErrorConstants.TaskListNotFound);

            var taskListDto = _mapper.Map<TaskListDto>(taskListEntity);

            return taskListDto;
        }

        public async Task<TaskListDto> UpdateAsync(UpdateTaskListDto updateTaskListDto)
        {
            var taskListEntity = await _tripFlipDbContext.TaskLists
                .FindAsync(updateTaskListDto.Id);

            EntityValidationHelper
                .ValidateEntityNotNull(taskListEntity, ErrorConstants.TaskListNotFound);

            // Validate current user has route 'Editor' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: taskListEntity.RouteId,
                routeRoleToValidate: RouteRoles.Editor,
                errorMessage: ErrorConstants.NotRouteEditor);

            taskListEntity.Title = updateTaskListDto.Title;

            await _tripFlipDbContext.SaveChangesAsync();
            var taskListDto = _mapper.Map<TaskListDto>(taskListEntity);

            return taskListDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var taskListEntity = await _tripFlipDbContext
                .TaskLists
                .SingleOrDefaultAsync(t => t.Id == id);

            EntityValidationHelper
                .ValidateEntityNotNull(taskListEntity, ErrorConstants.TaskListNotFound);

            // Validate current user has route 'Admin' role.
            await EntityValidationHelper.ValidateCurrentUserRouteRoleAsync(
                currentUserService: _currentUserService,
                tripFlipDbContext: _tripFlipDbContext,
                routeId: taskListEntity.RouteId,
                routeRoleToValidate: RouteRoles.Admin,
                errorMessage: ErrorConstants.NotRouteAdmin);

            _tripFlipDbContext.TaskLists.Remove(taskListEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Validates whether route with specified id exists or not.
        /// Throws an exception if route with specified id doesn't exist.
        /// </summary>
        /// <param name="routeId">Route id.</param>
        private async Task ValidateRouteExistsAsync(int routeId)
        {
            var routeEntity = await _tripFlipDbContext.Routes
                .AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == routeId);

            if (routeEntity is null)
            {
                throw new NotFoundException(ErrorConstants.AddingTaskListToNotExistingRoute);
            }

        }

        private void ValidateTaskListEntityNotNull(TaskListEntity taskList)
        {

            if (taskList is null)
            {
                throw new NotFoundException(ErrorConstants.TaskListNotFound);
            }

        }
    }
}

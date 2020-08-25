using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.TaskListDtos;
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

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public TaskListService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
        }

        public async Task<TaskListDto> CreateAsync(CreateTaskListDto createTaskListDto)
        {
            await ValidateRouteExistsAsync(createTaskListDto.RouteId);

            var taskListEntity = _mapper.Map<TaskListEntity>(createTaskListDto);

            await _tripFlipDbContext.TaskLists.AddAsync(taskListEntity);
            await _tripFlipDbContext.SaveChangesAsync();

            var createdTaskListDto = _mapper.Map<TaskListDto>(taskListEntity);

            return createdTaskListDto;
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
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
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

            var taskListsPagedList = taskListEntitiesQuery.ToPagedList(pageNumber, pageSize);

            var taskListDtos = _mapper.Map< PagedList<TaskListDto> >(taskListsPagedList);

            return taskListDtos;
        }

        public async Task<TaskListDto> GetByIdAsync(int id)
        {
            var taskList = await _tripFlipDbContext.TaskLists.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            var taskListDto = _mapper.Map<TaskListDto>(taskList);

            return taskListDto;
        }

        public async Task<TaskListDto> UpdateAsync(UpdateTaskListDto updateTaskListDto)
        {
            var taskLsitToUpdateEntity = await _tripFlipDbContext.TaskLists
                .FindAsync(updateTaskListDto.Id);

            if (taskLsitToUpdateEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            taskLsitToUpdateEntity.Title = updateTaskListDto.Title;

            await _tripFlipDbContext.SaveChangesAsync();
            var updatedTaskListDto = _mapper.Map<TaskListDto>(taskLsitToUpdateEntity);

            return updatedTaskListDto;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var taskListToDelete = await _tripFlipDbContext.TaskLists.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskListToDelete is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            _tripFlipDbContext.TaskLists.Remove(taskListToDelete);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Validates whether route with specified id exists or not.
        /// Throws an exception if route with specified id doesn't exist.
        /// </summary>
        /// <param name="routeId">Route id.</param>
        private async Task ValidateRouteExistsAsync(int routeId)
        {
            var route = await _tripFlipDbContext.Routes.AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == routeId);

            if (route is null)
            {
                throw new ArgumentException(ErrorConstants.AddingTaskListToNotExistingRoute);
            }

        }
    }
}

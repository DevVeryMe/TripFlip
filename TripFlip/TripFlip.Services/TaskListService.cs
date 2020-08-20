using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO.TaskListDtos;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class TaskListService : ITaskListService
    {
        private readonly FlipTripDbContext _flipTripDbContext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor. Initializes _flipTripDbContext and _mapper fields.
        /// </summary>
        /// <param name="flipTripDbContext">FlipTripDbContext instance.</param>
        /// <param name="mapper">IMapper instance.</param>
        public TaskListService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _flipTripDbContext = flipTripDbContext;
            _mapper = mapper;
        }

        public async Task<TaskListDto> CreateAsync(CreateTaskListDto taskListDto)
        {
            await ValidateRouteExists(taskListDto.RouteId);

            var taskListEntity = _mapper.Map<TaskListEntity>(taskListDto);
            taskListEntity.DateCreated = DateTimeOffset.Now;

            await _flipTripDbContext.TaskLists.AddAsync(taskListEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var createdTaskListDto = _mapper.Map<TaskListDto>(taskListEntity);

            return createdTaskListDto;
        }

        /// <summary>
        /// Validates whether route with specified id exists or not.
        /// Throws an exception if route with specified id doesn't exist.
        /// </summary>
        /// <param name="routeId">Route id.</param>
        /// <returns>Nothing.</returns>
        private async Task ValidateRouteExists(int routeId)
        {
            var route = await _flipTripDbContext.Routes.AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == routeId);

            if (route is null)
            {
                throw new ArgumentException(ErrorConstants.AddingTaskListToNotExistingRoute);
            }
        }
    }
}

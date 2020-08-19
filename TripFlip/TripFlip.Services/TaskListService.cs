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
        /// <param name="flipTripDbContext">FlipTripDbContext instance</param>
        /// <param name="mapper">IMapper instance</param>
        public TaskListService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _flipTripDbContext = flipTripDbContext;
            _mapper = mapper;
        }

        public async Task<TaskListDto> CreateAsync(TaskListDto taskListDto)
        {
            var route = await _flipTripDbContext.Routes.AsNoTracking()
                .SingleOrDefaultAsync(r => r.Id == taskListDto.RouteId);

            if (route is null)
            {
                throw new ArgumentException(ErrorConstants.AddingTaskListToNotExistingRoute);
            }

            var taskListEntity = _mapper.Map<TaskListEntity>(taskListDto);
            taskListEntity.DateCreated = DateTimeOffset.Now;

            await _flipTripDbContext.TaskLists.AddAsync(taskListEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var createdTaskListDto = _mapper.Map<TaskListDto>(taskListEntity);

            return createdTaskListDto;
        }

        public async Task<TaskListDto> GetByIdAsync(int id)
        {
            var taskList = await _flipTripDbContext.TaskLists.AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            var taskListDto = _mapper.Map<TaskListDto>(taskList);

            return taskListDto;
        }

        public async Task<TaskListDto> UpdateAsync(TaskListDto taskListDto)
        {
            var updatedTaskListEntity = _mapper.Map<TaskListEntity>(taskListDto);
            var taskLsitToUpdateEntity = await _flipTripDbContext.TaskLists
                .FindAsync(updatedTaskListEntity.Id);

            if (taskLsitToUpdateEntity is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            taskLsitToUpdateEntity.Title = updatedTaskListEntity.Title;

            await _flipTripDbContext.SaveChangesAsync();
            var updatedTaskListDto = _mapper.Map<TaskListDto>(taskLsitToUpdateEntity);

            return updatedTaskListDto;
        }
    }
}

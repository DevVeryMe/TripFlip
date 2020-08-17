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
    public class TaskListService : ITaskListService
    {
        private readonly FlipTripDbContext _flipTripDbContext;
        private readonly IMapper _mapper;

        public TaskListService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _flipTripDbContext = flipTripDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<TaskListDto> CreateAsync(TaskListDto taskListDto)
        {
            var taskListEntity = _mapper.Map<TaskListEntity>(taskListDto);
            taskListEntity.DateCreated = DateTimeOffset.Now;

            await _flipTripDbContext.TaskLists.AddAsync(taskListEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var taskToReturn = _mapper.Map<TaskListDto>(taskListEntity);

            return taskToReturn;
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
        }

        /// <inheritdoc />
        public async Task<TaskListDto> GetByIdAsync(int id)
        {
            var taskList = await _flipTripDbContext.TaskLists.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            var taskListDto = _mapper.Map<TaskListDto>(taskList);

            return taskListDto;
        }

        /// <inheritdoc />
        public async Task<TaskListDto> UpdateAsync(TaskListDto taskListDto)
        {
            var updatedTaskList = _mapper.Map<TaskListEntity>(taskListDto);
            var taskLsitToUpdate = await _flipTripDbContext.TaskLists.FindAsync(updatedTaskList.Id);

            if (taskLsitToUpdate is null)
            {
                throw new ArgumentException(ErrorConstants.TaskNotFound);
            }

            taskLsitToUpdate.Title = updatedTaskList.Title;

            await _flipTripDbContext.SaveChangesAsync();
            var taskListToReturn = _mapper.Map<TaskListDto>(taskLsitToUpdate);

            return taskListToReturn;
        }
    }
}

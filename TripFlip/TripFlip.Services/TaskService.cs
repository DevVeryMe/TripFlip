using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    public class TaskService : ITaskService
    {
        private readonly FlipTripDbContext _flipTripDbContext;
        private readonly IMapper _mapper;

        public TaskService(FlipTripDbContext flipTripDbContext, IMapper mapper)
        {
            _flipTripDbContext = flipTripDbContext;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<TaskDto> CreateAsync(TaskDto taskDto)
        {
            var taskEntity = _mapper.Map<TaskEntity>(taskDto);
            taskEntity.DateCreated = DateTimeOffset.Now;

            await _flipTripDbContext.Tasks.AddAsync(taskEntity);
            await _flipTripDbContext.SaveChangesAsync();

            var taskToReturn = _mapper.Map<TaskDto>(taskEntity);

            return taskToReturn;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TaskDto>> GetAllByTaskListIdAsync(int id)
        {
            var taskList = await _flipTripDbContext.TaskLists.FindAsync(id);

            if (taskList is null)
            {
                throw new ArgumentException(ErrorConstants.TaskListNotFound);
            }

            var tasks = await _flipTripDbContext.Tasks.Where(t => t.TaskListId == id).AsNoTracking().ToListAsync();
            var taskDtos = _mapper.Map<List<TaskDto>>(tasks);

            return taskDtos;
        }
    }
}

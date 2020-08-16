using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    public class TaskService : ITaskService
    {
        private readonly FlipTripDbContext _tripFlipDbContext;
        private readonly IMapper _mapper;

        public TaskService(FlipTripDbContext tripFlipDbContext, IMapper mapper)
        {
            _tripFlipDbContext = tripFlipDbContext;
            _mapper = mapper;
        }

        public void CreateTask(TaskDto task)
        {
            var taskEntity = _mapper.Map<TaskEntity>(task);
            _tripFlipDbContext.Tasks.Add(taskEntity);
        }

        public void DeleteTask(int id)
        {
        }

        public TaskDto GetTask(int id)
        {
            return null;
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            return null;
        }

        public void UpdateTask(TaskDto task)
        {
        }
    }
}

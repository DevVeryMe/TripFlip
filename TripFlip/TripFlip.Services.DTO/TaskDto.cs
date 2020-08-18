﻿using System;
using System.Collections.Generic;
using System.Text;
using TripFlip.Services.DTO.Enums;

namespace TripFlip.Services.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public TaskPriorityLevel PriorityLevel { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public bool IsCompleted { get; set; }

        public int TaskListId { get; set; }
    }
}
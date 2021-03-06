﻿using System;

namespace TripFlip.Services.Dto.TaskListDtos
{
    public class TaskListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public int RouteId { get; set; }
    }
}

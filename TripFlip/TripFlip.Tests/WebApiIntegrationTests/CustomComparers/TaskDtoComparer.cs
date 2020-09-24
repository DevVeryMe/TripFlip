using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.TaskDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class TaskDtoComparer : IComparer<TaskDto>
    {
        public int Compare(TaskDto x, TaskDto y)
        {
            if (x is null && y is null)
            {
                return 0;
            }

            if (x is null)
            {
                return -1;
            }

            if (y is null)
            {
                return 1;
            }

            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }

            if (x.TaskListId.CompareTo(y.TaskListId) != 0)
            {
                return x.TaskListId.CompareTo(y.TaskListId);
            }

            if (string.Compare(x.Description, y.Description, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Description, y.Description, StringComparison.Ordinal);
            }

            if (x.IsCompleted.CompareTo(y.IsCompleted) != 0)
            {
                return x.IsCompleted.CompareTo(y.IsCompleted);
            }

            if (x.PriorityLevel.CompareTo(y.PriorityLevel) != 0)
            {
                return x.PriorityLevel.CompareTo(y.PriorityLevel);
            }

            if (DateTimeOffset.Compare(x.DateCreated, y.DateCreated) != 0)
            {
                return DateTimeOffset.Compare(x.DateCreated, y.DateCreated);
            }

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Dto.TaskListDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class TaskListWithTasksDtoComparer : IComparer<TaskListWithTasksDto>
    {
        public int Compare(TaskListWithTasksDto x, TaskListWithTasksDto y)
        {
            if (x == null && y == null)
            {
                return 0;
            }

            if (x == null)
            {
                return -1;
            }

            if (y == null)
            {
                return 1;
            }

            if (x.Id.CompareTo(y.Id) != 0)
            {
                return x.Id.CompareTo(y.Id);
            }

            if (string.Compare(x.Title, y.Title, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            }

            #region Comparing nested objects
            if (x.Tasks is null && !(y.Tasks is null))
            {
                return -1;
            }

            if (!(x.Tasks is null) && y.Tasks is null)
            {
                return 1;
            }

            if (!(x.Tasks is null) && !(y.Tasks is null))
            {
                var xTasksArray = x.Tasks.ToArray();
                var yTasksArray = y.Tasks.ToArray();

                if (xTasksArray.Length < yTasksArray.Length)
                {
                    return -1;
                }

                if (xTasksArray.Length > yTasksArray.Length)
                {
                    return 1;
                }

                var comparer = new TaskWithAssigneesDtoComparer();

                for (int i = 0; i < xTasksArray.Length; i++)
                {
                    int result = comparer.Compare(xTasksArray[i], yTasksArray[i]);

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }
            #endregion

            return 0;
        }
    }
}

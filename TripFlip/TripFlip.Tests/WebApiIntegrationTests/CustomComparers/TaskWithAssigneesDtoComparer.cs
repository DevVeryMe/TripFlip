using System;
using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Dto.TaskDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class TaskWithAssigneesDtoComparer : IComparer<TaskWithAssigneesDto>
    {
        public int Compare(TaskWithAssigneesDto x, TaskWithAssigneesDto y)
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

            #region Comparing nested objects
            if (x.TaskAssignees is null && !(y.TaskAssignees is null))
            {
                return -1;
            }

            if (!(x.TaskAssignees is null) && y.TaskAssignees is null)
            {
                return 1;
            }

            if (!(x.TaskAssignees is null) && !(y.TaskAssignees is null))
            {
                var xTaskAssigneesArray = x.TaskAssignees.ToArray();
                var yTaskAssigneesArray = y.TaskAssignees.ToArray();

                if (xTaskAssigneesArray.Length < yTaskAssigneesArray.Length)
                {
                    return -1;
                }

                if (xTaskAssigneesArray.Length > yTaskAssigneesArray.Length)
                {
                    return 1;
                }

                for (int i = 0; i < xTaskAssigneesArray.Length; i++)
                {
                    if (xTaskAssigneesArray[i].Id.CompareTo(yTaskAssigneesArray[i].Id) != 0)
                    {
                        return xTaskAssigneesArray[i].Id.CompareTo(yTaskAssigneesArray[i].Id);
                    }

                    if (xTaskAssigneesArray[i].TripSubscriberId
                        .CompareTo(yTaskAssigneesArray[i].TripSubscriberId) != 0)
                    {
                        return xTaskAssigneesArray[i].Id.CompareTo(yTaskAssigneesArray[i].TripSubscriberId);
                    }
                }
            }
            #endregion

            return 0;
        }
    }
}

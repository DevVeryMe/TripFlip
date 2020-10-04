using System;
using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Dto.RouteDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class RouteWithPointsItemAndTaskListsDtoComparer
        : IComparer<RouteWithPointsItemAndTaskListsDto>
    {
        public int Compare(
            RouteWithPointsItemAndTaskListsDto x, 
            RouteWithPointsItemAndTaskListsDto y)
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

            if (string.Compare(x.Title, y.Title, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            }

            #region Comparing nested objects
            if (x.RoutePoints is null && !(y.RoutePoints is null))
            {
                return -1;
            }

            if (!(x.RoutePoints is null) && y.RoutePoints is null)
            {
                return 1;
            }

            if (!(x.RoutePoints is null) && !(y.RoutePoints is null))
            {
                var xRoutePointsArray = x.RoutePoints.ToArray();
                var yRoutePointsArray = y.RoutePoints.ToArray();

                if (xRoutePointsArray.Length < yRoutePointsArray.Length)
                {
                    return -1;
                }

                if (xRoutePointsArray.Length > yRoutePointsArray.Length)
                {
                    return 1;
                }

                var routePointDtoComparer = new RoutePointDtoComparer();

                for (int i = 0; i < xRoutePointsArray.Length; i++)
                {
                    int result = routePointDtoComparer.Compare(
                        xRoutePointsArray[i], yRoutePointsArray[i]);

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }

            if (x.ItemLists is null && !(y.ItemLists is null))
            {
                return -1;
            }

            if (!(x.ItemLists is null) && y.ItemLists is null)
            {
                return 1;
            }

            if (!(x.ItemLists is null) && !(y.ItemLists is null))
            {
                var xItemListsArray = x.ItemLists.ToArray();
                var yItemListsArray = y.ItemLists.ToArray();

                if (xItemListsArray.Length < yItemListsArray.Length)
                {
                    return -1;
                }

                if (xItemListsArray.Length > yItemListsArray.Length)
                {
                    return 1;
                }

                var itemListWithItemsDtoComparer = new ItemListWithItemsDtoComparer();

                for (int i = 0; i < xItemListsArray.Length; i++)
                {
                    int result = itemListWithItemsDtoComparer.Compare(
                        xItemListsArray[i], yItemListsArray[i]);

                    if (result != 0)
                    {
                        return result;
                    }
                }
            }

            if (x.TaskLists is null && !(y.TaskLists is null))
            {
                return -1;
            }

            if (!(x.TaskLists is null) && y.TaskLists is null)
            {
                return 1;
            }

            if (!(x.TaskLists is null) && !(y.TaskLists is null))
            {
                var xTaskListsArray = x.TaskLists.ToArray();
                var yTaskListsArray = y.TaskLists.ToArray();

                if (xTaskListsArray.Length < yTaskListsArray.Length)
                {
                    return -1;
                }

                if (xTaskListsArray.Length > yTaskListsArray.Length)
                {
                    return 1;
                }

                var taskListWithTasksDtoComparer = new TaskListWithTasksDtoComparer();

                for (int i = 0; i < xTaskListsArray.Length; i++)
                {
                    int result = taskListWithTasksDtoComparer.Compare(
                        xTaskListsArray[i], yTaskListsArray[i]);

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

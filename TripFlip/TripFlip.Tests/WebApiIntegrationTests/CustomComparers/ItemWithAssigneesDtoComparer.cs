using System;
using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Dto.ItemDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class ItemWithAssigneesDtoComparer : IComparer<ItemWithAssigneesDto>
    {
        public int Compare(ItemWithAssigneesDto x, ItemWithAssigneesDto y)
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

            if (string.Compare(x.Comment, y.Comment, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Comment, y.Comment, StringComparison.Ordinal);
            }

            if (string.Compare(x.Quantity, y.Quantity, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Quantity, y.Quantity, StringComparison.Ordinal);
            }

            if (x.IsCompleted.CompareTo(y.IsCompleted) != 0)
            {
                return x.IsCompleted.CompareTo(y.IsCompleted);
            }

            #region Comparing nested objects
            if (x.ItemAssignees is null && !(y.ItemAssignees is null))
            {
                return -1;
            }

            if (!(x.ItemAssignees is null) && y.ItemAssignees is null)
            {
                return 1;
            }

            if (!(x.ItemAssignees is null) && !(y.ItemAssignees is null))
            {
                var xItemAssigneesArray = x.ItemAssignees.ToArray();
                var yItemAssigneesArray = y.ItemAssignees.ToArray();

                if (xItemAssigneesArray.Length < yItemAssigneesArray.Length)
                {
                    return -1;
                }

                if (xItemAssigneesArray.Length > yItemAssigneesArray.Length)
                {
                    return 1;
                }

                for (int i = 0; i < xItemAssigneesArray.Length; i++)
                {
                    if (xItemAssigneesArray[i].Id.CompareTo(yItemAssigneesArray[i].Id) != 0)
                    {
                        return xItemAssigneesArray[i].Id.CompareTo(yItemAssigneesArray[i].Id);
                    }

                    if (xItemAssigneesArray[i].TripSubscriberId
                        .CompareTo(yItemAssigneesArray[i].TripSubscriberId) != 0)
                    {
                        return xItemAssigneesArray[i].Id.CompareTo(yItemAssigneesArray[i].TripSubscriberId);
                    }
                }
            }
            #endregion

            return 0;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using TripFlip.Services.Dto.ItemListDtos;

namespace WebApiIntegrationTests.CustomComparers
{
    public class ItemListDtoComparer : IComparer<ItemListDto>
    {
        public int Compare(ItemListDto x, ItemListDto y)
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

            if (x.RouteId.CompareTo(y.RouteId) != 0)
            {
                return x.RouteId.CompareTo(y.RouteId);
            }

            if (string.Compare(x.Title, y.Title, StringComparison.Ordinal) != 0)
            {
                return string.Compare(x.Title, y.Title, StringComparison.Ordinal);
            }

            if (DateTimeOffset.Compare(x.DateCreated, y.DateCreated) != 0)
            {
                return DateTimeOffset.Compare(x.DateCreated, y.DateCreated);
            }

            return 0;
        }

        public int CompareUpdated(ItemListDto x, UpdateItemListDto y)
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

            return 0;
        }
    }
}

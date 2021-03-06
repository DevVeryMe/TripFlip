﻿using System;
using System.Collections.Generic;

namespace TripFlip.Services.Interfaces.Helpers
{
    /// <summary>
    /// List with meta information about number of pages, page size, current page number.
    /// </summary>
    /// <typeparam name="T">Any entity to store.</typeparam>
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;

        public bool HasNext => CurrentPage < TotalPages;

        public PagedList() { }

        /// <summary>
        /// Initializes total count, page size, page number and total number of pages, adds items to list.
        /// </summary>
        /// <param name="items">IEnumerable implementation with items.</param>
        /// <param name="totalCount">Total count of items.</param>
        /// <param name="pageNumber">Number of selected page.</param>
        /// <param name="pageSize">Size of pages.</param>
        public PagedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = pageSize == 0 ? 
                pageNumber :
                (int)Math.Ceiling(totalCount / (double)pageSize);
        }
	}
}

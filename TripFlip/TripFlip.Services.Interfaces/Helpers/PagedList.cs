﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TripFlip.Services.Interfaces.Helpers
{
    /// <summary>
    /// List with meta information about pages number, page size, current page.
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

        public PagedList()
        {
        }

        /// <summary>
        /// Initializes total count, page size, page number and total number of pages, adds items to list.
        /// </summary>
        /// <param name="items">IEnumerable implementation with items.</param>
        /// <param name="count">Total count of items.</param>
        /// <param name="pageNumber">Number of selected page.</param>
        /// <param name="pageSize">Size of pages.</param>
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        /// <summary>
        /// Converts IQueryable implementation with items to PagedList.
        /// </summary>
        /// <param name="source">Implementation of IQueryable</param>
        /// <param name="pageNumber">Number of selected page.</param>
        /// <param name="pageSize">Size of pages.</param>
        /// <returns>Paged list.</returns>
        public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
	}
}

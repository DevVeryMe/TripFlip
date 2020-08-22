using System.Collections.Generic;
using System.Linq;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services.Interfaces.HelpersExtensions
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// Converts IEnumerable implementation with items to PagedList.
        /// </summary>
        /// <param name="source">Implementation of IEnumerable</param>
        /// <param name="pageNumber">Number of selected page.</param>
        /// <param name="pageSize">Size of pages.</param>
        /// <returns>Paged list.</returns>
        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, pageNumber, pageSize);
        }
    }
}

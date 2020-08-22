using System.Linq;

namespace TripFlip.Services.Interfaces.Helpers.Extensions
{
    public static class PagedListExtensions
    {
        /// <summary>
        /// Converts IQueryable implementation with items to PagedList.
        /// </summary>
        /// <param name="source">Implementation of IQueryable</param>
        /// <param name="pageNumber">Number of selected page.</param>
        /// <param name="pageSize">Size of pages.</param>
        /// <returns>Paged list.</returns>

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var count = source.Count();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}

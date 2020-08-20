using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.Services.DTO.ItemListDtos;

namespace TripFlip.Services.Interfaces
{
    /// <summary>
    /// Interface that describes supported CRUD operations with ItemList entity.
    /// </summary>
    public interface IITemListService
    {
        /// <summary>
        /// Returns ItemList with the given Id.
        /// </summary>
        /// <returns>Object that represents the database entry with the given Id.</returns>
        Task<ResultItemListDto> GetByIdAsync(int itemListId);

        /// <summary>
        /// Returns all ItemLists with the given Route Id.
        /// </summary>
        /// <returns>Object that represents the <see cref="IEnumerable{ResultRouteDto}"/> collection of database entries with the given Route Id.</returns>
        Task<IEnumerable<ResultItemListDto>> GetAllByRouteIdAsync(int routeId);
    }
}

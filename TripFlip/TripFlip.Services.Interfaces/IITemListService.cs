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
        /// <param name="itemListId">Id of ItemList.</param>
        /// <returns>Object that represents the database entry with the given Id.</returns>
        Task<ResultItemListDto> GetByIdAsync(int itemListId);

        /// <summary>
        /// Returns all ItemLists with the given Route Id.
        /// </summary>
        /// <param name="routeId">Id of Route.</param>
        /// <returns>Object that represents the <see cref="IEnumerable{ResultRouteDto}"/> collection of database entries with the given Route Id.</returns>
        Task<IEnumerable<ResultItemListDto>> GetAllByRouteIdAsync(int routeId);

        /// <summary>
        /// Creates new ItemList with the given <see cref="CreateItemListDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <param name="createItemListDto">New ItemList object to be added to database.</param>
        /// <returns><see cref="ResultItemListDto"/> object that represents the new entry that was added to database.</returns>
        Task<ResultItemListDto> CreateAsync(CreateItemListDto createItemListDto);

        /// <summary>
        /// Updates the ItemList with the given <see cref="UpdateItemListDto"/> object. Applies changes to database asynchronously.
        /// </summary>
        /// <param name="updateItemListDto">Object that represents changes to be made to ItemList.</param>
        /// <returns><see cref="ResultItemListDto"/> object that represents the updated entry in database.</returns>
        Task<ResultItemListDto> UpdateAsync(UpdateItemListDto updateItemListDto);
    }
}

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
    }
}

using System.Collections.Generic;
using TripFlip.Services.Dto.ItemDtos;

namespace TripFlip.Services.Dto.ItemListDtos
{
    public class ItemListWithItemsDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<ItemWithoutListIdDto> Items { get; set; }
    }
}
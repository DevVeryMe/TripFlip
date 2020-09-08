using System.Collections.Generic;
using TripFlip.Services.Dto.ItemDtos;

namespace TripFlip.Services.Dto.ItemListDtos
{
    public class ItemListWithIncludesDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<ItemDto> Items { get; set; }
    }
}
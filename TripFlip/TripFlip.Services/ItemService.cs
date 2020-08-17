using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.Interfaces;

namespace TripFlip.Services
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly FlipTripDbContext _flipTripDbContext;

        public ItemService(IMapper mapper, FlipTripDbContext flipTripDbContext)
        {
            _mapper = mapper;
            _flipTripDbContext = flipTripDbContext;
        }

        /// <inheritdoc />
        public async Task<ItemDto> CreateAsync(CreateItemDto createItemDto)
        {
            var item = _mapper.Map<ItemEntity>(createItemDto);

            await _flipTripDbContext.Items.AddAsync(item);
            await _flipTripDbContext.SaveChangesAsync();

            var itemToReturnDto = _mapper.Map<ItemDto>(item);

            return itemToReturnDto;
        }

        /// <inheritdoc />
        public Task<IEnumerable<ItemDto>> GetAllAsync(int itemListId)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ItemDto> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<ItemDto> UpdateAsync(CreateItemDto createItemDto)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

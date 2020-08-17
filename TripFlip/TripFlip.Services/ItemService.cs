using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TripFlip.DataAccess;
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

        public Task<CreateItemDto> CreateAsync(CreateItemDto createItemDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CreateItemDto>> GetAllAsync(int itemListId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CreateItemDto> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<CreateItemDto> UpdateAsync(CreateItemDto createItemDto)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}

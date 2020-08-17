using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TripFlip.Services.DTO.ItemDtos;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.ItemViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        private readonly IMapper _mapper;

        public ItemsController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateItemViewModel createItemViewModel)
        {
            var createItemDto = _mapper.Map<CreateItemDto>(createItemViewModel);
            var itemDto = await _itemService.CreateAsync(createItemDto);
            var itemViewModel = _mapper.Map<ItemViewModel>(itemDto);

            return Ok(itemViewModel);
        }
    }
}

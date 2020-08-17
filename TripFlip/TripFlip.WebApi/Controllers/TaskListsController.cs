using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public TaskListsController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TripFlip.Services.Interfaces;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        private readonly IGreetingService _greetingService;

        public GreetingsController(IGreetingService greetingService)
        {
            _greetingService = greetingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string greetingString = _greetingService.GetGreeting();
            return Ok(greetingString);
        }
    }
}

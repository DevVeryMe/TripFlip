using AutoMapper;
using GoogleAuthentication.Services.Interfaces;
using GoogleAuthentication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoogleAuthentication.WebApi.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> SingInWithGoogle()
        {
            var authenticatedUserDto = await _userService.SingInWithGoogle();

            var authenticatedUserViewModel = _mapper.Map<AuthenticatedUserViewModel>(authenticatedUserDto);

            return Ok(authenticatedUserViewModel);
        }
    }
}

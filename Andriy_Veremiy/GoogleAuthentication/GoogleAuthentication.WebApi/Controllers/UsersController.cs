using AutoMapper;
using GoogleAuthentication.Services.Interfaces;
using GoogleAuthentication.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace GoogleAuthentication.WebApi.Controllers
{
    [ApiController]
    [Authorize]
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

        [AllowAnonymous]
        [HttpPost("signin-google")]
        public async Task<IActionResult> SingInWithGoogle()
        {
            var authenticatedUserDto = await _userService.SignInWithGoogle();

            var authenticatedUserViewModel = _mapper.Map<AuthenticatedUserViewModel>(authenticatedUserDto);

            return Ok(authenticatedUserViewModel);
        }

        [HttpPost("switch-google-account")]
        public async Task<IActionResult> SwitchGoogleAccount()
        {
            var authenticatedUserDto = await _userService.SwitchGoogleAccount();

            var authenticatedUserViewModel = _mapper.Map<AuthenticatedUserViewModel>(authenticatedUserDto);

            return Ok(authenticatedUserViewModel);
        }
    }
}

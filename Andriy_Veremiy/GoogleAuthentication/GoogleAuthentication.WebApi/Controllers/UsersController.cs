using System;
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

        /// <summary>
        /// Initializes user service and automapper.
        /// </summary>
        /// <param name="userService">IUserService instance.</param>
        /// <param name="mapper">IMapper instance.</param>
        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Authorizes user with Google account.
        /// </summary>
        /// <returns>Authenticated user view model containing user email and JWT.</returns>
        [AllowAnonymous]
        [HttpPost("signin-google")]
        public async Task<IActionResult> SingInWithGoogle()
        {
            var authenticatedUserDto = await _userService.SignInWithGoogle();

            var authenticatedUserViewModel = _mapper.Map<AuthenticatedUserViewModel>(authenticatedUserDto);

            return Ok(authenticatedUserViewModel);
        }

        /// <summary>
        /// Gets User by id.
        /// </summary>
        /// <param name="id">Id of User.</param>
        /// <returns>User view model that represents
        ///  user database entry.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var userDto = await _userService.GetUserById(id);

            var userViewModel = _mapper.Map<UserViewModel>(userDto);

            return Ok(userViewModel);
        }
    }
}

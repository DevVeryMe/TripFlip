using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.TripViewModels;
using TripFlip.ViewModels.UserViewModels;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Updates User.
        /// </summary>
        /// <param name="updateUserViewModel">New User data with existing User id.</param>
        /// <returns>User view model that
        /// represents the updated database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /users
        ///     {
        ///         "id": 0f8fad5b-d9cb-469f-a165-70867728950e,
        ///         "email": "sample@gmail.com",
        ///         "password": "TestPassword@1",
        ///     }
        /// </remarks>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(UpdateTripViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserViewModel updateUserViewModel)
        {
            var updateUserDto = _mapper.Map<UpdateUserDto>(updateUserViewModel);

            var userDto = await _userService.UpdateAsync(updateUserDto);

            var userViewModel = _mapper.Map<UserViewModel>(userDto);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Authorizes User.
        /// </summary>
        /// <param name="loginViewModel">User credentials to log in.</param>
        /// <returns>User view model that
        /// represents the authenticated User.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /users/authorize
        ///     {
        ///         "email": "sample@gmail.com",
        ///         "password": "TestPassword@1",
        ///     }
        /// </remarks>
        [HttpPut("authorize")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthenticatedUserViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthorizeAsync([FromBody] LoginViewModel loginViewModel)
        {
            var loginDto = _mapper.Map<LoginDto>(loginViewModel);

            var authenticatedUserDto = await _userService.AuthorizeAsync(loginDto);

            var authenticatedUserViewModel =
                _mapper.Map<AuthenticatedUserViewModel>(authenticatedUserDto);

            return Ok(authenticatedUserViewModel);
        }

        /// Deletes User.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>No content (HTTP code 204).</returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            await _userService.DeleteByIdAsync(id);

            return NoContent();
        }
    }
}

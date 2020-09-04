using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;
using TripFlip.ViewModels;
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
        /// Gets User by id.
        /// </summary>
        /// <param name="id">Id of User.</param>
        /// <returns>User view model that represents
        ///  user database entry.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var userDto = await _userService.GetByIdAsync(id);

            var userViewModel = _mapper.Map<UserViewModel>(userDto);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <param name="searchString">String to filter Users.</param>
        /// <param name="paginationViewModel">Pagination settings.</param>
        /// <returns>Paged list of User view models that
        /// represent database entries.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedList<UserViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string searchString,
            [FromQuery] PaginationViewModel paginationViewModel)
        {
            var paginationDto = _mapper.Map<PaginationDto>(paginationViewModel);

            var pagedUserDtos = await _userService.GetAllAsync(
                searchString,
                paginationDto);

            var pagedUserViewModels = _mapper.Map<PagedList<UserViewModel>>(pagedUserDtos);

            return Ok(pagedUserViewModels);
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

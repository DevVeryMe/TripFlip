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
using TripFlip.ViewModels.UserViewModels;
using TripFlip.WebApi.StringConstants;

namespace TripFlip.WebApi.Areas.SuperAdmin.Controllers
{
    [Area(AreaName.SuperAdmin)]
    [Route("api/super-admin/users")]
    [Authorize(Roles = ApplicationRoleName.SuperAdminAndAdminRoles)]
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
        /// Grants appication roles to user.
        /// </summary>
        /// <param name="grantApplicationRolesViewModel">Data with
        /// user id and application roles.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /super-admin/users/grant-application-roles
        ///     {
        ///         "userId": "17E9456A-A362-46CA-9CD4-3DC1CD2061FF",
        ///         "applicationRoleIds": [
        ///         1, 2
        ///         ]
        ///     }
        /// </remarks>
        [HttpPut("grant-application-roles")]
        [Authorize(Roles = ApplicationRoleName.SuperAdmin)]
        public async Task<IActionResult> GrantRoleAsync(
            [FromBody] GrantApplicationRolesViewModel grantApplicationRolesViewModel)
        {
            var grantApplicationRoleDto =
                _mapper.Map<GrantApplicationRolesDto>(grantApplicationRolesViewModel);

            await _userService.GrantApplicationRoleAsync(grantApplicationRoleDto);

            return Ok();
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
        /// Gets all Users by trip Id and categorized by roles.
        /// </summary>
        /// <param name="tripId">Id of a trip to find users with.</param>
        /// <returns>User view model that
        /// represent all users that are subscribed to a given trip. 
        /// All users are categorized by their trip roles.</returns>
        [HttpGet("get-by-trip/{tripId}")]
        [ProducesResponseType(typeof(UsersByTripAndCategorizedByRoleViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByTripIdAndCategorizeByRoleAsync(
            int tripId)
        {
            var resultDto = await _userService.GetAllByTripIdAndCategorizeByRoleAsync(tripId);

            var resultViewModel = _mapper.Map<UsersByTripAndCategorizedByRoleViewModel>(resultDto);

            return Ok(resultViewModel);
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
    }
}

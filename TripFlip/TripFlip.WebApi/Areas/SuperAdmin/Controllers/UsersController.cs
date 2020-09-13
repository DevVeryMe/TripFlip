using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces;
using TripFlip.ViewModels.UserViewModels;
using TripFlip.WebApi.StringConstants;

namespace TripFlip.WebApi.Areas.SuperAdmin.Controllers
{
    [Area(AreaName.SuperAdmin)]
    [Route("api/super-admin/users")]
    [Authorize(Roles = ApplicationRoleName.SuperAdmin)]
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
        /// Grants appication role to user.
        /// </summary>
        /// <param name="grantApplicationRoleViewModel">Data with
        /// user id and application role.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /super-admin/users/grant-application-role
        ///     {
        ///         "userId": "17E9456A-A362-46CA-9CD4-3DC1CD2061FF",
        ///         "applicationRole": 2
        ///     }
        /// </remarks>
        [HttpPut("grant-application-role")]
        public async Task<IActionResult> GrantRoleAsync(
            [FromBody] GrantApplicationRoleViewModel grantApplicationRoleViewModel)
        {
            var grantApplicationRoleDto =
                _mapper.Map<GrantApplicationRoleDto>(grantApplicationRoleViewModel);

            await _userService.GrantApplicationRoleAsync(grantApplicationRoleDto);

            return Ok();
        }
    }
}

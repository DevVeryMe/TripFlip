using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Authorize]
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
        /// Updates profile of authorized user.
        /// </summary>
        /// <param name="updateUserViewModel">New User data.</param>
        /// <returns>User view model that
        /// represents the updated database entry.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /users
        ///     {
        ///         "email": "sample@gmail.com",
        ///         "firstName": "Marco",
        ///         "lastName": "Polo",
        ///         "aboutMe": "A great adventurer from Venice.",
        ///         "gender": 1,
        ///         "birthDate": "1524-09-12T19:45:44.631Z"
        ///     }
        /// </remarks>
        [HttpPut]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UpdateUserProfileViewModel updateUserViewModel)
        {
            var updateUserDto = _mapper.Map<UpdateUserProfileDto>(updateUserViewModel);

            var userDto = await _userService.UpdateUserProfileAsync(updateUserDto);

            var userViewModel = _mapper.Map<UserViewModel>(userDto);

            return Ok(userViewModel);
        }

        /// <summary>
        /// Changes password of authorized user.
        /// </summary>
        /// <param name="changeUserPasswordViewModel">View model that contains both 
        /// old and new user passwords.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /users/change-password
        ///     {
        ///         "oldPassword": "old-Password1",
        ///         "newPassword": "new-Password2",
        ///         "newPasswordConfirmation": "new-Password2"
        ///     }
        /// </remarks>
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePasswordAsync(
            [FromBody] ChangeUserPasswordViewModel changeUserPasswordViewModel)
        {
            var changeUserPasswordDto = _mapper.Map<ChangeUserPasswordDto>(
                changeUserPasswordViewModel);

            await _userService.ChangePasswordAsync(changeUserPasswordDto);

            return Ok();
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
        ///     Post /users/authorize
        ///     {
        ///         "email": "sample@gmail.com",
        ///         "password": "TestPassword@1",
        ///     }
        /// </remarks>
        [HttpPost("authorize")]
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

        /// <summary>
        /// Registers User.
        /// </summary>
        /// <param name="registerUserViewModel">Data to register User with.</param>
        /// <returns>User view model that
        /// represents the new entry that was added to database.</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /users
        ///     {
        ///         "email": "example@mail.com",
        ///         "password": "rel1able-Password",
        ///         "passwordConfirmation": "rel1able-Password"
        ///         "firstName": "Marco",
        ///         "lastName": "Polo",
        ///         "aboutMe": "A great adventurer from Venice.",
        ///         "gender": 1,
        ///         "birthDate": "1524-09-12T19:45:44.631Z"
        ///     }
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] RegisterUserViewModel registerUserViewModel)
        {
            var registerUserDto = _mapper.Map<RegisterUserDto>(registerUserViewModel);
            var registeredUserDto = await _userService.RegisterAsync(registerUserDto);
            var registeredUserViewModel = _mapper.Map<UserViewModel>(registeredUserDto);

            return Ok(registeredUserViewModel);
        }

        /// <summary>
        /// Deletes User.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>No content (HTTP code 204).</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(Guid id)
        {
            await _userService.DeleteByIdAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Grants role to trip subscriber.
        /// </summary>
        /// <param name="grantSubscriberRoleViewModel">Data with trip id,
        /// user id and role ids.</param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /users/grant-role
        ///     {
        ///         "tripId": 1,
        ///         "userId": "8BCC1E77-D183-41C9-8210-038D00AB12E4",
        ///         "tripRoleIds": [1, 2, 3]
        ///     }
        /// </remarks>
        [HttpPut("grant-role")]
        public async Task<IActionResult> GrantRoleAsync(
            [FromBody] GrantSubscriberRoleViewModel grantSubscriberRoleViewModel)
        {
            var grantSubscriberRoleDto =
                _mapper.Map<GrantSubscriberRoleDto>(grantSubscriberRoleViewModel);

            await _userService.GrantTripRoleAsync(grantSubscriberRoleDto);

            return Ok();
        }

        /// <summary>
        /// Subscribes current user to the trip.
        /// </summary>
        /// <param name="tripId">Id of trip to subscribe.</param>
        [HttpPut("subscribe-trip/{tripId}")]
        public async Task<IActionResult> SubscribeToTripAsync([FromRoute] 
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int tripId)
        {
            await _userService.SubscribeToTripAsync(tripId);

            return Ok();
        }

        /// <summary>
        /// Unsubscribes current user from the trip.
        /// </summary>
        /// <param name="tripId">Id of a trip to unsubscribe from.</param>
        [HttpPut("unsubscribe-trip/{tripId}")]
        public async Task<IActionResult> UnsubscribeFromTripAsync([FromRoute]
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int tripId)
        {
            await _userService.UnsubscribeFromTripAsync(tripId);

            return Ok();
        }

        /// <summary>
        /// Unsubscribes current user from the route.
        /// </summary>
        /// <param name="routeId">Id of a route to unsubscribe from.</param>
        [HttpPut("unsubscribe-route/{routeId}")]
        public async Task<IActionResult> UnsubscribeFromRouteAsync([FromRoute]
            [Range(1, int.MaxValue, ErrorMessage = ErrorConstants.IdLessThanOneError)] int routeId)
        {
            await _userService.UnsubscribeFromTripAsync(routeId);

            return Ok();
        }

        /// <summary>
        /// Gets all trips, which are subscribed by current user, with included routes,
        /// including route points, task, item lists with tasks and items and roles of
        /// current user in these trips.
        /// </summary>
        [HttpPut("subscribed-trips")]
        public async Task<IActionResult> GetAllSubscribedTrips()
        {
            var tripWithRoutesDto = await _userService.GetAllSubscribedTripsAsync();

            var tripWithRoutesViewModel = _mapper.Map<List<TripWithRoutesAndUserRolesViewModel>>(tripWithRoutesDto);

            return Ok(tripWithRoutesViewModel);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TripFlip.Services.Interfaces;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/google-api-users")]
    [Authorize]
    [ApiController]
    public class GoogleApiUsersController : ControllerBase
    {
        private readonly string googleUserDeniedAccessError = "access_denied";
        private readonly string googleUserDeniedAccessErrorMessage = "User did not approve the access request.";

        private readonly IGoogleApiUserService _googleApiUserService;

        public GoogleApiUsersController(
            IGoogleApiUserService googleApiUserService)
        {
            _googleApiUserService = googleApiUserService;
        }

        /// <summary>
        /// Logs user in with the authorization code provided by Google and returns
        /// the newly generated JWT.
        /// </summary>
        /// <param name="code">A one-time authorization code provided by Google
        /// that is used to obtain Google's access token, ID token and refresh token.</param>
        /// <param name="error">Parameter that represents possible errors that 
        /// Google's OAuth server can return.</param>
        /// <returns>A newly generated JWT.</returns>
        [AllowAnonymous]
        [HttpGet("signin-google")]
        public async Task<IActionResult> SignInWithGoogleAsync(string code, string error)
        {
            if (!string.IsNullOrWhiteSpace(error))
            {
                if (error == googleUserDeniedAccessError)
                {
                    return BadRequest(googleUserDeniedAccessErrorMessage);
                }

                return BadRequest();
            }

            string jsonWebToken =
                await _googleApiUserService.LoginWithAuthCodeAsync(code);

            return Ok(jsonWebToken);
        }

        /// <summary>
        /// Redirects to Google's OAuth 2.0 server to initiate
        /// the authentication and authorization process.
        /// </summary>
        [AllowAnonymous]
        [HttpGet("get-authorization-code")]
        public async Task<IActionResult> GetAuthorizationCode()
        {
            string googleAuthUriWithParams =
                await _googleApiUserService.GetAuthorizationUrlWithParamsAsync();

            return Redirect(googleAuthUriWithParams);
        }
    }
}

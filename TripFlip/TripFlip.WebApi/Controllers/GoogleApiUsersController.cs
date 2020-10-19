using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TripFlip.WebApi.Controllers
{
    [Route("api/google-api-users")]
    [Authorize]
    [ApiController]
    public class GoogleApiUsersController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("signin-google")]
        public async Task<IActionResult> SignInWithGoogleAsync()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("get-authorization-code")]
        public IActionResult GetAuthorizationCode()
        {
            return Ok();
        }
    }
}

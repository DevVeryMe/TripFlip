using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace TripFlip.WebApi.Controllers
{
    public class UserLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class TestAuthController : ControllerBase
    {
        IConfiguration _appConfiguration;

        public TestAuthController(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(
            [FromBody] UserLoginViewModel userLoginViewModel)
        {
            bool isAuthentified =
                userLoginViewModel.Email == "sample@mail.com" &&
                userLoginViewModel.Password == "123456";

            if (!isAuthentified)
            {
                return Unauthorized();
            }

            string jwt = GenerateJsonWebToken();

            var response = new
            {
                access_token = jwt
            };

            return Ok(response);
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetData()
        {
            return Ok("Great success! 👍");
        }

        string GenerateJsonWebToken()
        {
            var encodedSecretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_appConfiguration["Jwt:SecretKey"]) );
            var credentials = new SigningCredentials(
                encodedSecretKey,
                SecurityAlgorithms.HmacSha256);

            int expirationTime = int.Parse(_appConfiguration["Jwt:TokenLifetime"]);

            // creating JWT
            var jwt = new JwtSecurityToken(
                issuer: _appConfiguration["Jwt:Issuer"],
                audience: _appConfiguration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: credentials
                );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}

using AutoMapper;
using GoogleAuthentication.DataAccess;
using GoogleAuthentication.Domain.Entities;
using GoogleAuthentication.Services.CustomExceptions;
using GoogleAuthentication.Services.Dtos;
using GoogleAuthentication.Services.Interfaces;
using GoogleAuthentication.Services.Poco;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAuthentication.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        private readonly GoogleAuthorizationConfiguration _googleConfiguration;

        private readonly GoogleAuthenticationDbContext _googleAuthenticationDbContext;

        private readonly IMapper _mapper;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initializes jwt configuration, google configuration, Db context and automapper.
        /// </summary>
        /// <param name="jwtConfiguration">JwtConfiguration instance.</param>
        /// <param name="googleConfiguration">GoogleAuthorizationConfiguration instance.</param>
        /// <param name="googleAuthenticationDbContext">GoogleAuthenticationDbContext instance.</param>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="httpContextAccessor">IHttpContextAccessor instance.</param>
        /// <param name="httpClientFactory">IHttpContextAccessor instance.</param>
        public UserService(JwtConfiguration jwtConfiguration,
            GoogleAuthorizationConfiguration googleConfiguration,
            GoogleAuthenticationDbContext googleAuthenticationDbContext,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory)
        {
            _jwtConfiguration = jwtConfiguration;
            _googleConfiguration = googleConfiguration;
            _googleAuthenticationDbContext = googleAuthenticationDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AuthenticatedUserDto> SignInWithGoogle()
        {
            var idToken = await GetUserGoogleIdToken();

            var userEmail = GetEmailFromToken(idToken);

            var userEntity = await _googleAuthenticationDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == userEmail);

            if (userEntity is null)
            {
                userEntity = new UserEntity()
                {
                    Email = userEmail
                };

                await _googleAuthenticationDbContext.AddAsync(userEntity);
                await _googleAuthenticationDbContext.SaveChangesAsync();
            }

            var authenticatedUserDto = _mapper.Map<AuthenticatedUserDto>(userEntity);
            authenticatedUserDto.Token = GenerateJsonWebToken(userEntity);

            return authenticatedUserDto;
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var userEntity = await _googleAuthenticationDbContext
                .Users
                .AsNoTracking()
                .SingleOrDefaultAsync(user => user.Id == id);

            if (userEntity is null)
            {
                throw new NotFoundException(ErrorConstants.UserNotFound);
            }

            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        private async Task<string> GetUserGoogleIdToken()
        {
            var query = _httpContextAccessor.HttpContext.Request.Query;
            var code = query["code"];
            var postUrl = "https://oauth2.googleapis.com/token?" +
                          $"client_id={_googleConfiguration.ClientId}" +
                          $"&client_secret={_googleConfiguration.ClientSecret}" +
                          $"&code={code}" +
                          "&grant_type=authorization_code" +
                          $"&redirect_uri={_googleConfiguration.RedirectUri}";

            var uri = new Uri(postUrl);

            var httpClient = _httpClientFactory.CreateClient();
            var responseMessage = await httpClient.PostAsync(uri, null);

            var stringToParse = await responseMessage.Content.ReadAsStringAsync();

            var dictionaryWithTokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(stringToParse);

            var idToken = dictionaryWithTokens["id_token"];

            if (string.IsNullOrEmpty(idToken))
            {
                throw new ArgumentException(ErrorConstants.FailedToFetchIdToken);
            }

            return idToken;
        }

        private static string GetEmailFromToken(string idToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var decodedToken = jwtHandler.ReadJwtToken(idToken);

            var claims = decodedToken.Claims.ToList();
            var userEmail = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentException(ErrorConstants.FailedToFetchEmailFromIdToken);
            }

            return userEmail;
        }

        private string GenerateJsonWebToken(UserEntity userEntity)
        {
            var encodedSecretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_jwtConfiguration.SecretKey));
            var credentials = new SigningCredentials(
                encodedSecretKey,
                SecurityAlgorithms.HmacSha256
            );

            var expirationTime = _jwtConfiguration.TokenLifetime;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userEntity.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userEntity.Email)
            };

            // creating JWT
            var jwt = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: credentials
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}

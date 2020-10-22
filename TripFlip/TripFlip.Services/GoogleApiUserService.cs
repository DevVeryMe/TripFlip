using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Configurations;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Models;

namespace TripFlip.Services
{
    /// <inheritdoc cref="IGoogleApiUserService"/>
    public class GoogleApiUserService : IGoogleApiUserService
    {
        private readonly TripFlipDbContext _tripFlipDbContext;

        private readonly IHttpClientFactory _httpClientFactory;

        private readonly GoogleApiConfiguration _googleApiConfiguration;

        private readonly JwtConfiguration _jwtConfiguration;

        private readonly IWebHostEnvironment _environment;

        private readonly IMailService _mailService;

        private readonly MailServiceConfiguration _mailServiceConfiguration;

        private GoogleOpenIdConfiguration _googleOpenIdConfiguration;

        /// <summary>
        /// Initializes db context, HttpClient factory and Google API configuration fields.
        /// </summary>
        /// <param name="tripFlipDbContext">Instance of db context.</param>
        /// <param name="httpClientFactory">Instance of HttpClient factory.</param>
        /// <param name="googleApiConfiguration">Object that encapsulates configurations for
        /// Google API.</param>
        /// <param name="jwtConfiguration">Object that encapsulates configurations for JWT.</param>
        /// <param name="environment">Instance of web host environment.</param>
        /// <param name="mailService">Instance of mail service.</param>
        /// <param name="mailServiceConfiguration">Object that encapsulates configurations for
        /// mail service.</param>
        public GoogleApiUserService(
            TripFlipDbContext tripFlipDbContext,
            IHttpClientFactory httpClientFactory,
            GoogleApiConfiguration googleApiConfiguration,
            JwtConfiguration jwtConfiguration,
            IWebHostEnvironment environment,
            IMailService mailService,
            MailServiceConfiguration mailServiceConfiguration)
        {
            _tripFlipDbContext = tripFlipDbContext;

            _httpClientFactory = httpClientFactory;

            _googleApiConfiguration = googleApiConfiguration;

            _jwtConfiguration = jwtConfiguration;

            _environment = environment;

            _mailService = mailService;

            _mailServiceConfiguration = mailServiceConfiguration;
        }

        public async Task<string> GetAuthorizationUrlWithParamsAsync()
        {
            await GetGoogleOpenIdConfigurationAsync();

            if (_googleOpenIdConfiguration.AuthorizationEndpoint is null)
            {
                throw new Exception(ErrorConstants.GoogleFailedToReturnOpenIdConfig);
            }

            string requiredScopes =
                GetAuthRequiredScopes(_googleOpenIdConfiguration.ScopesSupported);

            string requiredResposeType =
                GetAuthRequiredResponseType(_googleOpenIdConfiguration.ResponseTypesSupported);

            string authorizationEndpointUrl =
                _googleOpenIdConfiguration.AuthorizationEndpoint;

            string authorizationParams =
                $"?client_id={_googleApiConfiguration.ClientId}" +
                $"&redirect_uri={_googleApiConfiguration.RedirectUri}" +
                $"&response_type={requiredResposeType}" +
                $"&scope={requiredScopes}";

            return authorizationEndpointUrl + authorizationParams;
        }

        /// <summary>
        /// Is responsible for calling other methods that execute following actions:
        /// <list type="number">
        /// <item>Exchanging given authorization code for id token.</item>
        /// <item>Obtaining user email from id token.</item>
        /// <item>Adding new user entry into database if user with the given email 
        /// does not already exist.</item>
        /// <item>Generating new JWT token for the current user.</item>
        /// </list>
        /// </summary>
        /// <param name="authorizationCode">>A one-time authorization code provided by Google 
        /// that is used to obtain Google's access token, ID token and refresh token.</param>
        /// <returns>A newly generated JWT.</returns>
        public async Task<string> LoginWithAuthCodeAsync(string authorizationCode)
        {
            await GetGoogleOpenIdConfigurationAsync();

            var googleOauthResponse =
                await ExchangeAuthCodeForTokensAsync(authorizationCode);

            if (string.IsNullOrWhiteSpace(googleOauthResponse.AccessToken))
            {
                throw new Exception(ErrorConstants.GoogleFailedToExchangeAuthCodeForTokens);
            }

            string userEmail =
                GetEmailFromGoogleIdToken(googleOauthResponse.IdToken);

            var userEntity = await GetUserByEmailAsync(userEmail);

            if (userEntity is null)
            {
                userEntity = await RegisterAsync(userEmail);
            }

            string jwt = JsonWebTokenHelper.GenerateJsonWebToken(
                userIncludingRoles: userEntity,
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                secretKey: _jwtConfiguration.SecretKey,
                tokenLifetime: _jwtConfiguration.TokenLifetime);

            return jwt;
        }

        /// <summary>
        /// Makes HTTP POST request to Google's OAuth endpoint in order 
        /// to exchange an authorization code for an access token.
        /// </summary>
        /// <param name="authorizationCode">A one-time authorization code provided by Google 
        /// that is used to obtain Google's access token, ID token and refresh token.</param>
        /// <returns><see cref="GoogleOauthResponse"/> object that encapsulates Google access 
        /// token along with other values.</returns>
        private async Task<GoogleOauthResponse> ExchangeAuthCodeForTokensAsync(string authorizationCode)
        {
            if (string.IsNullOrWhiteSpace(authorizationCode))
            {
                throw new ArgumentException(ErrorConstants.GoogleInvalidAuthorizationCode);
            }

            var endpointParams = new Dictionary<string, string>
            {
                {"client_id",  _googleApiConfiguration.ClientId },
                {"client_secret",  _googleApiConfiguration.ClientSecret },
                {"code",  authorizationCode },
                {"grant_type",  Constants.GoogleRequiredAuthGrantType },
                {"redirect_uri",  _googleApiConfiguration.RedirectUri }
            };

            var formUrlEncodedContent = new FormUrlEncodedContent(endpointParams);

            var requestMessage = new HttpRequestMessage(
                method: HttpMethod.Post,
                requestUri: _googleOpenIdConfiguration.TokenEndpoint)
            {
                Content = formUrlEncodedContent
            };

            var httpClient = _httpClientFactory.CreateClient();

            var responseMessage = await httpClient.SendAsync(requestMessage);

            string responseContentString = await responseMessage.Content.ReadAsStringAsync();

            var googleOauthResponse =
                JsonConvert.DeserializeObject<GoogleOauthResponse>(responseContentString);

            return googleOauthResponse;
        }

        /// <summary>
        /// Reads Google's id token and returns email from it.
        /// </summary>
        /// <param name="idToken">Google's id token.</param>
        /// <returns>User's email.</returns>
        private string GetEmailFromGoogleIdToken(string idToken)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var decodedInfo = jwtHandler.ReadJwtToken(idToken);

            string email = decodedInfo
                .Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)
                ?.Value;

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(ErrorConstants.GoogleNoEmailInIdToken);
            }

            return email;
        }

        /// <summary>
        /// Makes database query and returns user with matching email address.
        /// </summary>
        /// <param name="email">Email to search user with.</param>
        /// <returns><see cref="UserEntity"/> object that represents user with
        /// the matching email address.</returns>
        private async Task<UserEntity> GetUserByEmailAsync(string email)
        {
            var user = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .Include(user => user.ApplicationRoles)
                .FirstOrDefaultAsync(user => user.Email == email);

            return user;
        }

        /// <summary>
        /// Returns a string of Google scopes that are required in order to 
        /// authenticate user.
        /// </summary>
        /// <param name="supportedScopes">An array of all Google scopes that are 
        /// currently supported for authentication.</param>
        /// <returns>Space delimeted string of required scopes.</returns>
        private string GetAuthRequiredScopes(string[] supportedScopes)
        {
            string requiredScopes = string.Empty;

            foreach (string scope in supportedScopes)
            {
                requiredScopes += scope + " ";
            }

            return requiredScopes;
        }

        /// <summary>
        /// Returns response type value that is required in order to 
        /// authenticate user.
        /// </summary>
        /// <param name="supportedResponseTypes">An array of all Google OAuth endpoint 
        /// response types that are currently supported.</param>
        /// <returns>A string value of a Google OAuth response type that is 
        /// required in order to authenticate user.</returns>
        private string GetAuthRequiredResponseType(string[] supportedResponseTypes)
        {
            string authorizationCodeResponseType = Constants.RequiredAuthorizationCodeResponseType;

            if (!supportedResponseTypes.Contains(authorizationCodeResponseType))
            {
                throw new Exception(ErrorConstants.GoogleInvalidAuthResponseType);
            }

            return authorizationCodeResponseType;
        }

        /// <summary>
        /// Makes HTTP request to Google's openid-configuration endpoint in order to obtain 
        /// Google's OpenID configuration values, such as: authorization enpoint, token endpoint, 
        /// supported scopes, etc.
        /// </summary>
        private async Task GetGoogleOpenIdConfigurationAsync()
        {
            if (!(_googleOpenIdConfiguration is null))
            {
                return;
            }

            var httpClient = _httpClientFactory.CreateClient();

            var responseMessage = await httpClient.GetAsync(
                _googleApiConfiguration.GoogleOpenIdConfigurationUri);

            string responseContentString = await responseMessage.Content.ReadAsStringAsync();

            _googleOpenIdConfiguration =
                JsonConvert.DeserializeObject<GoogleOpenIdConfiguration>(responseContentString);
        }

        /// <summary>
        /// Registers new user, randomizing his password.
        /// </summary>
        /// <param name="email">Email to register user with.</param>
        /// <returns>Registered user entity.</returns>
        private async Task<UserEntity> RegisterAsync(string email)
        {
            var userToRegister = CreateUserEntityWithRandomPassword(email);

            await _tripFlipDbContext.Users.AddAsync(userToRegister);
            await _tripFlipDbContext.SaveChangesAsync();

            await EmailUserNotifierHelper.NotifyRegisteredUser(
                email, _environment, _mailService, _mailServiceConfiguration);

            return userToRegister;
        }

        /// <summary>
        /// Creates a user entity with randomized password.
        /// </summary>
        /// <param name="email">Email to create user with.</param>
        /// <returns>Created user entity.</returns>
        private UserEntity CreateUserEntityWithRandomPassword(string email)
        {
            var password = PasswordGeneratorHelper.GeneratePassword(useLowercase: true,
                useUppercase: true, useNumbers: true, useSpecial: true, passwordSize: 50);

            var passwordHash = PasswordHasherHelper.HashPassword(password);

            return new UserEntity()
            {
                Email = email,
                PasswordHash = passwordHash,
                ApplicationRoles = new List<ApplicationUserRoleEntity>()
            };
        }
    }
}

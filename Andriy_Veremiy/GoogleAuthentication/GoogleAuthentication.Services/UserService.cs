using Google.Apis.Auth.OAuth2;
using GoogleAuthentication.Services.Configurations;
using GoogleAuthentication.Services.Dtos;
using GoogleAuthentication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GoogleAuthentication.DataAccess;
using GoogleAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GoogleAuthentication.Services
{
    public class UserService : IUserService
    {
        private readonly JwtConfiguration _jwtConfiguration;

        private readonly GoogleAuthenticationDbContext _googleAuthenticationDbContext;

        private readonly IMapper _mapper;

        public UserService(JwtConfiguration jwtConfiguration,
            GoogleAuthenticationDbContext googleAuthenticationDbContext,
            IMapper mapper)
        {
            _jwtConfiguration = jwtConfiguration;
            _googleAuthenticationDbContext = googleAuthenticationDbContext;
            _mapper = mapper;
        }

        public async Task<AuthenticatedUserDto> SingInWithGoogle()
        {
            var userCredential = await AuthorizeWithGoogle();

            var jwtHandler = new JwtSecurityTokenHandler();
            var decodedToken = jwtHandler.ReadJwtToken(userCredential?.Token?.IdToken);

            var claims = decodedToken.Claims.ToList();
            var userEmail = claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new ArgumentException(ErrorConstants.GoogleSignInFailed);
            }

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

        private static async Task<UserCredential> AuthorizeWithGoogle()
        {
            await using var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read);

            var userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[]
                {
                    "https://www.googleapis.com/auth/userinfo.email",
                    "https://www.googleapis.com/auth/userinfo.profile",
                    "openid"
                },
                "user",
                CancellationToken.None);

            return userCredential;
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

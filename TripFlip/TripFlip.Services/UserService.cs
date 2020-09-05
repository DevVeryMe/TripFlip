using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Services.Dto;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Helpers;
using TripFlip.Services.Interfaces;
using TripFlip.Services.Interfaces.Helpers;

namespace TripFlip.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        private readonly TripFlipDbContext _tripFlipDbContext;

        private readonly JsonWebTokenConfig _jsonWebTokenConfig;

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        /// <param name="jsonWebTokenConfig">JsonWebTokenConfig instance.</param>
        public UserService(IMapper mapper,
            TripFlipDbContext tripFlipDbContext,
            JsonWebTokenConfig jsonWebTokenConfig)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
            _jsonWebTokenConfig = jsonWebTokenConfig;
        }

        public Task<PagedList<UserDto>> GetAllAsync(string searchString,
            PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthenticatedUserDto> AuthorizeAsync(LoginDto loginDto)
        {
            var userEntity = await _tripFlipDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Email == loginDto.Email);

            ValidateUserEntityNotNull(userEntity);

            bool isPasswordVerified = PasswordHasherHelper
                .VerifyPassword(loginDto.Password, userEntity.PasswordHash);

            if (!isPasswordVerified)
            {
                throw new ArgumentException(ErrorConstants.PasswordNotVerified);
            }

            AuthenticatedUserDto authenticatedUserDto = 
                _mapper.Map<AuthenticatedUserDto>(userEntity);

            authenticatedUserDto.Token = GenerateJsonWebToken(userEntity);

            return authenticatedUserDto;
        }

        public Task<UserDto> RegisterAsync(RegisterUserDto registerUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> UpdateAsync(UpdateUserDto updateUserDto)
        {
            var userEntity = await _tripFlipDbContext.Users.FindAsync(updateUserDto.Id);

            ValidateUserEntityNotNull(userEntity);

            userEntity.Email = updateUserDto.Email;

            await _tripFlipDbContext.SaveChangesAsync();
            var userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var userEntity = await _tripFlipDbContext.Users.FindAsync(id);

            ValidateUserEntityNotNull(userEntity);

            _tripFlipDbContext.Remove(userEntity);
            await _tripFlipDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Checks if the given <see cref="UserEntity"/> is not null. If null,
        /// then throws an <see cref="ArgumentException"/> with a corresponding message.
        /// </summary>
        /// <param name="userEntity">Object that should be checked.</param>
        private void ValidateUserEntityNotNull(UserEntity userEntity)
        {
            if (userEntity is null)
            {
                throw new ArgumentException(ErrorConstants.UserNotFound);
            }
        }

        /// <summary>
        /// Generates JWT.
        /// </summary>
        /// <param name="user">User entity needed to add claims.</param>
        /// <returns>Encoded JWT.</returns>
        private string GenerateJsonWebToken(UserEntity user)
        {
            var encodedSecretKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_jsonWebTokenConfig.SecretKey));
                var credentials = new SigningCredentials(
                encodedSecretKey,
                SecurityAlgorithms.HmacSha256
                );

            int expirationTime = _jsonWebTokenConfig.TokenLifetime;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            // creating JWT
            var jwt = new JwtSecurityToken(
                issuer: _jsonWebTokenConfig.Issuer,
                audience: _jsonWebTokenConfig.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationTime),
                signingCredentials: credentials
                );

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}

﻿using AutoMapper;
using System;
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

        /// <summary>
        /// Initializes database context and automapper.
        /// </summary>
        /// <param name="mapper">IMapper instance.</param>
        /// <param name="tripFlipDbContext">TripFlipDbContext instance.</param>
        public UserService(TripFlipDbContext tripFlipDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _tripFlipDbContext = tripFlipDbContext;
        }

        public Task<PagedList<UserDto>> GetAllAsync(string searchString, PaginationDto paginationDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticatedUserDto> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
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
    }
}

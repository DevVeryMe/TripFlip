﻿using System.Threading.Tasks;
using GoogleAuthentication.Services.Dtos;

namespace GoogleAuthentication.Services.Interfaces
{
    public interface IUserService
    {
        public Task<AuthenticatedUserDto> SingInWithGoogle();
    }
}
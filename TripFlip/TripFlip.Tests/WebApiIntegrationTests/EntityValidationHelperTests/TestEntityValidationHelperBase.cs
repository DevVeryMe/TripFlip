using Moq;
using System;
using System.Collections.Generic;
using TripFlip.Domain.Entities;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.EntityValidationHelperTests
{
    public abstract class TestEntityValidationHelperBase : TestServiceBase
    {
        protected static UserEntity ValidUser => new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "correct@mail.com"
        };

        protected static UserEntity NonExistentUser => new UserEntity()
        {
            Id = Guid.Parse("322967ec-9415-4778-99c6-7f566d1bb8d2"),
            Email = "nonexistent@mail.com"
        };

        protected IEnumerable<ApplicationRoleEntity> ApplicationRoleEntitiesToSeed = 
            new List<ApplicationRoleEntity>()
            {
                new ApplicationRoleEntity()
                {
                    Id = 1,
                    Name = "SuperAdmin"
                },
                new ApplicationRoleEntity()
                {
                    Id = 2,
                    Name = "Admin"
                }
            };

        protected ApplicationUserRoleEntity ApplicationUserRoleEntityToSeed =
            new ApplicationUserRoleEntity()
            {
                ApplicationRoleId = 1, 
                UserId = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e")
            };

        protected ICurrentUserService CurrentUserService;

        protected static ICurrentUserService CreateCurrentUserService(Guid id, string email)
        {
            var correctEmail = email;
            var correctGuid = id;

            var mock = new Mock<ICurrentUserService>();
            mock.Setup(a => a.UserEmail).Returns(correctEmail);
            mock.Setup(a => a.UserId).Returns(correctGuid);

            return mock.Object;
        }
    }
}

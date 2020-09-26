using System;
using Moq;
using TripFlip.Domain.Entities;
using TripFlip.Services.Configurations;
using TripFlip.Services.Dto.Enums;
using TripFlip.Services.Dto.UserDtos;
using TripFlip.Services.Interfaces;

namespace WebApiIntegrationTests.UserServiceTests
{
    public class TestUserServiceBase : TestServiceBase
    {
        protected static UserEntity ValidUser => new UserEntity()
        {
            Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
            Email = "correct@mail.com"
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

        protected static JwtConfiguration CreateJwtConfiguration()
        {
            var jwtConfiguration = new JwtConfiguration()
            {
                SecretKey = "TripFlip-WebApi-SecretKey",
                Issuer = "TripFlip-WebApi",
                Audience = "TripFlip-WebApi-User",
                TokenLifetime = 10
            };

            return jwtConfiguration;
        }

        protected RegisterUserDto GetRegisterUserDto(string email = "correct@mail.com",
            string password = "Correctpass1@", string aboutMe = null, string firstName = null,
            string lastName = null, DateTimeOffset? birthDate = null, UserGender? gender = null)
        {
            return new RegisterUserDto()
            {
                Email = email,
                Password = password,
                AboutMe = aboutMe,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                Gender = gender
            };
        }
    }
}

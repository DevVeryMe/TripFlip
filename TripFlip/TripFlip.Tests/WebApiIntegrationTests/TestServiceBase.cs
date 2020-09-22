using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Root.MappingProfiles;

namespace WebApiIntegrationTests
{
    public abstract class TestServiceBase
    {
        protected TripFlipDbContext TripFlipDbContext;

        protected IMapper Mapper;

        protected static bool IsSeeded = false;

        protected TestServiceBase()
        {
            TripFlipDbContext = CreateDbContext();
            Mapper = CreateMapper();

            if (!IsSeeded)
            {
                Seed();
            }
        }

        private IMapper CreateMapper()
        {
            var entityFromDtoProfile = new EntityFromDto();
            var entityToDtoProfile = new EntityToDto();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(entityFromDtoProfile);
                cfg.AddProfile(entityToDtoProfile);
            });

            var mapper = new Mapper(configuration);

            return mapper;
        }

        private TripFlipDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TripFlipDbContext>()
                .UseInMemoryDatabase(databaseName: "TripFlipInMemoryDatabase")
                .Options;

            var context = new TripFlipDbContext(options);

            return context;
        }

        private void Seed()
        {
            TripFlipDbContext.Users.Add(new UserEntity()
            {
                Id = Guid.Parse("0f8fad5b-d9cb-469f-a165-70867728950e"),
                Email = "string@mail.com"
            });

            TripFlipDbContext.TripRoles.AddRange(new List<TripRoleEntity>()
            {
                new TripRoleEntity()
                {
                    Id = 1,
                    Name = "Admin"
                },
                new TripRoleEntity()
                {
                    Id = 2,
                    Name = "Editor"
                },
                new TripRoleEntity()
                {
                    Id = 3,
                    Name = "Guest"
                }
            });

            TripFlipDbContext.SaveChanges();

            IsSeeded = true;
        }
    }
}

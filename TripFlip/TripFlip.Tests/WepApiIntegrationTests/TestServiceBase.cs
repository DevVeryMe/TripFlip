using System;
using System.Globalization;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TripFlip.DataAccess;
using TripFlip.Domain.Entities;
using TripFlip.Root.MappingProfiles;

namespace WepApiIntegrationTests
{
    public abstract class TestServiceBase
    {
        protected TripFlipDbContext TripFlipDbContext;

        protected IMapper Mapper;

        protected TestServiceBase()
        {
            TripFlipDbContext = CreateDbContext();
            Mapper = CreateMapper();
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
            SeedHelper.Seed(context);

            return context;
        }
    }
}

using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using TripFlip.DataAccess;
using TripFlip.Root.MappingProfiles;

namespace WebApiIntegrationTests
{
    public abstract class TestServiceBase
    {
        protected TripFlipDbContext TripFlipDbContext;

        protected IMapper Mapper;

        protected List<string> Logs = new List<string>();

        protected TestServiceBase()
        {
            Mapper = CreateMapper();
            TripFlipDbContext = CreateDbContext();
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

        protected TripFlipDbContext CreateDbContext()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TripFlipDbContext>();
            builder.UseInMemoryDatabase("In memory test database")
                .UseInternalServiceProvider(serviceProvider);

            var context = new TripFlipDbContext(builder.Options);

            return context;
        }

        protected void Seed<TEntity>(TEntity entity)
        where TEntity: class
        {
            TripFlipDbContext.Add(entity);
            TripFlipDbContext.SaveChanges();
        }

        protected void Seed<TEntity>(IEnumerable<TEntity> entities)
        where TEntity: class
        {
            TripFlipDbContext.AddRange(entities);
            TripFlipDbContext.SaveChanges();
        }
    }
}

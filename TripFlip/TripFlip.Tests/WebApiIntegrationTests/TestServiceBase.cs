using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using TripFlip.DataAccess;
using TripFlip.Root.MappingProfiles;

namespace WebApiIntegrationTests
{
    public abstract class TestServiceBase
    {
        protected IMapper Mapper;

        protected TripFlipDbContext TripFlipDbContext;

        protected TestServiceBase()
        {
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

        protected TripFlipDbContext CreateDbContext()
        {
            var dbName = Guid.NewGuid().ToString();
            
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TripFlipDbContext>();
            builder.UseInMemoryDatabase(dbName)
                .UseInternalServiceProvider(serviceProvider);

            var context = new TripFlipDbContext(builder.Options);

            return context;
        }

        protected void Seed<TEntity>(TripFlipDbContext context, params TEntity[] entities)
            where TEntity : class
        {
            foreach (var entity in entities)
            {
                context.Add(entity);
                context.SaveChanges();
                context.Entry(entity).State = EntityState.Detached;
            }
        }
    }
}

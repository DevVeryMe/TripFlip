using GoogleAuthentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace GoogleAuthentication.DataAccess.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity()
                {
                    Id = new Guid("e7cd790b-7dfb-4fb2-bba8-d01a65b39621"),
                    Email = "andrewveremiyy@gmail.com",
                    PasswordHash = "some_hash"
                });
        }
    }
}

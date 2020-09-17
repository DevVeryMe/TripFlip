﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripFlip.DataAccess;

namespace TripFlip.DataAccess.Migrations
{
    [DbContext(typeof(TripFlipDbContext))]
    [Migration("20200916142916_RemoveCascadeDeleteBetweenRoutesAndTrips")]
    partial class RemoveCascadeDeleteBetweenRoutesAndTrips
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TripFlip.Domain.Entities.ApplicationRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ApplicationRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "SuperAdmin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.ApplicationUserRoleEntity", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ApplicationRoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ApplicationRoleId");

                    b.HasIndex("ApplicationRoleId");

                    b.ToTable("ApplicationUsersRoles");
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.ItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ItemListId")
                        .HasColumnType("int");

                    b.Property<string>("Quantity")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ItemListId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsCompleted = false,
                            ItemListId = 1,
                            Title = "Id card"
                        },
                        new
                        {
                            Id = 2,
                            IsCompleted = false,
                            ItemListId = 1,
                            Quantity = "1000$",
                            Title = "Money"
                        },
                        new
                        {
                            Id = 3,
                            IsCompleted = false,
                            ItemListId = 1,
                            Title = "Train tickets"
                        },
                        new
                        {
                            Id = 4,
                            IsCompleted = false,
                            ItemListId = 2,
                            Title = "Playing cards"
                        },
                        new
                        {
                            Id = 5,
                            IsCompleted = false,
                            ItemListId = 2,
                            Title = "Food"
                        },
                        new
                        {
                            Id = 6,
                            IsCompleted = false,
                            ItemListId = 2,
                            Title = "Guitar"
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.ItemListEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("ItemLists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(9738), new TimeSpan(0, 3, 0, 0, 0)),
                            RouteId = 1,
                            Title = "Most needed items"
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(367), new TimeSpan(0, 3, 0, 0, 0)),
                            RouteId = 1,
                            Title = "Not needed, but you can take"
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.RouteEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("TripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("Routes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(2616), new TimeSpan(0, 3, 0, 0, 0)),
                            Title = "First route",
                            TripId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(3346), new TimeSpan(0, 3, 0, 0, 0)),
                            Title = "Second route",
                            TripId = 1
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.RoutePointEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RoutePoints");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(6777), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 56.642000000000003,
                            Longitude = 14.333,
                            Order = 1,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7442), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 60.341000000000001,
                            Longitude = 17.332000000000001,
                            Order = 2,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7472), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 62.622,
                            Longitude = 18.199000000000002,
                            Order = 3,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7480), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 70.849000000000004,
                            Longitude = 22.143999999999998,
                            Order = 4,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7486), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 97.787000000000006,
                            Longitude = 31.122,
                            Order = 5,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7494), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 118.782,
                            Longitude = 49.523000000000003,
                            Order = 1,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 7,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7500), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 145.899,
                            Longitude = 54.320999999999998,
                            Order = 2,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 8,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7507), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 160.99799999999999,
                            Longitude = 69.212999999999994,
                            Order = 3,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 9,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7514), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 180.11099999999999,
                            Longitude = 71.293999999999997,
                            Order = 4,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 10,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 998, DateTimeKind.Unspecified).AddTicks(7521), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 185.23500000000001,
                            Longitude = 73.224999999999994,
                            Order = 5,
                            RouteId = 2
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TaskEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsCompleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("PriorityLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<int>("TaskListId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskListId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(7704), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy food.",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8803), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy train tickets",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8841), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy tent",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8848), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy drugs",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(8854), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy chips",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TaskListEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<int>("RouteId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("TaskLists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 999, DateTimeKind.Unspecified).AddTicks(5447), new TimeSpan(0, 3, 0, 0, 0)),
                            RouteId = 1,
                            Title = "Tasks"
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTimeOffset?>("EndsAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("StartsAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Trips");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 15, 994, DateTimeKind.Unspecified).AddTicks(6074), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "We wanna visit several different cities of Ukraine",
                            EndsAt = new DateTimeOffset(new DateTime(2020, 8, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            StartsAt = new DateTimeOffset(new DateTime(2020, 8, 17, 14, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)),
                            Title = "Our first trip"
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripFileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("TripFiles");
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripRoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TripRoles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Editor"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Guest"
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripSubscriberEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("DateSubscribed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.HasIndex("UserId");

                    b.ToTable("TripSubscribers");
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripSubscriberRoleEntity", b =>
                {
                    b.Property<int>("TripSubscriberId")
                        .HasColumnType("int");

                    b.Property<int>("TripRoleId")
                        .HasColumnType("int");

                    b.HasKey("TripSubscriberId", "TripRoleId");

                    b.HasIndex("TripRoleId");

                    b.ToTable("TripSubscribersRoles");
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AboutMe")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<DateTimeOffset?>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateCreated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetimeoffset")
                        .HasDefaultValueSql("SYSDATETIMEOFFSET()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(320)")
                        .HasMaxLength(320);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("59771c83-7cea-4f17-bcc6-6345b2b765e2"),
                            AboutMe = "About me",
                            BirthDate = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(4214), new TimeSpan(0, 3, 0, 0, 0)),
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(1108), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "sample1.email@mail.com",
                            FirstName = "Andrew",
                            Gender = 1,
                            LastName = "Kravchuk",
                            PasswordHash = "some_hash"
                        },
                        new
                        {
                            Id = new Guid("ec3b5fc8-620a-4a94-941d-9a12c9e834b3"),
                            AboutMe = "About me",
                            BirthDate = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5233), new TimeSpan(0, 3, 0, 0, 0)),
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5168), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "sample2.email@mail.com",
                            FirstName = "Andrew",
                            Gender = 1,
                            LastName = "Veremiy",
                            PasswordHash = "some_other_hash"
                        },
                        new
                        {
                            Id = new Guid("a0012388-e6fd-4bf6-b036-7a9a088df572"),
                            AboutMe = "About me",
                            BirthDate = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5273), new TimeSpan(0, 3, 0, 0, 0)),
                            DateCreated = new DateTimeOffset(new DateTime(2020, 9, 16, 17, 29, 16, 0, DateTimeKind.Unspecified).AddTicks(5266), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "sample3.email@mail.com",
                            FirstName = "Stas",
                            Gender = 1,
                            LastName = "Lazarev",
                            PasswordHash = "hash_hash"
                        });
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.ApplicationUserRoleEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.ApplicationRoleEntity", "ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripFlip.Domain.Entities.UserEntity", "User")
                        .WithMany("ApplicationRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.ItemEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.ItemListEntity", "ItemList")
                        .WithMany("Items")
                        .HasForeignKey("ItemListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.ItemListEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.RouteEntity", "Route")
                        .WithMany("ItemLists")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.RouteEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.TripEntity", "Trip")
                        .WithMany("Routes")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.RoutePointEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.RouteEntity", "Route")
                        .WithMany("RoutePoints")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TaskEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.TaskListEntity", "TaskList")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TaskListEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.RouteEntity", "Route")
                        .WithMany("TaskLists")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripFileEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.TripEntity", "Trip")
                        .WithMany("TripFiles")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripSubscriberEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.TripEntity", "Trip")
                        .WithMany("TripSubscribers")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripFlip.Domain.Entities.UserEntity", "User")
                        .WithMany("TripSubscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TripFlip.Domain.Entities.TripSubscriberRoleEntity", b =>
                {
                    b.HasOne("TripFlip.Domain.Entities.TripRoleEntity", "TripRole")
                        .WithMany("TripSubscribers")
                        .HasForeignKey("TripRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TripFlip.Domain.Entities.TripSubscriberEntity", "TripSubscriber")
                        .WithMany("TripRoles")
                        .HasForeignKey("TripSubscriberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
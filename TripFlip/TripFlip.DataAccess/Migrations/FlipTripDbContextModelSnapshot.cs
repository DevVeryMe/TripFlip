﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace TripFlip.DataAccess.Migrations
{
    [DbContext(typeof(TripFlipDbContext))]
    partial class TripFlipDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(1346), new TimeSpan(0, 3, 0, 0, 0)),
                            RouteId = 1,
                            Title = "Most needed items"
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(1865), new TimeSpan(0, 3, 0, 0, 0)),
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

                    b.Property<int>("TripId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TripId");

                    b.ToTable("Routes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(5778), new TimeSpan(0, 3, 0, 0, 0)),
                            Title = "First route",
                            TripId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(6441), new TimeSpan(0, 3, 0, 0, 0)),
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
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(8982), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 56.642000000000003,
                            Longitude = 14.333,
                            Order = 1,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9523), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 60.341000000000001,
                            Longitude = 17.332000000000001,
                            Order = 2,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9550), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 62.622,
                            Longitude = 18.199000000000002,
                            Order = 3,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9556), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 70.849000000000004,
                            Longitude = 22.143999999999998,
                            Order = 4,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9562), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 97.787000000000006,
                            Longitude = 31.122,
                            Order = 5,
                            RouteId = 1
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9568), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 118.782,
                            Longitude = 49.523000000000003,
                            Order = 1,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 7,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9573), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 145.899,
                            Longitude = 54.320999999999998,
                            Order = 2,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 8,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9579), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 160.99799999999999,
                            Longitude = 69.212999999999994,
                            Order = 3,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 9,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9584), new TimeSpan(0, 3, 0, 0, 0)),
                            Latitude = 180.11099999999999,
                            Longitude = 71.293999999999997,
                            Order = 4,
                            RouteId = 2
                        },
                        new
                        {
                            Id = 10,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 979, DateTimeKind.Unspecified).AddTicks(9589), new TimeSpan(0, 3, 0, 0, 0)),
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
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(7858), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy food.",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(8789), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy train tickets",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(8822), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy tent",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(8829), new TimeSpan(0, 3, 0, 0, 0)),
                            Description = "Buy drugs",
                            IsCompleted = false,
                            PriorityLevel = 0,
                            TaskListId = 1
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(8834), new TimeSpan(0, 3, 0, 0, 0)),
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
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 980, DateTimeKind.Unspecified).AddTicks(6070), new TimeSpan(0, 3, 0, 0, 0)),
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
                            DateCreated = new DateTimeOffset(new DateTime(2020, 8, 23, 14, 25, 34, 976, DateTimeKind.Unspecified).AddTicks(4151), new TimeSpan(0, 3, 0, 0, 0)),
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
#pragma warning restore 612, 618
        }
    }
}

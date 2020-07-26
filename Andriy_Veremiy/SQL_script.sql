USE [master]
GO
/****** Object:  Database [JSP_SQL_TASK]    Script Date: 7/26/2020 8:23:22 AM ******/
CREATE DATABASE [JSP_SQL_TASK]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JSP_SQL_TASK', FILENAME = N'D:\SQL Server 2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\JSP_SQL_TASK.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JSP_SQL_TASK_log', FILENAME = N'D:\SQL Server 2019\MSSQL15.MSSQLSERVER\MSSQL\DATA\JSP_SQL_TASK_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [JSP_SQL_TASK] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JSP_SQL_TASK].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JSP_SQL_TASK] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET ARITHABORT OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JSP_SQL_TASK] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JSP_SQL_TASK] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JSP_SQL_TASK] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JSP_SQL_TASK] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET RECOVERY FULL 
GO
ALTER DATABASE [JSP_SQL_TASK] SET  MULTI_USER 
GO
ALTER DATABASE [JSP_SQL_TASK] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JSP_SQL_TASK] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JSP_SQL_TASK] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JSP_SQL_TASK] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JSP_SQL_TASK] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'JSP_SQL_TASK', N'ON'
GO
ALTER DATABASE [JSP_SQL_TASK] SET QUERY_STORE = OFF
GO
USE [JSP_SQL_TASK]
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 7/26/2020 8:23:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CarName] [varchar](50) NOT NULL,
	[CarModel] [varchar](50) NOT NULL,
	[CarMileage] [real] NOT NULL,
	[ManufactureDate] [date] NOT NULL,
	[Horsepower] [int] NOT NULL,
	[EngineType] [nchar](8) NOT NULL,
	[Price] [money] NOT NULL,
 CONSTRAINT [PK_Cars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cars] ON 

INSERT [dbo].[Cars] ([ID], [CarName], [CarModel], [CarMileage], [ManufactureDate], [Horsepower], [EngineType], [Price]) VALUES (1, N'Ford', N'Focus', 30000, CAST(N'2019-08-08' AS Date), 135, N'Gasoline', 23000.0000)
INSERT [dbo].[Cars] ([ID], [CarName], [CarModel], [CarMileage], [ManufactureDate], [Horsepower], [EngineType], [Price]) VALUES (2, N'Honda', N'Civic type R', 5000, CAST(N'2020-05-08' AS Date), 280, N'Gasoline', 22000.0000)
INSERT [dbo].[Cars] ([ID], [CarName], [CarModel], [CarMileage], [ManufactureDate], [Horsepower], [EngineType], [Price]) VALUES (4, N'Honda', N'Accord Coupe', 12000, CAST(N'2009-07-07' AS Date), 285, N'Gasoline', 14000.0000)
INSERT [dbo].[Cars] ([ID], [CarName], [CarModel], [CarMileage], [ManufactureDate], [Horsepower], [EngineType], [Price]) VALUES (6, N'Tesla', N'Model X', 100, CAST(N'2020-07-03' AS Date), 500, N'Electric', 100000.0000)
INSERT [dbo].[Cars] ([ID], [CarName], [CarModel], [CarMileage], [ManufactureDate], [Horsepower], [EngineType], [Price]) VALUES (9, N'Subaru', N'Wrx Sti', 1500.7, CAST(N'2020-07-26' AS Date), 400, N'Gasoline', 15000.0000)
SET IDENTITY_INSERT [dbo].[Cars] OFF
/****** Object:  StoredProcedure [dbo].[sp_CreateCar]    Script Date: 7/26/2020 8:23:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_CreateCar]
    @CarName nvarchar(50),
    @CarModel nvarchar(50),
	@CarMileage real,
	@ManufactureDate date,
	@Horsepower int,
	@EngineType nchar(8),
	@Price money
AS
    INSERT INTO [Cars](CarName, CarModel, CarMileage, ManufactureDate, Horsepower, EngineType, Price)
    VALUES (@CarName, @CarModel, @CarMileage, @ManufactureDate, @Horsepower, @EngineType, @Price)
  
    SELECT SCOPE_IDENTITY()
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteCar]    Script Date: 7/26/2020 8:23:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_DeleteCar]
    @ID int
AS
    DELETE FROM [Cars] WHERE [ID] = @ID
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCar]    Script Date: 7/26/2020 8:23:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCar]
    @ID int
AS
    SELECT * FROM [Cars] WHERE [ID] = @ID
GO
/****** Object:  StoredProcedure [dbo].[sp_GetCars]    Script Date: 7/26/2020 8:23:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetCars]

AS
    SELECT * FROM Cars
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateCar]    Script Date: 7/26/2020 8:23:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_UpdateCar]
	@ID int,
    @CarName nvarchar(50),
    @CarModel nvarchar(50),
	@CarMileage real,
	@ManufactureDate date,
	@Horsepower int,
	@EngineType nchar(8),
	@Price money
AS
    UPDATE [Cars] 
	SET [CarName] = @CarName, [CarModel] = @CarModel, [CarMileage] = @CarMileage, [ManufactureDate] = @ManufactureDate, 
	[Horsepower] = @Horsepower, [EngineType] = @EngineType, [Price] = @Price
	WHERE [ID] = @ID
GO
USE [master]
GO
ALTER DATABASE [JSP_SQL_TASK] SET  READ_WRITE 
GO

USE [master]
GO
/****** Object:  Database [task]    Script Date: 25.07.2020 18:48:21 ******/
CREATE DATABASE [task]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'task', FILENAME = N'C:\Users\Andrew\task.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'task_log', FILENAME = N'C:\Users\Andrew\task_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [task] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [task].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [task] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [task] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [task] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [task] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [task] SET ARITHABORT OFF 
GO
ALTER DATABASE [task] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [task] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [task] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [task] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [task] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [task] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [task] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [task] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [task] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [task] SET  DISABLE_BROKER 
GO
ALTER DATABASE [task] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [task] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [task] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [task] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [task] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [task] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [task] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [task] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [task] SET  MULTI_USER 
GO
ALTER DATABASE [task] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [task] SET DB_CHAINING OFF 
GO
ALTER DATABASE [task] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [task] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [task] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [task] SET QUERY_STORE = OFF
GO
USE [task]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [task]
GO
/****** Object:  Table [dbo].[Books]    Script Date: 25.07.2020 18:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[Count] [int] NOT NULL,
	[ReleaseDate] [date] NOT NULL,
	[Publisher] [nvarchar](50) NOT NULL,
	[Language] [nchar](2) NOT NULL,
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [Description], [Author], [Price], [Count], [ReleaseDate], [Publisher], [Language]) VALUES (3, N'Too Much and Never Enough : How My Family Created the World''s Most Dangerous Man', N'In this revelatory, authoritative portrait of Donald J. Trump and the toxic family that made him, Mary L. Trump, a trained clinical psychologist and Donald''s only niece, shines a bright light on the dark history of their family in order to explain how her uncle became the man who now threatens the world''s health, economic security and social fabric.

Mary Trump spent much of her childhood in her grandparents'' large, imposing house in New York, where Donald and his four siblings grew up. She describes a nightmare of traumas, destructive relationships and a tragic combination of neglect and abuse. She explains how specific events and general family patterns created the damaged man who currently occupies the Oval Office, including the strange and harmful relationship between Fred Trump and his two oldest sons, Fred Jr. and Donald.

A first-hand witness, Mary brings an incisive wit and unexpected humour to sometimes grim, often confounding family events. She recounts in unsparing detail everything from her uncle Donald''s place in the family spotlight and Ivana''s penchant for regifting to her grandmother''s frequent injuries and illnesses and the appalling way Donald, Fred Trump''s favourite son, dismissed and derided him when he began to succumb to Alzheimer''s.

Numerous pundits, armchair psychologists and journalists have sought to explain Donald Trump''s lethal flaws. Mary Trump has the education, insight and intimate familiarity needed to reveal what makes Donald, and the rest of her clan, tick. She alone can recount this fascinating, unnerving saga, not just because of her insider''s perspective but also because she is the only Trump willing to tell the truth about one of the world''s most powerful and dysfunctional families.', N'Mary L. Trump', 15.4800, 10831, CAST(N'2020-07-14' AS Date), N'Simon & Schuster Ltd', N'EN')
INSERT [dbo].[Books] ([Id], [Title], [Description], [Author], [Price], [Count], [ReleaseDate], [Publisher], [Language]) VALUES (6, N'The Art of Her Deal : The Untold Story of Melania Trump', N'This revelatory biography of Melania Trump from Pulitzer Prize-winning Washington Post reporter Mary Jordan depicts a first lady who is far more influential in the White House than most people realize.

Based on interviews with more than one hundred people in five countries, The Art of Her Deal: The Untold Story of Melania Trump draws an unprecedented portrait of the first lady. While her public image is of an aloof woman floating above the political gamesmanship of Washington, behind the scenes Melania Trump is not only part of President Trump''s inner circle, but for some key decisions she has been his single most influential adviser.

Throughout her public life, Melania Trump has purposefully worked to remain mysterious. With the help of key people speaking publicly for the first time and never-before-seen documents and tapes, The Art of Her Deal looks beyond the surface image to find a determined immigrant and the life she had before she met Donald Trump. Mary Jordan traces Melania''s journey from Slovenia, where her family stood out for their nonconformity, to her days as a fledgling model known for steering clear of the industry''s hard-partying scene, to a tiny living space in Manhattan she shared platonically with a male photographer, to the long, complicated dating dance that finally resulted in her marriage to Trump. Jordan documents Melania''s key role in Trump''s political life before and at the White House, and shows why he trusts her instincts above all.

The picture of Melania Trump that emerges in The Art of Her Deal is one of a woman who is savvy, steely, ambitious, deliberate, and who plays the long game. And while it is her husband who became famous for the phrase "the art of the deal," it is she who has consistently used her leverage to get exactly what she wants. This is the story of the art of her deal.', N'Mary Jordan', 15.9700, 8798, CAST(N'2020-07-01' AS Date), N'SIMON & SCHUSTER', N'EN')
INSERT [dbo].[Books] ([Id], [Title], [Description], [Author], [Price], [Count], [ReleaseDate], [Publisher], [Language]) VALUES (7, N'Saga Volume 1', N'Winner of the 2013 Hugo award for Best Graphic Story!
When two soldiers from opposite sides of a never-ending galactic war fall in
love, they risk everything to bring a fragile new life into a dangerous old
universe. From New York Times bestselling writer Brian K. Vaughan (Y:
The Last Man, Ex Machina) and critically acclaimed artist Fiona
Staples (Mystery Society, North 40), Saga is the sweeping
tale of one young family fighting to find their place in the worlds. Fantasy and
science fiction are wed like never before in this sexy, subversive drama for
adults.
This specially priced volume collects the first six issues of the smash-hit
series The Onion A.V. Club calls "the emotional epic Hollywood wishes it could
make."
Voted one of the top graphic novels of the year by the NYT, IGN,
the Examiner, and SF Weekly. Voted Best Comic of the year by MTV Geek
and Best New Series by Paradox Comics. Voted a finalist in the GoodReads Best GN
of 2012 contest.
Named one of Time Magazine''s top 10 graphic novels for
2013', N'Brian K. Vaughan', 8.5700, 3962, CAST(N'2013-09-09' AS Date), N'Image Comics', N'EN')
SET IDENTITY_INSERT [dbo].[Books] OFF
/****** Object:  StoredProcedure [dbo].[spCreateBook]    Script Date: 25.07.2020 18:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCreateBook]
	@Title nvarchar(100),
	@Description nvarchar(MAX),
	@Author nvarchar(50),
	@Price money,
	@Count int,
	@ReleaseDate date,
	@Publisher nvarchar(50),
	@Language nchar(2)
AS
	INSERT INTO Books(Title, Description, Author, Price, Count, ReleaseDate, Publisher, Language)
	VALUES (@Title, @Description, @Author, @Price, @Count, @ReleaseDate, @Publisher, @Language);
	SELECT SCOPE_IDENTITY();
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[spDeleteBook]    Script Date: 25.07.2020 18:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spDeleteBook]
	@Id int
AS
	DELETE FROM Books WHERE Id = @Id
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[spGetBook]    Script Date: 25.07.2020 18:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetBook]
	@Id int
AS
	SELECT * FROM Books WHERE Id = @Id
RETURN 0
GO
/****** Object:  StoredProcedure [dbo].[spGetBooks]    Script Date: 25.07.2020 18:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetBooks]
AS
	SELECT * FROM Books
GO
/****** Object:  StoredProcedure [dbo].[spUpdateBook]    Script Date: 25.07.2020 18:48:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUpdateBook]
	@Id int,
	@Title nvarchar(100),
	@Description nvarchar(MAX),
	@Author nvarchar(50),
	@Price money,
	@Count int,
	@ReleaseDate date,
	@Publisher nvarchar(50),
	@Language nchar(2)
AS
	UPDATE Books SET Title = @Title, Description = @Description, Author = @Author, Price = @Price, Count = @Count, ReleaseDate = @ReleaseDate,
	Publisher = @Publisher, Language = @Language WHERE Id = @Id
RETURN 0
GO
USE [master]
GO
ALTER DATABASE [task] SET  READ_WRITE 
GO

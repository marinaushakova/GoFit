USE [master]
GO
/****** Object:  Database [gofitdb]    Script Date: 07/16/2015 14:52:12 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'gofitdb')
BEGIN
CREATE DATABASE [gofitdb] ON  PRIMARY 
( NAME = N'gofitdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\gofitdb.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'gofitdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\gofitdb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
END
GO
ALTER DATABASE [gofitdb] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [gofitdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [gofitdb] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [gofitdb] SET ANSI_NULLS OFF
GO
ALTER DATABASE [gofitdb] SET ANSI_PADDING OFF
GO
ALTER DATABASE [gofitdb] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [gofitdb] SET ARITHABORT OFF
GO
ALTER DATABASE [gofitdb] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [gofitdb] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [gofitdb] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [gofitdb] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [gofitdb] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [gofitdb] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [gofitdb] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [gofitdb] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [gofitdb] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [gofitdb] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [gofitdb] SET  DISABLE_BROKER
GO
ALTER DATABASE [gofitdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [gofitdb] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [gofitdb] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [gofitdb] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [gofitdb] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [gofitdb] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [gofitdb] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [gofitdb] SET  READ_WRITE
GO
ALTER DATABASE [gofitdb] SET RECOVERY FULL
GO
ALTER DATABASE [gofitdb] SET  MULTI_USER
GO
ALTER DATABASE [gofitdb] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [gofitdb] SET DB_CHAINING OFF
GO
USE [gofitdb]
GO
/****** Object:  ForeignKey [exercise$fk_Exercise_Type1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_Type1]
GO
/****** Object:  ForeignKey [exercise$fk_Exercise_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_User1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_Category1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_Category1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_User1]
GO
/****** Object:  ForeignKey [workout_rating$fk_Workout_Rating_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_rating$fk_Workout_Rating_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_rating]'))
ALTER TABLE [dbo].[workout_rating] DROP CONSTRAINT [workout_rating$fk_Workout_Rating_Workout1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Exercise1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_User1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_User1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_Workout1]
GO
/****** Object:  Table [dbo].[comment]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_User1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_Workout1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[comment]') AND type in (N'U'))
DROP TABLE [dbo].[comment]
GO
/****** Object:  Table [dbo].[user_favorite_workout]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]') AND type in (N'U'))
DROP TABLE [dbo].[user_favorite_workout]
GO
/****** Object:  Table [dbo].[user_workout]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_User1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user_work__date___145C0A3F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [DF__user_work__date___145C0A3F]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user_work__date___15502E78]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [DF__user_work__date___15502E78]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_workout]') AND type in (N'U'))
DROP TABLE [dbo].[user_workout]
GO
/****** Object:  Table [dbo].[workout_exercise]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise]') AND type in (N'U'))
DROP TABLE [dbo].[workout_exercise]
GO
/****** Object:  Table [dbo].[workout_rating]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_rating$fk_Workout_Rating_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_rating]'))
ALTER TABLE [dbo].[workout_rating] DROP CONSTRAINT [workout_rating$fk_Workout_Rating_Workout1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout_rating]') AND type in (N'U'))
DROP TABLE [dbo].[workout_rating]
GO
/****** Object:  Table [dbo].[workout]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_Category1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_User1]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout]') AND type in (N'U'))
DROP TABLE [dbo].[workout]
GO
/****** Object:  Table [dbo].[exercise]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_Type1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_User1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__exercise__link__0BC6C43E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [DF__exercise__link__0BC6C43E]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[exercise]') AND type in (N'U'))
DROP TABLE [dbo].[exercise]
GO
/****** Object:  Table [dbo].[type]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[type]') AND type in (N'U'))
DROP TABLE [dbo].[type]
GO
/****** Object:  Table [dbo].[user]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__fname__023D5A04]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__fname__023D5A04]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__lname__03317E3D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__lname__03317E3D]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__is_male__0425A276]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__is_male__0425A276]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__is_admin__0519C6AF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__is_admin__0519C6AF]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__weight__060DEAE8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__weight__060DEAE8]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__height__07020F21]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__height__07020F21]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
DROP TABLE [dbo].[user]
GO
/****** Object:  Table [dbo].[category]    Script Date: 07/16/2015 14:52:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[category]') AND type in (N'U'))
DROP TABLE [dbo].[category]
GO
/****** Object:  Table [dbo].[category]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[category]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](45) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_category_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'category', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.category' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'category'
GO
SET IDENTITY_INSERT [dbo].[category] ON
INSERT [dbo].[category] ([id], [name], [description]) VALUES (1, N'endurance', N'Endurance workouts help keep your heart, lungs, and circulatory system healthy.')
INSERT [dbo].[category] ([id], [name], [description]) VALUES (2, N'strength', N'Strength workouts build muscle size and power')
INSERT [dbo].[category] ([id], [name], [description]) VALUES (3, N'flexibility', N'Flexibility workouts stretch your muscles and help protect you body from exercise incurred injuries')
SET IDENTITY_INSERT [dbo].[category] OFF
/****** Object:  Table [dbo].[user]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](45) NOT NULL,
	[password] [nvarchar](200) NOT NULL,
	[fname] [nvarchar](45) NULL DEFAULT (NULL),
	[lname] [nvarchar](45) NULL DEFAULT (NULL),
	[is_male] [smallint] NULL DEFAULT (NULL),
	[is_admin] [smallint] NOT NULL DEFAULT ((0)),
	[weight] [int] NULL DEFAULT (NULL),
	[height] [decimal](3, 1) NULL DEFAULT (NULL),
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_user_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [user$username_UNIQUE] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'user', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.`user`' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user'
GO
SET IDENTITY_INSERT [dbo].[user] ON
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (1, N'admin', N'3c1c88f0b0fec9b5f539c3d6b0577bd138bd157d604125a53e60e35cf940a5fe', N'Bob', N'Jones', 1, 1, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (2, N'admin2', N'cf312ac87c1b48968a08cb1b2809a5d0223668ad334ac322ca7f7ab3bce08929', N'Jane', N'Forsythe', 0, 1, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (3, N'hunts', N'5faf9c3e4b1fe468cb6fc9e7cc3bd1eb93f3cad399d45e9a358a14c2c7b483d6', N'Sharon', N'Hunt', 0, 0, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (4, N'SharonArmstrong', N'b78ca34b54a6da5d0b8d95a584276450f3c6dbdaafa0d6b4c26bdf893f234573', N'Sharon', N'Armstrong', 0, 0, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (5, N'dunnj', N'c98f2f0ce893c007f05c4c2904a5f66e72dd2ea2b8824d3927a3a763be4fb235', N'Joseph', N'Dunn', 1, 0, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (6, N'AnneBailey', N'46bab764e7e864db5484015e720e5170a6b9f32fb2ab5b1af3cbe7b32dcc9659', N'Anne', N'Bailey', 0, 0, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (7, N'FrankSmith', N'4f01f4fcf95a540b2623301119fd13a914c91a669cae2df5beb284bed4bc166e', N'Frank', N'Smith', 1, 0, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (8, N'DorothyMoreno', N'2acf57de58e9bc67475e872727864dd07c11268e652cdd5649de9d7a94c0ec71', N'Dorothy', N'Moreno', 0, 0, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (9, N'CharlesDavis', N'9ddc3eb834f671e17f1bac2f0b19ae4b1f3befec7134cd95ccc6f57c7864f575', N'Charles', N'Davis', 1, 0, NULL, NULL)
INSERT [dbo].[user] ([id], [username], [password], [fname], [lname], [is_male], [is_admin], [weight], [height]) VALUES (10, N'kevin', N'82c9b355f7b9eadabafd8c73a6c89ad39bb23cdf2c93c49307a9022ae3d0b2e9', N'Kevin', N'Carlson', 1, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
/****** Object:  Table [dbo].[type]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[type]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[type](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](45) NOT NULL,
	[measure] [nvarchar](45) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_type_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'type', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'type'
GO
SET IDENTITY_INSERT [dbo].[type] ON
INSERT [dbo].[type] ([id], [name], [measure]) VALUES (1, N'distance', N'miles')
INSERT [dbo].[type] ([id], [name], [measure]) VALUES (2, N'quantity', N'unit')
INSERT [dbo].[type] ([id], [name], [measure]) VALUES (3, N'duration', N'seconds')
INSERT [dbo].[type] ([id], [name], [measure]) VALUES (4, N'duration', N'minutes')
INSERT [dbo].[type] ([id], [name], [measure]) VALUES (5, N'duration', N'hours')
SET IDENTITY_INSERT [dbo].[type] OFF
/****** Object:  Table [dbo].[exercise]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[exercise]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[exercise](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type_id] [int] NOT NULL,
	[created_by_user_id] [int] NOT NULL,
	[created_at] [datetime2](0) NOT NULL,
	[link] [nvarchar](255) NULL DEFAULT (NULL),
	[description] [nvarchar](max) NULL,
	[timestamp] [timestamp] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_exercise_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[exercise]') AND name = N'fk_Exercise_Type1_idx')
CREATE NONCLUSTERED INDEX [fk_Exercise_Type1_idx] ON [dbo].[exercise] 
(
	[type_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[exercise]') AND name = N'fk_Exercise_User1_idx')
CREATE NONCLUSTERED INDEX [fk_Exercise_User1_idx] ON [dbo].[exercise] 
(
	[created_by_user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'exercise', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.exercise' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'exercise'
GO
SET IDENTITY_INSERT [dbo].[exercise] ON
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (1, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=bJ3Ogh5mFE4', N'Standard push-ups', N'Standard Push-ups')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (2, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=pUJnPMjYLxU', N'An easier variation of the standard push-up', N'Knee Down Push-ups')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (3, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=Ir8IrbYcM8w', N'Pull-ups', N'Pull-ups')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (4, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=_71FpEaq-fQ', N'Chin-ups', N'Chin-ups')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (5, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=COKYKgQ8KR0', N'Lunge', N'Lunge')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (6, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=7mDWDlzFobQ', N'Walking lunge', N'Walking Lunge')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (7, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=y7Iug7eC0dk', N'Jumping lunge', N'Jumping Lunge')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (8, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=UOGvtqv856A', N'Mountain climber plank', N'Mountain Climber Plank')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (9, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=NXr4Fw8q60o', N'Side plank', N'Side Plank')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (10, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=HJLE_VQ3Knc', N'Oblique V-up', N'Oblique V-up')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (11, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=jDwoBqPH0jk', N'Sit-up', N'Sit-up')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (12, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=MKmrqcoCZ-M', N'Stomach crunch', N'Stomach Crunch')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (13, 1, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=e7m205ZIxBE', N'Running', N'Running')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (14, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=9PxkxHxGRvU', N'T-Pose', N'T-Pose')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (15, 2, 1, CAST(0x0001BB002B3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=i4XOoQUtaCU', N'W-Pose', N'W-Pose')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (16, 2, 2, CAST(0x00000000133A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=xKy-l9Pf0cQ', N'O-Pose', N'O-Pose')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (17, 2, 2, CAST(0x00000000133A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=JY9SlansmJ4', N'Y-Pose', N'Y-Pose')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (18, 2, 2, CAST(0x00000000133A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=wmVMcHeqoZc', N'I-Pose', N'I-Pose')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (19, 2, 2, CAST(0x00000000133A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=Aa6zdmje-c4', N'Cobra stretch', N'Cobra stretch')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (20, 2, 2, CAST(0x00000000103A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=ZiNXOE5EsZw', N'Cat Stretch', N'Cat Stretch')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (21, 2, 2, CAST(0x00000000113A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=7rRWy7-Gokg', N'Reverse Crunch', N'Reverse Crunch')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (22, 2, 2, CAST(0x00000000113A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=FrFyUbxs1uQ', N'Side Crunch', N'Side Crunch')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (23, 2, 2, CAST(0x00000000113A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=5laCNeFnKdE', N'One-sided Crunch', N'One-sided Crunch')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (24, 2, 2, CAST(0x00000000113A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=VSp0z7Mp5IU', N'Inchworm ', N'Inchworm')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (25, 2, 2, CAST(0x00000000103A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=ihG0E_4_tCM', N'Scissor Lifts', N'Scissor Lifts')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (26, 2, 2, CAST(0x000000000E3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=rXAbcneAr3I', N'Glute Bridge March', N'Glute Bridge March')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (27, 2, 2, CAST(0x000000000E3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=HPnFTmjjDDA', N'Hollow Rock', N'Hollow Rock')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (28, 2, 2, CAST(0x000000000E3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=XMxHTNPPgxM', N'Plank', N'Plank')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (29, 2, 2, CAST(0x000000000E3A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=jGQ8_IMPQOY', N'Squat ', N'Squat ')
INSERT [dbo].[exercise] ([id], [type_id], [created_by_user_id], [created_at], [link], [description], [name]) VALUES (30, 2, 2, CAST(0x00000000103A0B0000 AS DateTime2), N'https://www.youtube.com/watch?v=U4s4mEQ5VqU', N'Squat Jump', N'Squat Jump')
SET IDENTITY_INSERT [dbo].[exercise] OFF
/****** Object:  Table [dbo].[workout]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[workout](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](45) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[category_id] [int] NOT NULL,
	[created_by_user_id] [int] NOT NULL,
	[created_at] [datetime2](0) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_workout_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[workout]') AND name = N'fk_Workout_Category1_idx')
CREATE NONCLUSTERED INDEX [fk_Workout_Category1_idx] ON [dbo].[workout] 
(
	[category_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[workout]') AND name = N'fk_Workout_User1_idx')
CREATE NONCLUSTERED INDEX [fk_Workout_User1_idx] ON [dbo].[workout] 
(
	[created_by_user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'workout', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.workout' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'workout'
GO
SET IDENTITY_INSERT [dbo].[workout] ON
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (1, N'Upper Body Builder 1', N'Works out your back and chest', 2, 1, CAST(0x002FBB002B3A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (2, N'Leg Workout 1', N'Works out your legs and lower back', 2, 1, CAST(0x002FBB002B3A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (3, N'Running Core Workout', N'Build endurance and core strength', 1, 1, CAST(0x002FBB002B3A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (4, N'Endurance Endurance Endurance', N'Build endurance and core strength', 1, 1, CAST(0x002FBB002B3A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (5, N'Sprints', N'Build endurance and leg strength', 1, 1, CAST(0x002FBB002B3A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (6, N'Running Upper Body Workout', N'Build endurance and upper body strength', 1, 1, CAST(0x002FBB002B3A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (7, N'Ab Workout', N'Build core strength', 2, 1, CAST(0x002FBB002B3A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (30, N'Upper Body Builder 2', N'Strengthens your back, chest, and arms', 2, 5, CAST(0x00CB0301313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (31, N'Pull up sprints', N'Fast low rep pull up workout', 2, 5, CAST(0x00350401313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (32, N'Ab Sculptor', N'Focuses on building and toning ab muscles', 2, 5, CAST(0x00960401313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (33, N'Leg Workout 2', N'Strengthens leg muscles ', 2, 5, CAST(0x00580501313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (34, N'Stretches', N'Standard stretch workout.', 3, 5, CAST(0x00030601313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (35, N'Back Workout 1', N'Mixed back exercises', 2, 5, CAST(0x00E40601313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (36, N'Stomach and Back', N'Back and ab workout mix', 2, 5, CAST(0x00480701313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (37, N'Everything In One', N'A workout with a little bit of everything, focusing on muscle building', 2, 3, CAST(0x00CB0801313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (38, N'Leg S & E', N'Focuses on leg strength and endurance', 1, 3, CAST(0x003E0901313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (39, N'Back and Legs 17', N'Strengthens back and leg muscles', 2, 3, CAST(0x00792601313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (40, N'Distance Running', N'Long running exercises', 1, 3, CAST(0x00AF2601313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (41, N'Planking', N'All plank exercises', 2, 3, CAST(0x000D2701313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (42, N'Short Back and Chest ', N'Strengthens back and chest muscles', 2, 3, CAST(0x006D2701313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (43, N'Advanced Upper Body', N'Difficult upper body workout', 2, 3, CAST(0x00A72701313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (44, N'Abs and Cardio', N'Running and ab workouts', 1, 2, CAST(0x000F2801313A0B0000 AS DateTime2))
INSERT [dbo].[workout] ([id], [name], [description], [category_id], [created_by_user_id], [created_at]) VALUES (45, N'Iron Man', N'A variety of high rep exercises', 2, 2, CAST(0x005D2801313A0B0000 AS DateTime2))
SET IDENTITY_INSERT [dbo].[workout] OFF
/****** Object:  Table [dbo].[workout_rating]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout_rating]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[workout_rating](
	[workout_id] [int] NOT NULL,
	[average_rating] [decimal](3, 1) NOT NULL,
	[times_rated] [int] NOT NULL,
 CONSTRAINT [PK_workout_rating_workout_id] PRIMARY KEY CLUSTERED 
(
	[workout_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'workout_rating', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.workout_rating' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'workout_rating'
GO
INSERT [dbo].[workout_rating] ([workout_id], [average_rating], [times_rated]) VALUES (1, CAST(8.0 AS Decimal(3, 1)), 1)
INSERT [dbo].[workout_rating] ([workout_id], [average_rating], [times_rated]) VALUES (2, CAST(7.0 AS Decimal(3, 1)), 10)
INSERT [dbo].[workout_rating] ([workout_id], [average_rating], [times_rated]) VALUES (3, CAST(6.0 AS Decimal(3, 1)), 3)
INSERT [dbo].[workout_rating] ([workout_id], [average_rating], [times_rated]) VALUES (4, CAST(8.0 AS Decimal(3, 1)), 3)
INSERT [dbo].[workout_rating] ([workout_id], [average_rating], [times_rated]) VALUES (5, CAST(9.0 AS Decimal(3, 1)), 16)
INSERT [dbo].[workout_rating] ([workout_id], [average_rating], [times_rated]) VALUES (6, CAST(10.0 AS Decimal(3, 1)), 2)
INSERT [dbo].[workout_rating] ([workout_id], [average_rating], [times_rated]) VALUES (7, CAST(4.0 AS Decimal(3, 1)), 9)
/****** Object:  Table [dbo].[workout_exercise]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[workout_exercise](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[workout_id] [int] NOT NULL,
	[exercise_id] [int] NOT NULL,
	[position] [int] NOT NULL,
	[duration] [decimal](5, 2) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_workout_exercise_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise]') AND name = N'fk_Workout_has_Exercise_Exercise1_idx')
CREATE NONCLUSTERED INDEX [fk_Workout_has_Exercise_Exercise1_idx] ON [dbo].[workout_exercise] 
(
	[exercise_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise]') AND name = N'fk_Workout_has_Exercise_Workout1_idx')
CREATE NONCLUSTERED INDEX [fk_Workout_has_Exercise_Workout1_idx] ON [dbo].[workout_exercise] 
(
	[workout_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'workout_exercise', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.workout_exercise' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'workout_exercise'
GO
SET IDENTITY_INSERT [dbo].[workout_exercise] ON
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (1, 1, 2, 1, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (2, 1, 1, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (3, 1, 3, 3, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (4, 1, 4, 4, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (5, 2, 5, 1, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (6, 2, 6, 2, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (7, 2, 7, 3, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (8, 2, 12, 4, CAST(40.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (9, 2, 5, 5, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (10, 2, 6, 6, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (11, 2, 7, 7, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (12, 3, 13, 1, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (13, 3, 12, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (14, 3, 13, 3, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (15, 3, 12, 4, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (16, 3, 11, 5, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (17, 3, 10, 6, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (18, 4, 13, 1, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (19, 4, 14, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (20, 4, 15, 3, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (21, 4, 8, 4, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (22, 4, 9, 5, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (23, 4, 13, 6, CAST(2.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (24, 4, 14, 7, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (25, 4, 15, 8, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (26, 5, 13, 1, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (27, 5, 13, 2, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (28, 5, 13, 3, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (29, 6, 3, 1, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (30, 6, 13, 2, CAST(2.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (31, 6, 1, 3, CAST(30.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (32, 7, 10, 1, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (33, 7, 11, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (34, 7, 12, 3, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (41, 30, 2, 1, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (42, 30, 1, 2, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (43, 30, 3, 3, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (44, 30, 2, 4, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (45, 30, 4, 5, CAST(8.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (46, 30, 1, 6, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (47, 31, 3, 1, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (48, 31, 3, 2, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (49, 31, 3, 3, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (50, 31, 3, 4, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (51, 31, 3, 5, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (52, 31, 3, 6, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (53, 31, 3, 7, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (54, 31, 3, 8, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (55, 32, 12, 1, CAST(30.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (56, 32, 10, 2, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (57, 32, 12, 3, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (58, 32, 11, 4, CAST(30.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (59, 32, 21, 5, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (60, 32, 22, 6, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (61, 32, 23, 7, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (62, 33, 29, 1, CAST(30.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (63, 33, 30, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (64, 33, 29, 3, CAST(25.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (65, 33, 30, 4, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (66, 33, 29, 5, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (67, 33, 30, 6, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (68, 33, 29, 7, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (69, 33, 30, 8, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (70, 34, 19, 1, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (71, 34, 20, 2, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (72, 34, 14, 3, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (73, 34, 15, 4, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (74, 34, 16, 5, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (75, 34, 17, 6, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (76, 34, 18, 7, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (77, 34, 19, 8, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (78, 34, 20, 9, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (79, 35, 14, 1, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (80, 35, 15, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (81, 35, 16, 3, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (82, 35, 17, 4, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (83, 35, 18, 5, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (84, 35, 25, 6, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (85, 35, 4, 7, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (86, 35, 3, 8, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (87, 36, 14, 1, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (88, 36, 8, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (89, 36, 24, 3, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (90, 36, 27, 4, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (91, 36, 26, 5, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (92, 36, 14, 6, CAST(5.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (93, 36, 8, 7, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (94, 37, 1, 1, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (95, 37, 11, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (96, 37, 16, 3, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (97, 37, 6, 4, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (98, 37, 13, 5, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (99, 37, 24, 6, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (100, 37, 8, 7, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (101, 37, 3, 8, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (102, 37, 4, 9, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (103, 37, 5, 10, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (104, 38, 13, 1, CAST(2.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (105, 38, 5, 2, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (106, 38, 7, 3, CAST(10.00 AS Decimal(5, 2)))
GO
print 'Processed 100 total records'
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (107, 38, 13, 4, CAST(1.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (108, 38, 29, 5, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (109, 38, 30, 6, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (110, 38, 5, 7, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (111, 38, 6, 8, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (112, 39, 15, 1, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (113, 39, 29, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (114, 39, 17, 3, CAST(40.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (115, 39, 30, 4, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (116, 40, 13, 1, CAST(3.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (117, 40, 6, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (118, 40, 13, 3, CAST(3.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (119, 40, 6, 4, CAST(30.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (120, 41, 8, 1, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (121, 41, 9, 2, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (122, 41, 28, 3, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (123, 41, 8, 4, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (124, 41, 9, 5, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (125, 41, 28, 6, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (126, 42, 1, 1, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (127, 42, 14, 2, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (128, 42, 18, 3, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (129, 42, 1, 4, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (130, 43, 1, 1, CAST(40.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (131, 43, 3, 2, CAST(15.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (132, 43, 1, 3, CAST(40.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (133, 43, 11, 4, CAST(70.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (134, 43, 4, 5, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (135, 43, 1, 6, CAST(30.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (136, 44, 12, 1, CAST(30.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (137, 44, 13, 2, CAST(12.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (138, 44, 24, 3, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (139, 44, 12, 4, CAST(20.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (140, 45, 1, 1, CAST(50.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (141, 45, 13, 2, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (142, 45, 11, 3, CAST(60.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (143, 45, 3, 4, CAST(10.00 AS Decimal(5, 2)))
INSERT [dbo].[workout_exercise] ([id], [workout_id], [exercise_id], [position], [duration]) VALUES (144, 45, 13, 5, CAST(2.00 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[workout_exercise] OFF
/****** Object:  Table [dbo].[user_workout]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_workout]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[user_workout](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[workout_id] [int] NOT NULL,
	[number_of_ex_completed] [int] NOT NULL,
	[date_started] [date] NULL DEFAULT (NULL),
	[date_finished] [date] NULL DEFAULT (NULL),
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_user_workout_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[user_workout]') AND name = N'fk_User_has_Workout_User1_idx')
CREATE NONCLUSTERED INDEX [fk_User_has_Workout_User1_idx] ON [dbo].[user_workout] 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[user_workout]') AND name = N'fk_User_has_Workout_Workout1_idx')
CREATE NONCLUSTERED INDEX [fk_User_has_Workout_Workout1_idx] ON [dbo].[user_workout] 
(
	[workout_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'user_workout', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.user_workout' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_workout'
GO
SET IDENTITY_INSERT [dbo].[user_workout] ON
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (1, 3, 7, 3, CAST(0x2B3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (2, 3, 4, 8, CAST(0x2B3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (3, 3, 2, 7, CAST(0x2B3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (6, 3, 7, 3, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (7, 3, 4, 8, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (8, 3, 2, 7, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (9, 3, 2, 7, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (10, 3, 3, 6, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (11, 3, 1, 4, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (12, 3, 1, 4, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (13, 3, 1, 4, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
INSERT [dbo].[user_workout] ([id], [user_id], [workout_id], [number_of_ex_completed], [date_started], [date_finished]) VALUES (14, 3, 1, 4, CAST(0x2C3A0B00 AS Date), CAST(0x2C3A0B00 AS Date))
SET IDENTITY_INSERT [dbo].[user_workout] OFF
/****** Object:  Table [dbo].[user_favorite_workout]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[user_favorite_workout](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[workout_id] [int] NOT NULL,
	[timestamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_user_favorite_workout_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [user_favorite_workout$uq_User_Workout_idx] UNIQUE NONCLUSTERED 
(
	[user_id] ASC,
	[workout_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]') AND name = N'fk_User_Favorite_Workout_User1_idx')
CREATE NONCLUSTERED INDEX [fk_User_Favorite_Workout_User1_idx] ON [dbo].[user_favorite_workout] 
(
	[user_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]') AND name = N'fk_User_Favorite_Workout_Workout1_idx')
CREATE NONCLUSTERED INDEX [fk_User_Favorite_Workout_Workout1_idx] ON [dbo].[user_favorite_workout] 
(
	[workout_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'user_favorite_workout', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.user_favorite_workout' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'user_favorite_workout'
GO
/****** Object:  Table [dbo].[comment]    Script Date: 07/16/2015 14:52:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[comment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[comment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[message] [nvarchar](max) NOT NULL,
	[timestamp] [timestamp] NOT NULL,
	[User_id] [int] NOT NULL,
	[Workout_id] [int] NOT NULL,
	[date_created] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_comment_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[comment]') AND name = N'fk_Comment_User1_idx')
CREATE NONCLUSTERED INDEX [fk_Comment_User1_idx] ON [dbo].[comment] 
(
	[User_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[comment]') AND name = N'fk_Comment_Workout1_idx')
CREATE NONCLUSTERED INDEX [fk_Comment_Workout1_idx] ON [dbo].[comment] 
(
	[Workout_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_SSMA_SOURCE' , N'SCHEMA',N'dbo', N'TABLE',N'comment', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_SSMA_SOURCE', @value=N'gofitdb.comment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'comment'
GO
SET IDENTITY_INSERT [dbo].[comment] ON
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (1, N'I found this workout effective. I would recommend it to a beginner.', 4, 1, CAST(0x0082B900133A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (2, N'Was very challenging for me but effective. I loved it.', 5, 2, CAST(0x0018BB00133A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (3, N'Please, post more workouts like this.', 3, 3, CAST(0x006C0201133A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (4, N'My body and I was working out together. We both found this workout challenging.', 3, 5, CAST(0x00394E01133A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (5, N'Greate workout, A+', 5, 1, CAST(0x00978100143A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (6, N'It was to easy. And I like intensive working out.', 6, 3, CAST(0x00449D00143A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (7, N'I liked the combination of exercises here. They are not too difficult and not too easy.', 3, 6, CAST(0x00A8D900143A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (8, N'I recommend this workout if you just starting to build you upper body muscles', 3, 1, CAST(0x0097A900153A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (9, N'Fun and challenging. Thanks for posting it!', 4, 2, CAST(0x001A6C00163A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (10, N'I did not think I liked working out until I came accross this site. This workout was the first one I haved tried.', 4, 7, CAST(0x00691401163A0B0000 AS DateTime2))
INSERT [dbo].[comment] ([id], [message], [User_id], [Workout_id], [date_created]) VALUES (11, N'I found this too easy for me.', 6, 1, CAST(0x00A23600173A0B0000 AS DateTime2))
SET IDENTITY_INSERT [dbo].[comment] OFF
/****** Object:  ForeignKey [exercise$fk_Exercise_Type1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise]  WITH CHECK ADD  CONSTRAINT [exercise$fk_Exercise_Type1] FOREIGN KEY([type_id])
REFERENCES [dbo].[type] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] CHECK CONSTRAINT [exercise$fk_Exercise_Type1]
GO
/****** Object:  ForeignKey [exercise$fk_Exercise_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise]  WITH CHECK ADD  CONSTRAINT [exercise$fk_Exercise_User1] FOREIGN KEY([created_by_user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] CHECK CONSTRAINT [exercise$fk_Exercise_User1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_Category1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout]  WITH CHECK ADD  CONSTRAINT [workout$fk_Workout_Category1] FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] CHECK CONSTRAINT [workout$fk_Workout_Category1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout]  WITH CHECK ADD  CONSTRAINT [workout$fk_Workout_User1] FOREIGN KEY([created_by_user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] CHECK CONSTRAINT [workout$fk_Workout_User1]
GO
/****** Object:  ForeignKey [workout_rating$fk_Workout_Rating_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_rating$fk_Workout_Rating_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_rating]'))
ALTER TABLE [dbo].[workout_rating]  WITH CHECK ADD  CONSTRAINT [workout_rating$fk_Workout_Rating_Workout1] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_rating$fk_Workout_Rating_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_rating]'))
ALTER TABLE [dbo].[workout_rating] CHECK CONSTRAINT [workout_rating$fk_Workout_Rating_Workout1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Exercise1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise]  WITH CHECK ADD  CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1] FOREIGN KEY([exercise_id])
REFERENCES [dbo].[exercise] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] CHECK CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise]  WITH CHECK ADD  CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] CHECK CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout]  WITH CHECK ADD  CONSTRAINT [user_workout$fk_User_has_Workout_User1] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] CHECK CONSTRAINT [user_workout$fk_User_has_Workout_User1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout]  WITH CHECK ADD  CONSTRAINT [user_workout$fk_User_has_Workout_Workout1] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] CHECK CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout]  WITH CHECK ADD  CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] CHECK CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout]  WITH CHECK ADD  CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] CHECK CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_User1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [comment$fk_Comment_User1] FOREIGN KEY([User_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [comment$fk_Comment_User1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_Workout1]    Script Date: 07/16/2015 14:52:13 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [comment$fk_Comment_Workout1] FOREIGN KEY([Workout_id])
REFERENCES [dbo].[workout] ([id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [comment$fk_Comment_Workout1]
GO

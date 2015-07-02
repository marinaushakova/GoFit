USE [master]
GO
/****** Object:  Database [gofitdb]    Script Date: 06/26/2015 13:43:05 ******/
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
/****** Object:  ForeignKey [exercise$fk_Exercise_Type1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_Type1]
GO
/****** Object:  ForeignKey [exercise$fk_Exercise_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_User1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_Category1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_Category1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_User1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Exercise1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_User1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_User1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_Workout1]
GO
/****** Object:  Table [dbo].[comment]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_User1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [comment$fk_Comment_Workout1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__comment__timesta__00551192]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[comment] DROP CONSTRAINT [DF__comment__timesta__00551192]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[comment]') AND type in (N'U'))
DROP TABLE [dbo].[comment]
GO
/****** Object:  Table [dbo].[user_favorite_workout]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user_favo__times__0EA330E9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [DF__user_favo__times__0EA330E9]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]') AND type in (N'U'))
DROP TABLE [dbo].[user_favorite_workout]
GO
/****** Object:  Table [dbo].[user_workout]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_User1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user_work__date___108B795B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [DF__user_work__date___108B795B]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user_work__date___117F9D94]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [DF__user_work__date___117F9D94]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user_work__times__1273C1CD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [DF__user_work__times__1273C1CD]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user_workout]') AND type in (N'U'))
DROP TABLE [dbo].[user_workout]
GO
/****** Object:  Table [dbo].[workout_exercise]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__workout_e__times__164452B1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [DF__workout_e__times__164452B1]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise]') AND type in (N'U'))
DROP TABLE [dbo].[workout_exercise]
GO
/****** Object:  Table [dbo].[workout]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_Category1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_User1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__workout__timesta__145C0A3F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [DF__workout__timesta__145C0A3F]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[workout]') AND type in (N'U'))
DROP TABLE [dbo].[workout]
GO
/****** Object:  Table [dbo].[exercise]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_Type1]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_User1]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__exercise__link__023D5A04]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [DF__exercise__link__023D5A04]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__exercise__timest__03317E3D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [DF__exercise__timest__03317E3D]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[exercise]') AND type in (N'U'))
DROP TABLE [dbo].[exercise]
GO
/****** Object:  Table [dbo].[type]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__type__timestamp__0519C6AF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[type] DROP CONSTRAINT [DF__type__timestamp__0519C6AF]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[type]') AND type in (N'U'))
DROP TABLE [dbo].[type]
GO
/****** Object:  Table [dbo].[user]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__fname__07020F21]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__fname__07020F21]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__lname__07F6335A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__lname__07F6335A]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__is_male__08EA5793]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__is_male__08EA5793]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__is_admin__09DE7BCC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__is_admin__09DE7BCC]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__weight__0AD2A005]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__weight__0AD2A005]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__height__0BC6C43E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__height__0BC6C43E]
END
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__user__timestamp__0CBAE877]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[user] DROP CONSTRAINT [DF__user__timestamp__0CBAE877]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[user]') AND type in (N'U'))
DROP TABLE [dbo].[user]
GO
/****** Object:  Table [dbo].[category]    Script Date: 06/26/2015 13:43:07 ******/
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__category__timest__7E6CC920]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[category] DROP CONSTRAINT [DF__category__timest__7E6CC920]
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[category]') AND type in (N'U'))
DROP TABLE [dbo].[category]
GO
/****** Object:  Table [dbo].[category]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[user]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[type]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[exercise]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[workout]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[workout_exercise]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[user_workout]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[user_favorite_workout]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  Table [dbo].[comment]    Script Date: 06/26/2015 13:43:07 ******/
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
/****** Object:  ForeignKey [exercise$fk_Exercise_Type1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise]  WITH CHECK ADD  CONSTRAINT [exercise$fk_Exercise_Type1] FOREIGN KEY([type_id])
REFERENCES [dbo].[type] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_Type1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] CHECK CONSTRAINT [exercise$fk_Exercise_Type1]
GO
/****** Object:  ForeignKey [exercise$fk_Exercise_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise]  WITH CHECK ADD  CONSTRAINT [exercise$fk_Exercise_User1] FOREIGN KEY([created_by_user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[exercise$fk_Exercise_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[exercise]'))
ALTER TABLE [dbo].[exercise] CHECK CONSTRAINT [exercise$fk_Exercise_User1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_Category1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout]  WITH CHECK ADD  CONSTRAINT [workout$fk_Workout_Category1] FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_Category1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] CHECK CONSTRAINT [workout$fk_Workout_Category1]
GO
/****** Object:  ForeignKey [workout$fk_Workout_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout]  WITH CHECK ADD  CONSTRAINT [workout$fk_Workout_User1] FOREIGN KEY([created_by_user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout$fk_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout]'))
ALTER TABLE [dbo].[workout] CHECK CONSTRAINT [workout$fk_Workout_User1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Exercise1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise]  WITH CHECK ADD  CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1] FOREIGN KEY([exercise_id])
REFERENCES [dbo].[exercise] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Exercise1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] CHECK CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
GO
/****** Object:  ForeignKey [workout_exercise$fk_Workout_has_Exercise_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise]  WITH CHECK ADD  CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[workout_exercise$fk_Workout_has_Exercise_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[workout_exercise]'))
ALTER TABLE [dbo].[workout_exercise] CHECK CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout]  WITH CHECK ADD  CONSTRAINT [user_workout$fk_User_has_Workout_User1] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] CHECK CONSTRAINT [user_workout$fk_User_has_Workout_User1]
GO
/****** Object:  ForeignKey [user_workout$fk_User_has_Workout_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout]  WITH CHECK ADD  CONSTRAINT [user_workout$fk_User_has_Workout_Workout1] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_workout$fk_User_has_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_workout]'))
ALTER TABLE [dbo].[user_workout] CHECK CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout]  WITH CHECK ADD  CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] CHECK CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
GO
/****** Object:  ForeignKey [user_favorite_workout$fk_User_Favorite_Workout_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout]  WITH CHECK ADD  CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1] FOREIGN KEY([workout_id])
REFERENCES [dbo].[workout] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[user_favorite_workout$fk_User_Favorite_Workout_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[user_favorite_workout]'))
ALTER TABLE [dbo].[user_favorite_workout] CHECK CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_User1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [comment$fk_Comment_User1] FOREIGN KEY([User_id])
REFERENCES [dbo].[user] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_User1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [comment$fk_Comment_User1]
GO
/****** Object:  ForeignKey [comment$fk_Comment_Workout1]    Script Date: 06/26/2015 13:43:07 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [comment$fk_Comment_Workout1] FOREIGN KEY([Workout_id])
REFERENCES [dbo].[workout] ([id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[comment$fk_Comment_Workout1]') AND parent_object_id = OBJECT_ID(N'[dbo].[comment]'))
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [comment$fk_Comment_Workout1]
GO

/** Insert data **/

use gofitdb;

INSERT INTO type([name],[measure]) VALUES('distance', 'miles');
INSERT INTO type([name],[measure]) VALUES('quantity', 'unit');
INSERT INTO type([name],[measure]) VALUES('duration', 'seconds');
INSERT INTO type([name],[measure]) VALUES('duration', 'minutes');
INSERT INTO type([name],[measure]) VALUES('duration', 'hours');

INSERT INTO category([name], [description]) VALUES('endurance', 'Endurance workouts help keep your heart, lungs, and circulatory system healthy.');
INSERT INTO category([name], [description]) VALUES('strength', 'Strength workouts build muscle size and power');
INSERT INTO category([name], [description]) VALUES('flexibility', 'Flexibility workouts stretch your muscles and help protect you body from exercise incurred injuries');

INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin', '3c1c88f0b0fec9b5f539c3d6b0577bd138bd157d604125a53e60e35cf940a5fe', 'Bob', 'Jones', 1, 1);
INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin2', 'cf312ac87c1b48968a08cb1b2809a5d0223668ad334ac322ca7f7ab3bce08929', 'Jane', 'Forsythe', 0, 1);

INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('hunts', '5faf9c3e4b1fe468cb6fc9e7cc3bd1eb93f3cad399d45e9a358a14c2c7b483d6', 'Sharon', 'Hunt', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('SharonArmstrong', 'b78ca34b54a6da5d0b8d95a584276450f3c6dbdaafa0d6b4c26bdf893f234573', 'Sharon', 'Armstrong', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('dunnj', 'c98f2f0ce893c007f05c4c2904a5f66e72dd2ea2b8824d3927a3a763be4fb235', 'Joseph', 'Dunn', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('AnneBailey', '46bab764e7e864db5484015e720e5170a6b9f32fb2ab5b1af3cbe7b32dcc9659', 'Anne', 'Bailey', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('FrankSmith', '4f01f4fcf95a540b2623301119fd13a914c91a669cae2df5beb284bed4bc166e', 'Frank', 'Smith', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('DorothyMoreno', '2acf57de58e9bc67475e872727864dd07c11268e652cdd5649de9d7a94c0ec71', 'Dorothy', 'Moreno', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('CharlesDavis', '9ddc3eb834f671e17f1bac2f0b19ae4b1f3befec7134cd95ccc6f57c7864f575', 'Charles', 'Davis', 1, 0);

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=bJ3Ogh5mFE4', 'Standard push-ups', 'Standard Push-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=pUJnPMjYLxU', 'An easier variation of the standard push-up', 'Knee Down Push-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=Ir8IrbYcM8w', 'Pull-ups', 'Pull-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=_71FpEaq-fQ', 'Chin-ups', 'Chin-ups');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=COKYKgQ8KR0', 'Lunge', 'Lunge');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=7mDWDlzFobQ', 'Walking lunge', 'Walking Lunge');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=y7Iug7eC0dk', 'Jumping lunge', 'Jumping Lunge');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=UOGvtqv856A', 'Mountain climber plank', 'Mountain Climber Plank');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=NXr4Fw8q60o', 'Side plank', 'Side Plank');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=HJLE_VQ3Knc', 'Oblique V-up', 'Oblique V-up');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=jDwoBqPH0jk', 'Sit-up', 'Sit-up');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=MKmrqcoCZ-M', 'Stomach crunch', 'Stomach Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (1, 1, GETDATE(), 'https://www.youtube.com/watch?v=e7m205ZIxBE', 'Running', 'Running');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=9PxkxHxGRvU', 'T-Pose', 'T-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 1, GETDATE(), 'https://www.youtube.com/watch?v=i4XOoQUtaCU', 'W-Pose', 'W-Pose');

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=xKy-l9Pf0cQ', 'O-Pose', 'O-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=JY9SlansmJ4', 'Y-Pose', 'Y-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=wmVMcHeqoZc', 'I-Pose', 'I-Pose');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-15', 'https://www.youtube.com/watch?v=Aa6zdmje-c4', 'Cobra stretch', 'Cobra stretch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-12', 'https://www.youtube.com/watch?v=ZiNXOE5EsZw', 'Cat Stretch', 'Cat Stretch');

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=7rRWy7-Gokg', 'Reverse Crunch', 'Reverse Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=FrFyUbxs1uQ', 'Side Crunch', 'Side Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=5laCNeFnKdE', 'One-sided Crunch', 'One-sided Crunch');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-13', 'https://www.youtube.com/watch?v=VSp0z7Mp5IU', 'Inchworm ', 'Inchworm');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-12', 'https://www.youtube.com/watch?v=ihG0E_4_tCM', 'Scissor Lifts', 'Scissor Lifts');

INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=rXAbcneAr3I', 'Glute Bridge March', 'Glute Bridge March');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=HPnFTmjjDDA', 'Hollow Rock', 'Hollow Rock');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=XMxHTNPPgxM', 'Plank', 'Plank');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-10', 'https://www.youtube.com/watch?v=jGQ8_IMPQOY', 'Squat ', 'Squat ');
INSERT INTO exercise([type_id], [created_by_user_id], [created_at], [link], [description], [name]) 
VALUES (2, 2, '2015-06-12', 'https://www.youtube.com/watch?v=U4s4mEQ5VqU', 'Squat Jump', 'Squat Jump');

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Upper Body Builder 1', 'Works out your back and chest', 2, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Leg Workout 1', 'Works out your legs and lower back', 2, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Running Core Workout', 'Build endurance and core strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Endurance Endurance Endurance', 'Build endurance and core strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Sprints', 'Build endurance and leg strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Running Upper Body Workout', 'Build endurance and upper body strength', 1, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Ab Workout', 'Build core strength', 2, 1, GETDATE());

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout8', 'Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. Some really long description. ', 2, 1, GETDATE());
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout9', 'desc9', 3, 3, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout10', 'desc10', 3, 3, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout11', 'desc11', 1, 3, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout12', 'desc12', 3, 1, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout13', 'desc13', 2, 2, '2015-06-12');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout14', 'desc14', 2, 2, '2015-06-12');

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout15', 'desc15', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout16', 'desc16', 3, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout17', 'desc17', 2, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout18', 'desc18', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout19', 'desc19', 3, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout20', 'desc20', 1, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout21', 'desc21', 1, 2, '2015-06-13');

INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout22', 'desc22', 3, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout23', 'desc23', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout24', 'desc24', 2, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout25', 'desc25', 3, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout26', 'desc26', 3, 3, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout27', 'desc27', 3, 2, '2015-06-13');
INSERT INTO workout([name],[description],[category_id],[created_by_user_id],[created_at])
VALUES('Workout28', 'desc28', 3, 3, '2015-06-14');


INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 2, 1, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 1, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 3, 3, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (1, 4, 4, 10);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 5, 1, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 6, 2, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 7, 3, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 12, 4, 40);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 5, 5, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 6, 6, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (2, 7, 7, 10);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 13, 1, .5);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 12, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 13, 3, .5);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 12, 4, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 11, 5, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (3, 10, 6, 10);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 13, 1, 1);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 14, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 15, 3, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 8, 4, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 9, 5, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 13, 6, 2);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 14, 7, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (4, 15, 8, 20);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (5, 13, 1, .25);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (5, 13, 2, .25);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (5, 13, 3, .25);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (6, 3, 1, 10);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (6, 13, 2, 2);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (6, 1, 3, 30);

INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (7, 10, 1, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (7, 11, 2, 20);
INSERT INTO workout_exercise (workout_id, exercise_id, position, duration) values (7, 12, 3, 20);

INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('I found this workout effective. I would recommend it to a beginner.', 4, 1, '2015-6-15 13:11:30');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('Was very challenging for me but effective. I loved it.', 5, 2, '2015-6-15 13:18:16');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('Please, post more workouts like this.', 3, 3, '2015-6-15 18:22:36');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('My body and I was working out together. We both found this workout challenging.', 3, 5, '2015-6-15 23:46:01');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('Greate workout, A+', 5, 1, '2015-6-16 09:12:55');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('It was to easy. And I like intensive working out.', 6, 3, '2015-6-16 11:11:00');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('I liked the combination of exercises here. They are not too difficult and not too easy.', 3, 6, '2015-6-16 15:28:40');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('I recommend this workout if you just starting to build you upper body muscles', 3, 1, '2015-6-17 12:03:35');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('Fun and challenging. Thanks for posting it!', 4, 2, '2015-6-18 07:41:14');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('I did not think I liked working out until I came accross this site. This workout was the first one I haved tried.', 4, 7, '2015-6-18 19:39:21');
INSERT INTO comment ([message], [User_id], [Workout_id], [date_created]) values ('I found this too easy for me.', 6, 1, '2015-6-19 03:53:06');
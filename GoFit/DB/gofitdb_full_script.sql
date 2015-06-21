/** DROP/CREATE DB **/

USE [master]
GO

/****** Object:  Database [gofitdb]    Script Date: 06/10/2015 08:57:14 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'gofitdb')
DROP DATABASE [gofitdb]
GO

USE [master]
GO

/****** Object:  Database [gofitdb]    Script Date: 06/10/2015 08:57:14 ******/
CREATE DATABASE [gofitdb] ON  PRIMARY 
( NAME = N'gofitdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\gofitdb.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'gofitdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\gofitdb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
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

/** DROP/CREATE EMPTY TABLES **/


USE gofitdb
GO
 IF NOT EXISTS(SELECT * FROM sys.schemas WHERE [name] = N'dbo')      
     EXEC (N'CREATE SCHEMA dbo')                                   
 GO                                                               

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'category'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'category'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[category]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[category]
(
   [id] int IDENTITY(1, 1)  NOT NULL,
   [name] nvarchar(45)  NOT NULL,
   [description] nvarchar(max)  NOT NULL,
   [timestamp] datetime  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.category',
        N'SCHEMA', N'dbo',
        N'TABLE', N'category'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'exercise'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'exercise'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[exercise]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[exercise]
(
   [id] int IDENTITY(1, 1)  NOT NULL,
   [type_id] int  NOT NULL,
   [created_by_user_id] int  NOT NULL,
   [created_at] datetime2(0)  NOT NULL,
   [link] nvarchar(255)  NULL,
   [description] nvarchar(max)  NULL,
   [timestamp] datetime  NOT NULL,
   [name] nvarchar(50)  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.exercise',
        N'SCHEMA', N'dbo',
        N'TABLE', N'exercise'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'type'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'type'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[type]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[type]
(
   [id] int IDENTITY(1, 1)  NOT NULL,
   [name] nvarchar(45)  NOT NULL,
   [measure] nvarchar(45)  NOT NULL,
   [timestamp] datetime  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.type',
        N'SCHEMA', N'dbo',
        N'TABLE', N'type'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[user]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[user]
(
   [id] int IDENTITY(1, 1)  NOT NULL,
   [username] nvarchar(45)  NOT NULL,
   [password] nvarchar(200)  NOT NULL,
   [fname] nvarchar(45)  NULL,
   [lname] nvarchar(45)  NULL,
   [is_male] smallint  NULL,

   /*
   *   SSMA informational messages:
   *   M2SS0052: string literal was converted to NUMERIC literal
   */

   [is_admin] smallint  NOT NULL,
   [weight] int  NULL,
   [height] decimal(3, 1)  NULL,
   [timestamp] datetime  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.`user`',
        N'SCHEMA', N'dbo',
        N'TABLE', N'user'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_favorite_exercise'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user_favorite_exercise'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[user_favorite_exercise]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[user_favorite_exercise]
(
   [id] int IDENTITY(1, 1)  NOT NULL,
   [User_id] int  NOT NULL,
   [Exercise_id] int  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.user_favorite_exercise',
        N'SCHEMA', N'dbo',
        N'TABLE', N'user_favorite_exercise'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_favorite_workout'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user_favorite_workout'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[user_favorite_workout]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[user_favorite_workout]
(
   [id] int IDENTITY(1, 1)  NOT NULL,
   [user_id] int  NOT NULL,
   [workout_id] int  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.user_favorite_workout',
        N'SCHEMA', N'dbo',
        N'TABLE', N'user_favorite_workout'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_workout'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'user_workout'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[user_workout]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[user_workout]
(
   [id] int IDENTITY(1, 1) NOT NULL,
   [user_id] int  NOT NULL,
   [workout_id] int  NOT NULL,
   [number_of_ex_completed] int  NOT NULL,
   [date_started] date  NULL,
   [date_finished] date  NULL,
   [timestamp] datetime  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.user_workout',
        N'SCHEMA', N'dbo',
        N'TABLE', N'user_workout'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'workout'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'workout'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[workout]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[workout]
(
   [id] int IDENTITY(1, 1)  NOT NULL,
   [name] nvarchar(45)  NOT NULL,
   [description] nvarchar(max)  NOT NULL,
   [category_id] int  NOT NULL,
   [created_by_user_id] int  NOT NULL,
   [created_at] datetime2(0)  NOT NULL,
   [timestamp] datetime  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.workout',
        N'SCHEMA', N'dbo',
        N'TABLE', N'workout'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF  EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'workout_exercise'  AND sc.name=N'dbo'  AND type in (N'U'))
BEGIN

  DECLARE @drop_statement nvarchar(500)

  DECLARE drop_cursor CURSOR FOR
      SELECT 'alter table '+quotename(schema_name(ob.schema_id))+
      '.'+quotename(object_name(ob.object_id))+ ' drop constraint ' + quotename(fk.name) 
      FROM sys.objects ob INNER JOIN sys.foreign_keys fk ON fk.parent_object_id = ob.object_id
      WHERE fk.referenced_object_id = 
          (
             SELECT so.object_id 
             FROM sys.objects so JOIN sys.schemas sc
             ON so.schema_id = sc.schema_id
             WHERE so.name = N'workout_exercise'  AND sc.name=N'dbo'  AND type in (N'U')
           )

  OPEN drop_cursor

  FETCH NEXT FROM drop_cursor
  INTO @drop_statement

  WHILE @@FETCH_STATUS = 0
  BEGIN
     EXEC (@drop_statement)

     FETCH NEXT FROM drop_cursor
     INTO @drop_statement
  END

  CLOSE drop_cursor
  DEALLOCATE drop_cursor

  DROP TABLE [dbo].[workout_exercise]
END 
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE 
[dbo].[workout_exercise]
(
   [id] int IDENTITY(1, 1) NOT NULL,
   [workout_id] int  NOT NULL,
   [exercise_id] int  NOT NULL,
   [position] int  NOT NULL,
   [duration] decimal(5, 2)  NOT NULL,
   [timestamp] datetime  NOT NULL
)
GO
BEGIN TRY
    EXEC sp_addextendedproperty
        N'MS_SSMA_SOURCE', N'gofitdb.workout_exercise',
        N'SCHEMA', N'dbo',
        N'TABLE', N'workout_exercise'
END TRY
BEGIN CATCH
    IF (@@TRANCOUNT > 0) ROLLBACK
    PRINT ERROR_MESSAGE()
END CATCH
GO

USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_category_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[category] DROP CONSTRAINT [PK_category_id]
 GO



ALTER TABLE [dbo].[category]
 ADD CONSTRAINT [PK_category_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_exercise_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [PK_exercise_id]
 GO



ALTER TABLE [dbo].[exercise]
 ADD CONSTRAINT [PK_exercise_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_type_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[type] DROP CONSTRAINT [PK_type_id]
 GO



ALTER TABLE [dbo].[type]
 ADD CONSTRAINT [PK_type_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[user] DROP CONSTRAINT [PK_user_id]
 GO



ALTER TABLE [dbo].[user]
 ADD CONSTRAINT [PK_user_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_favorite_exercise_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[user_favorite_exercise] DROP CONSTRAINT [PK_user_favorite_exercise_id]
 GO



ALTER TABLE [dbo].[user_favorite_exercise]
 ADD CONSTRAINT [PK_user_favorite_exercise_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_favorite_workout_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [PK_user_favorite_workout_id]
 GO



ALTER TABLE [dbo].[user_favorite_workout]
 ADD CONSTRAINT [PK_user_favorite_workout_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_user_workout_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [PK_user_workout_id]
 GO



ALTER TABLE [dbo].[user_workout]
 ADD CONSTRAINT [PK_user_workout_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_workout_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [PK_workout_id]
 GO



ALTER TABLE [dbo].[workout]
 ADD CONSTRAINT [PK_workout_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'PK_workout_exercise_id'  AND sc.name=N'dbo'  AND type in (N'PK'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [PK_workout_exercise_id]
 GO



ALTER TABLE [dbo].[workout_exercise]
 ADD CONSTRAINT [PK_workout_exercise_id]
 PRIMARY KEY 
   CLUSTERED ([id] ASC)

GO


USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'exercise'  AND sc.name = N'dbo'  AND si.name = N'fk_Exercise_Type1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[exercise].[fk_Exercise_Type1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_Exercise_Type1_idx] ON [dbo].[exercise]
(
   [type_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'exercise'  AND sc.name = N'dbo'  AND si.name = N'fk_Exercise_User1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[exercise].[fk_Exercise_User1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_Exercise_User1_idx] ON [dbo].[exercise]
(
   [created_by_user_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_favorite_exercise'  AND sc.name = N'dbo'  AND si.name = N'fk_User_Favorite_Exercise_Exercise1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[user_favorite_exercise].[fk_User_Favorite_Exercise_Exercise1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_User_Favorite_Exercise_Exercise1_idx] ON [dbo].[user_favorite_exercise]
(
   [Exercise_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_favorite_exercise'  AND sc.name = N'dbo'  AND si.name = N'fk_User_Favorite_Exercise_User1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[user_favorite_exercise].[fk_User_Favorite_Exercise_User1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_User_Favorite_Exercise_User1_idx] ON [dbo].[user_favorite_exercise]
(
   [User_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_favorite_workout'  AND sc.name = N'dbo'  AND si.name = N'fk_User_Favorite_Workout_User1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[user_favorite_workout].[fk_User_Favorite_Workout_User1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_User_Favorite_Workout_User1_idx] ON [dbo].[user_favorite_workout]
(
   [user_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_favorite_workout'  AND sc.name = N'dbo'  AND si.name = N'fk_User_Favorite_Workout_Workout1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[user_favorite_workout].[fk_User_Favorite_Workout_Workout1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_User_Favorite_Workout_Workout1_idx] ON [dbo].[user_favorite_workout]
(
   [workout_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_workout'  AND sc.name = N'dbo'  AND si.name = N'fk_User_has_Workout_User1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[user_workout].[fk_User_has_Workout_User1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_User_has_Workout_User1_idx] ON [dbo].[user_workout]
(
   [user_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'user_workout'  AND sc.name = N'dbo'  AND si.name = N'fk_User_has_Workout_Workout1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[user_workout].[fk_User_has_Workout_Workout1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_User_has_Workout_Workout1_idx] ON [dbo].[user_workout]
(
   [workout_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'workout'  AND sc.name = N'dbo'  AND si.name = N'fk_Workout_Category1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[workout].[fk_Workout_Category1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_Workout_Category1_idx] ON [dbo].[workout]
(
   [category_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'workout_exercise'  AND sc.name = N'dbo'  AND si.name = N'fk_Workout_has_Exercise_Exercise1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[workout_exercise].[fk_Workout_has_Exercise_Exercise1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_Workout_has_Exercise_Exercise1_idx] ON [dbo].[workout_exercise]
(
   [exercise_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'workout_exercise'  AND sc.name = N'dbo'  AND si.name = N'fk_Workout_has_Exercise_Workout1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[workout_exercise].[fk_Workout_has_Exercise_Workout1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_Workout_has_Exercise_Workout1_idx] ON [dbo].[workout_exercise]
(
   [workout_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF  EXISTS (
       SELECT * FROM sys.objects  so JOIN sys.indexes si
       ON so.object_id = si.object_id
       JOIN sys.schemas sc
       ON so.schema_id = sc.schema_id
       WHERE so.name = N'workout'  AND sc.name = N'dbo'  AND si.name = N'fk_Workout_User1_idx' AND so.type in (N'U'))
   DROP INDEX [dbo].[workout].[fk_Workout_User1_idx] 
GO
CREATE NONCLUSTERED INDEX [fk_Workout_User1_idx] ON [dbo].[workout]
(
   [created_by_user_id] ASC
)
WITH (SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY] 
GO
GO

USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'exercise$fk_Exercise_Type1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_Type1]
 GO



ALTER TABLE [dbo].[exercise]
 ADD CONSTRAINT [exercise$fk_Exercise_Type1]
 FOREIGN KEY 
   ([type_id])
 REFERENCES 
   [gofitdb].[dbo].[type]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'exercise$fk_Exercise_User1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[exercise] DROP CONSTRAINT [exercise$fk_Exercise_User1]
 GO



ALTER TABLE [dbo].[exercise]
 ADD CONSTRAINT [exercise$fk_Exercise_User1]
 FOREIGN KEY 
   ([created_by_user_id])
 REFERENCES 
   [gofitdb].[dbo].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_favorite_exercise$fk_User_Favorite_Exercise_Exercise1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[user_favorite_exercise] DROP CONSTRAINT [user_favorite_exercise$fk_User_Favorite_Exercise_Exercise1]
 GO



ALTER TABLE [dbo].[user_favorite_exercise]
 ADD CONSTRAINT [user_favorite_exercise$fk_User_Favorite_Exercise_Exercise1]
 FOREIGN KEY 
   ([Exercise_id])
 REFERENCES 
   [gofitdb].[dbo].[exercise]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_favorite_exercise$fk_User_Favorite_Exercise_User1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[user_favorite_exercise] DROP CONSTRAINT [user_favorite_exercise$fk_User_Favorite_Exercise_User1]
 GO



ALTER TABLE [dbo].[user_favorite_exercise]
 ADD CONSTRAINT [user_favorite_exercise$fk_User_Favorite_Exercise_User1]
 FOREIGN KEY 
   ([User_id])
 REFERENCES 
   [gofitdb].[dbo].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_favorite_workout$fk_User_Favorite_Workout_User1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
 GO



ALTER TABLE [dbo].[user_favorite_workout]
 ADD CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_User1]
 FOREIGN KEY 
   ([user_id])
 REFERENCES 
   [gofitdb].[dbo].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_favorite_workout$fk_User_Favorite_Workout_Workout1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[user_favorite_workout] DROP CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
 GO



ALTER TABLE [dbo].[user_favorite_workout]
 ADD CONSTRAINT [user_favorite_workout$fk_User_Favorite_Workout_Workout1]
 FOREIGN KEY 
   ([workout_id])
 REFERENCES 
   [gofitdb].[dbo].[workout]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_workout$fk_User_has_Workout_User1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_User1]
 GO



ALTER TABLE [dbo].[user_workout]
 ADD CONSTRAINT [user_workout$fk_User_has_Workout_User1]
 FOREIGN KEY 
   ([user_id])
 REFERENCES 
   [gofitdb].[dbo].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'user_workout$fk_User_has_Workout_Workout1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[user_workout] DROP CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
 GO



ALTER TABLE [dbo].[user_workout]
 ADD CONSTRAINT [user_workout$fk_User_has_Workout_Workout1]
 FOREIGN KEY 
   ([workout_id])
 REFERENCES 
   [gofitdb].[dbo].[workout]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'workout$fk_Workout_Category1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_Category1]
 GO



ALTER TABLE [dbo].[workout]
 ADD CONSTRAINT [workout$fk_Workout_Category1]
 FOREIGN KEY 
   ([category_id])
 REFERENCES 
   [gofitdb].[dbo].[category]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'workout$fk_Workout_User1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[workout] DROP CONSTRAINT [workout$fk_Workout_User1]
 GO



ALTER TABLE [dbo].[workout]
 ADD CONSTRAINT [workout$fk_Workout_User1]
 FOREIGN KEY 
   ([created_by_user_id])
 REFERENCES 
   [gofitdb].[dbo].[user]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE gofitdb
GO
IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'workout_exercise$fk_Workout_has_Exercise_Exercise1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
 GO



ALTER TABLE [dbo].[workout_exercise]
 ADD CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Exercise1]
 FOREIGN KEY 
   ([exercise_id])
 REFERENCES 
   [gofitdb].[dbo].[exercise]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO

IF EXISTS (SELECT * FROM sys.objects so JOIN sys.schemas sc ON so.schema_id = sc.schema_id WHERE so.name = N'workout_exercise$fk_Workout_has_Exercise_Workout1'  AND sc.name=N'dbo'  AND type in (N'F'))
ALTER TABLE [dbo].[workout_exercise] DROP CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
 GO



ALTER TABLE [dbo].[workout_exercise]
 ADD CONSTRAINT [workout_exercise$fk_Workout_has_Exercise_Workout1]
 FOREIGN KEY 
   ([workout_id])
 REFERENCES 
   [gofitdb].[dbo].[workout]     ([id])
    ON DELETE NO ACTION
    ON UPDATE NO ACTION

GO


USE gofitdb
GO
ALTER TABLE  [dbo].[category]
 ADD DEFAULT getdate() FOR [timestamp]
GO


USE gofitdb
GO
ALTER TABLE  [dbo].[exercise]
 ADD DEFAULT NULL FOR [link]
GO

ALTER TABLE  [dbo].[exercise]
 ADD DEFAULT getdate() FOR [timestamp]
GO


USE gofitdb
GO
ALTER TABLE  [dbo].[type]
 ADD DEFAULT getdate() FOR [timestamp]
GO


USE gofitdb
GO
ALTER TABLE  [dbo].[user]
 ADD DEFAULT NULL FOR [fname]
GO

ALTER TABLE  [dbo].[user]
 ADD DEFAULT NULL FOR [lname]
GO

ALTER TABLE  [dbo].[user]
 ADD DEFAULT NULL FOR [is_male]
GO

ALTER TABLE  [dbo].[user]
 ADD DEFAULT 0 FOR [is_admin]
GO

ALTER TABLE  [dbo].[user]
 ADD DEFAULT NULL FOR [weight]
GO

ALTER TABLE  [dbo].[user]
 ADD DEFAULT NULL FOR [height]
GO

ALTER TABLE  [dbo].[user]
 ADD DEFAULT getdate() FOR [timestamp]
GO


USE gofitdb
GO
ALTER TABLE  [dbo].[user_workout]
 ADD DEFAULT NULL FOR [date_started]
GO

ALTER TABLE  [dbo].[user_workout]
 ADD DEFAULT NULL FOR [date_finished]
GO

ALTER TABLE  [dbo].[user_workout]
 ADD DEFAULT getdate() FOR [timestamp]
GO


USE gofitdb
GO
ALTER TABLE  [dbo].[workout]
 ADD DEFAULT getdate() FOR [timestamp]
GO


USE gofitdb
GO
ALTER TABLE  [dbo].[workout_exercise]
 ADD DEFAULT getdate() FOR [timestamp]
GO

/** ADD DATA **/

use gofitdb;

INSERT INTO type([name],[measure]) VALUES('distance', 'miles');
INSERT INTO type([name],[measure]) VALUES('quantity', 'unit');
INSERT INTO type([name],[measure]) VALUES('duration', 'seconds');
INSERT INTO type([name],[measure]) VALUES('duration', 'minutes');
INSERT INTO type([name],[measure]) VALUES('duration', 'hours');

INSERT INTO category([name], [description]) VALUES('endurance', 'Endurance workouts help keep your heart, lungs, and circulatory system healthy.');
INSERT INTO category([name], [description]) VALUES('strength', 'Strength workouts build muscle size and power');
INSERT INTO category([name], [description]) VALUES('flexibility', 'Flexibility workouts stretch your muscles and help protect you body from exercise incurred injuries');

INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin', 'admin', 'Bob', 'Jones', 1, 1);
INSERT INTO [user]([username],[password],[fname],[lname],[is_male],[is_admin]) VALUES('admin2', 'admin2', 'Jane', 'Forsythe', 0, 1);

INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('hunts', 'hunts', 'Sharon', 'Hunt', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('SharonArmstrong', 'SharonArmstrong', 'Sharon', 'Armstrong', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('dunnj', 'dunnj', 'Joseph', 'Dunn', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('AnneBailey', 'AnneBailey', 'Anne', 'Bailey', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('FrankSmith', 'FrankSmith', 'Frank', 'Smith', 1, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('DorothyMoreno', 'DorothyMoreno', 'Dorothy', 'Moreno', 0, 0);
INSERT INTO [user] (username, password, fname, lname, is_male, is_admin) values ('CharlesDavis', 'CharlesDavis', 'Charles', 'Davis', 1, 0);

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



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
   [password] nvarchar(45)  NOT NULL,
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
   [id] int  NOT NULL,
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
   [id] int  NOT NULL,
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


USE [bbomDb2];


GO

IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Выполняется удаление [dbo].[FK_UsersTemplateSettings_AspNetUsers]...';


GO
ALTER TABLE [dbo].[UsersTemplateSettings] DROP CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers];


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Выполняется удаление [dbo].[FK_UsersTemplateSettings_Templates]...';


GO
ALTER TABLE [dbo].[UsersTemplateSettings] DROP CONSTRAINT [FK_UsersTemplateSettings_Templates];


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
/*
Тип столбца Id в таблице [dbo].[UsersTemplateSettings] на данный момент -  NCHAR (10) NOT NULL, но будет изменен на  INT IDENTITY (0, 1) NOT NULL. Данные могут быть утеряны.
*/
GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[UsersTemplateSettings]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_UsersTemplateSettings] (
    [UserId]     NVARCHAR (128) NOT NULL,
    [TemplateId] INT            NOT NULL,
    [VideoLink]  NVARCHAR (MAX) NOT NULL,
    [Id]         INT            IDENTITY (0, 1) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_UsersTemplateSettings1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[UsersTemplateSettings])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UsersTemplateSettings] ON;
        INSERT INTO [dbo].[tmp_ms_xx_UsersTemplateSettings] ([Id], [UserId], [TemplateId], [VideoLink])
        SELECT   [Id],
                 [UserId],
                 [TemplateId],
                 [VideoLink]
        FROM     [dbo].[UsersTemplateSettings]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_UsersTemplateSettings] OFF;
    END

DROP TABLE [dbo].[UsersTemplateSettings];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_UsersTemplateSettings]', N'UsersTemplateSettings';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_UsersTemplateSettings1]', N'PK_UsersTemplateSettings', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Выполняется создание [dbo].[FK_UsersTemplateSettings_AspNetUsers]...';


GO
ALTER TABLE [dbo].[UsersTemplateSettings] WITH NOCHECK
    ADD CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Выполняется создание [dbo].[FK_UsersTemplateSettings_Templates]...';


GO
ALTER TABLE [dbo].[UsersTemplateSettings] WITH NOCHECK
    ADD CONSTRAINT [FK_UsersTemplateSettings_Templates] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Templates] ([Id]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'Транзакции обновления базы данных успешно завершены.'
COMMIT TRANSACTION
END
ELSE PRINT N'Сбой транзакций обновления базы данных.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Существующие данные проверяются относительно вновь созданных ограничений';


GO
USE [bbomDb2];


GO
ALTER TABLE [dbo].[UsersTemplateSettings] WITH CHECK CHECK CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers];

ALTER TABLE [dbo].[UsersTemplateSettings] WITH CHECK CHECK CONSTRAINT [FK_UsersTemplateSettings_Templates];


GO
PRINT N'Обновление завершено.';


GO

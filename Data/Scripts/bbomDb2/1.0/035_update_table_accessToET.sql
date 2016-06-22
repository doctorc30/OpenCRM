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
PRINT N'Выполняется удаление [dbo].[FK_AccessToEventTypes_AspNetRoles]...';


GO
ALTER TABLE [dbo].[AccessToEventTypes] DROP CONSTRAINT [FK_AccessToEventTypes_AspNetRoles];


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
PRINT N'Выполняется удаление [dbo].[FK_AccessToEventTypes_EventTypes]...';


GO
ALTER TABLE [dbo].[AccessToEventTypes] DROP CONSTRAINT [FK_AccessToEventTypes_EventTypes];


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
Удаляется столбец [dbo].[AccessToEventTypes].[Id], возможна потеря данных.
*/
GO
PRINT N'Выполняется запуск перестройки таблицы [dbo].[AccessToEventTypes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AccessToEventTypes] (
    [EventTypeId] INT            NOT NULL,
    [RoleId]      NVARCHAR (128) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_AccessToMenu21] PRIMARY KEY CLUSTERED ([RoleId] ASC, [EventTypeId] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AccessToEventTypes])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_AccessToEventTypes] ([RoleId], [EventTypeId])
        SELECT   [RoleId],
                 [EventTypeId]
        FROM     [dbo].[AccessToEventTypes]
        ORDER BY [RoleId] ASC, [EventTypeId] ASC;
    END

DROP TABLE [dbo].[AccessToEventTypes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AccessToEventTypes]', N'AccessToEventTypes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_AccessToMenu21]', N'PK_AccessToMenu2', N'OBJECT';

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
PRINT N'Выполняется создание [dbo].[FK_AccessToEventTypes_AspNetRoles]...';


GO
ALTER TABLE [dbo].[AccessToEventTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_AccessToEventTypes_AspNetRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]);


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
PRINT N'Выполняется создание [dbo].[FK_AccessToEventTypes_EventTypes]...';


GO
ALTER TABLE [dbo].[AccessToEventTypes] WITH NOCHECK
    ADD CONSTRAINT [FK_AccessToEventTypes_EventTypes] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventTypes] ([Id]);


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
ALTER TABLE [dbo].[AccessToEventTypes] WITH CHECK CHECK CONSTRAINT [FK_AccessToEventTypes_AspNetRoles];

ALTER TABLE [dbo].[AccessToEventTypes] WITH CHECK CHECK CONSTRAINT [FK_AccessToEventTypes_EventTypes];


GO
PRINT N'Обновление завершено.';


GO

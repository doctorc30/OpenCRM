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
PRINT N'Выполняется удаление [dbo].[FK_Events_EventTypes]...';


GO
ALTER TABLE [dbo].[Events] DROP CONSTRAINT [FK_Events_EventTypes];


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
PRINT N'Выполняется запуск перестройки таблицы [dbo].[EventTypes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_EventTypes] (
    [Id]   INT            IDENTITY (0, 1) NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_EventTypes1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[EventTypes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_EventTypes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_EventTypes] ([Id], [Name])
        SELECT   [Id],
                 [Name]
        FROM     [dbo].[EventTypes]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_EventTypes] OFF;
    END

DROP TABLE [dbo].[EventTypes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_EventTypes]', N'EventTypes';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_EventTypes1]', N'PK_EventTypes', N'OBJECT';

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
PRINT N'Выполняется создание [dbo].[FK_Events_EventTypes]...';


GO
ALTER TABLE [dbo].[Events] WITH NOCHECK
    ADD CONSTRAINT [FK_Events_EventTypes] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[EventTypes] ([Id]);


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
ALTER TABLE [dbo].[Events] WITH CHECK CHECK CONSTRAINT [FK_Events_EventTypes];


GO
PRINT N'Обновление завершено.';


GO

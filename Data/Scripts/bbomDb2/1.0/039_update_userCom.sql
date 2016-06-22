
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
PRINT N'Выполняется удаление [dbo].[FK_UserCommunications_AspNetUsers]...';


GO
ALTER TABLE [dbo].[UserCommunications] DROP CONSTRAINT [FK_UserCommunications_AspNetUsers];


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
PRINT N'Выполняется удаление [dbo].[FK_UserCommunications_Communicatios]...';


GO
ALTER TABLE [dbo].[UserCommunications] DROP CONSTRAINT [FK_UserCommunications_Communicatios];


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
PRINT N'Выполняется создание [dbo].[FK_UserCommunications_AspNetUsers]...';


GO
ALTER TABLE [dbo].[UserCommunications] WITH NOCHECK
    ADD CONSTRAINT [FK_UserCommunications_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


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
PRINT N'Выполняется создание [dbo].[FK_UserCommunications_Communicatios]...';


GO
ALTER TABLE [dbo].[UserCommunications] WITH NOCHECK
    ADD CONSTRAINT [FK_UserCommunications_Communicatios] FOREIGN KEY ([CommunicationId]) REFERENCES [dbo].[Communicatios] ([Id]) ON DELETE CASCADE;


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
ALTER TABLE [dbo].[UserCommunications] WITH CHECK CHECK CONSTRAINT [FK_UserCommunications_AspNetUsers];

ALTER TABLE [dbo].[UserCommunications] WITH CHECK CHECK CONSTRAINT [FK_UserCommunications_Communicatios];


GO
PRINT N'Обновление завершено.';


GO

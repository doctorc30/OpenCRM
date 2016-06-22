
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
PRINT N'Выполняется удаление [dbo].[FK_UserExtraRegParams_AspNetUsers]...';


GO
ALTER TABLE [dbo].[UserExtraRegParams] DROP CONSTRAINT [FK_UserExtraRegParams_AspNetUsers];


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
PRINT N'Выполняется удаление [dbo].[FK_UserExtraRegParams_ExtraRegParams]...';


GO
ALTER TABLE [dbo].[UserExtraRegParams] DROP CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams];


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
PRINT N'Выполняется создание [dbo].[FK_UserExtraRegParams_AspNetUsers]...';


GO
ALTER TABLE [dbo].[UserExtraRegParams] WITH NOCHECK
    ADD CONSTRAINT [FK_UserExtraRegParams_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE;


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
PRINT N'Выполняется создание [dbo].[FK_UserExtraRegParams_ExtraRegParams]...';


GO
ALTER TABLE [dbo].[UserExtraRegParams] WITH NOCHECK
    ADD CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams] FOREIGN KEY ([ExtraRegParamId]) REFERENCES [dbo].[ExtraRegParams] ([Id]) ON DELETE CASCADE;


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
ALTER TABLE [dbo].[UserExtraRegParams] WITH CHECK CHECK CONSTRAINT [FK_UserExtraRegParams_AspNetUsers];

ALTER TABLE [dbo].[UserExtraRegParams] WITH CHECK CHECK CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams];


GO
PRINT N'Обновление завершено.';


GO

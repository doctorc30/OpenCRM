USE [bbomDb];


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
PRINT N'??????????? ?????? ??????????? ??????? [dbo].[Events]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Events] (
    [Id]        INT           IDENTITY (0, 1) NOT NULL,
    [Title]     VARCHAR (128) NULL,
    [StartDate] DATE          NULL,
    [EndDate]   DATE          NOT NULL,
    [Url]       VARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Events])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Events] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Events] ([Id], [Title], [StartDate], [EndDate], [Url])
        SELECT   [Id],
                 [Title],
                 [StartDate],
                 [EndDate],
                 [Url]
        FROM     [dbo].[Events]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Events] OFF;
    END

DROP TABLE [dbo].[Events];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Events]', N'Events';

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

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'?????????? ?????????? ???? ?????? ??????? ?????????.'
COMMIT TRANSACTION
END
ELSE PRINT N'???? ?????????? ?????????? ???? ??????.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'?????????? ?????????.';


GO

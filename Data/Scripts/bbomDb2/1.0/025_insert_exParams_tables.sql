use bbomDb2

CREATE TABLE [dbo].[ExtraRegParams] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ExtraRegParams] PRIMARY KEY CLUSTERED ([Id] ASC)
);

INSERT INTO bbomDb2.dbo.ExtraRegParams (Name) VALUES ('Реферальная ссылка в "левую ногу"');
INSERT INTO bbomDb2.dbo.ExtraRegParams (Name) VALUES ('Реферальная ссылка в "правую ногу"');
INSERT INTO bbomDb2.dbo.ExtraRegParams (Name) VALUES ('Для 4 пакета');

CREATE TABLE [dbo].[ReceivedExtraRegParams] (
    [UserId]          NVARCHAR (128) NOT NULL,
    [ExtraRegParamId] INT            NOT NULL,
    CONSTRAINT [PK_ReceivedExtraRegParams] PRIMARY KEY CLUSTERED ([UserId] ASC, [ExtraRegParamId] ASC),
    CONSTRAINT [FK_ReceivedExtraRegParams_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_ReceivedExtraRegParams_ExtraRegParams] FOREIGN KEY ([ExtraRegParamId]) REFERENCES [dbo].[ExtraRegParams] ([Id])
);

CREATE TABLE [dbo].[UserExtraRegParams] (
    [UserId]          NVARCHAR (128) NOT NULL,
    [ExtraRegParamId] INT            NOT NULL,
    [Value]           NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserExtraRegParams] PRIMARY KEY CLUSTERED ([UserId] ASC, [ExtraRegParamId] ASC),
    CONSTRAINT [FK_UserExtraRegParams_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams] FOREIGN KEY ([ExtraRegParamId]) REFERENCES [dbo].[ExtraRegParams] ([Id])
);
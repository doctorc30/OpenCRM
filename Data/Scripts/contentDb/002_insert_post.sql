CREATE TABLE [dbo].[PostTypes] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_PostTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Posts] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [UserId] NVARCHAR (128) NOT NULL,
    [Date]   DATETIME       NOT NULL,
    [Text]   NVARCHAR (MAX) NOT NULL,
    [Title]  NVARCHAR (500) NOT NULL,
    [TypeId] INT            NOT NULL,
    CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Posts_PostTypes] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[PostTypes] ([Id])
);

SET IDENTITY_INSERT [dbo].[PostTypes] ON
INSERT INTO [dbo].[PostTypes] ([Id], [Name]) VALUES (1, N'Информация')
INSERT INTO [dbo].[PostTypes] ([Id], [Name]) VALUES (2, N'Новость')
SET IDENTITY_INSERT [dbo].[PostTypes] OFF


use bbomDb2
CREATE TABLE [dbo].[Communicatios] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_Communicatios] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[UserCommunications] (
    [CommunicationId] INT            NOT NULL,
    [UserId]          NVARCHAR (128) NOT NULL,
    [Value]           NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_UserCommunications] PRIMARY KEY CLUSTERED ([CommunicationId] ASC, [UserId] ASC),
    CONSTRAINT [FK_UserCommunications_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UserCommunications_Communicatios] FOREIGN KEY ([CommunicationId]) REFERENCES [dbo].[Communicatios] ([Id])
);

SET IDENTITY_INSERT [dbo].[Communicatios] ON
INSERT INTO [dbo].[Communicatios] ([Id], [Name]) VALUES (1, N'Skype')
INSERT INTO [dbo].[Communicatios] ([Id], [Name]) VALUES (2, N'Второй телефон')
INSERT INTO [dbo].[Communicatios] ([Id], [Name]) VALUES (3, N'Fasebooke')
INSERT INTO [dbo].[Communicatios] ([Id], [Name]) VALUES (4, N'VK')
SET IDENTITY_INSERT [dbo].[Communicatios] OFF
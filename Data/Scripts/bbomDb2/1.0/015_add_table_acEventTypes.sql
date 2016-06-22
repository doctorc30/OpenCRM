use bbomDb2
CREATE TABLE [dbo].[AccessToEventTypes] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [EventTypeId] INT            NOT NULL,
    [RoleId]      NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_AccessToEventTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccessToEventTypes_AspNetRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [FK_AccessToEventTypes_EventTypes] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventTypes] ([Id])
);

INSERT INTO [dbo].[AccessToEventTypes] ([EventTypeId], [RoleId]) VALUES (0, N'7138ef18-c696-450f-aac4-06a692e5f75c')
INSERT INTO [dbo].[AccessToEventTypes] ([EventTypeId], [RoleId]) VALUES (1, N'7138ef18-c696-450f-aac4-06a692e5f75c')
INSERT INTO [dbo].[AccessToEventTypes] ([EventTypeId], [RoleId]) VALUES (2, N'7138ef18-c696-450f-aac4-06a692e5f75c')
INSERT INTO [dbo].[AccessToEventTypes] ([EventTypeId], [RoleId]) VALUES (0, N'dd836c76-bcba-4700-bdd8-33d7dc608c8a')
INSERT INTO [dbo].[AccessToEventTypes] ([EventTypeId], [RoleId]) VALUES (0, N'87f990cc-cbf5-4db2-8d5d-a8ad93782b0b')
INSERT INTO [dbo].[AccessToEventTypes] ([EventTypeId], [RoleId]) VALUES (1, N'87f990cc-cbf5-4db2-8d5d-a8ad93782b0b')
INSERT INTO [dbo].[AccessToEventTypes] ([EventTypeId], [RoleId]) VALUES (2, N'87f990cc-cbf5-4db2-8d5d-a8ad93782b0b')

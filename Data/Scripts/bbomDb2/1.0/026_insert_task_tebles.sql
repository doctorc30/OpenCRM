use bbomDb2
CREATE TABLE [dbo].[Tasks] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[UserComplitedTasks] (
    [UserId] NVARCHAR (128) NOT NULL,
    [TaskId] INT            NOT NULL,
    CONSTRAINT [PK_UserComplitedTasks] PRIMARY KEY CLUSTERED ([UserId] ASC, [TaskId] ASC),
    CONSTRAINT [FK_UserComplitedTasks_Tasks] FOREIGN KEY ([TaskId]) REFERENCES [dbo].[Tasks] ([Id]),
    CONSTRAINT [FK_UserComplitedTasks_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
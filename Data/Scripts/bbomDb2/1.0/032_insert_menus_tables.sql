use bbomDb2
CREATE TABLE [dbo].[Menus] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Discription] NVARCHAR (200) NULL,
    [Icon]        NVARCHAR (MAX) NULL,
    [Action]      NVARCHAR (50)  NOT NULL,
    [Controller]  NVARCHAR (50)  NOT NULL,
    [Enable]      INT            NULL,
    [ParentId]    INT            NULL,
    [OrderNum]    INT            NOT NULL,
    CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Menus_Menus] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Menus] ([Id])
);

CREATE TABLE [dbo].[AccessToMenu] (
    [RoleId] NVARCHAR (128) NOT NULL,
    [MenuId] INT            NOT NULL,
    CONSTRAINT [PK_AccessToMenu] PRIMARY KEY CLUSTERED ([RoleId] ASC, [MenuId] ASC),
    CONSTRAINT [FK_AccessToMenu_AspNetRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [FK_AccessToMenu_Menus] FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menus] ([Id])
);


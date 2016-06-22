use bbomDb2
CREATE TABLE [dbo].[DiscountType] (
    [Id]   INT           IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_DiscountType] PRIMARY KEY CLUSTERED ([Id] ASC)
);
SET IDENTITY_INSERT [dbo].[DiscountType] ON
INSERT INTO [dbo].[DiscountType] ([Id], [Name]) VALUES (1, N'Полная скидка')
SET IDENTITY_INSERT [dbo].[DiscountType] OFF

CREATE TABLE [dbo].[Discounts] (
    [Id]                        INT             IDENTITY (1, 1) NOT NULL,
    [Name]                      NVARCHAR (200)  NOT NULL,
    [DiscountTypeId]            INT             NOT NULL,
    [DiscountAmount]            DECIMAL (18, 4) NOT NULL,
    [StartDate]                 DATETIME        NULL,
    [EndDate]                   DATETIME        NULL,
    [DiscountLimitationId]      INT             NULL,
    [LimitationTimes]           INT             NULL,
    [MaximumDiscountedQuantity] INT             NULL,
    [Description]               NVARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Discounts_DiscountType] FOREIGN KEY ([Id]) REFERENCES [dbo].[DiscountType] ([Id])
);
SET IDENTITY_INSERT [dbo].[Discounts] ON
INSERT INTO [dbo].[Discounts] ([Id], [Name], [DiscountTypeId], [DiscountAmount], [StartDate], [EndDate], [DiscountLimitationId], [LimitationTimes], [MaximumDiscountedQuantity], [Description]) VALUES (1, N'Скидка новым пользователям', 1, CAST(100.0000 AS Decimal(18, 4)), NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Discounts] OFF


CREATE TABLE [dbo].[PaymentPlans] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX)  NULL,
    [Amount]      DECIMAL (18, 4) NOT NULL,
    [Name]        NVARCHAR (200)  NOT NULL,
    [WorkAmount]  INT             NULL,
    CONSTRAINT [PK_PaymentPlans] PRIMARY KEY CLUSTERED ([Id] ASC)
);

SET IDENTITY_INSERT [dbo].[PaymentPlans] ON
INSERT INTO [dbo].[PaymentPlans] ([Id], [Description], [Amount], [Name], [WorkAmount]) VALUES (1, NULL, CAST(8.0000 AS Decimal(18, 4)), N'На 1 месяц', 1)
INSERT INTO [dbo].[PaymentPlans] ([Id], [Description], [Amount], [Name], [WorkAmount]) VALUES (2, NULL, CAST(15.0000 AS Decimal(18, 4)), N'На 3 месяца', 3)
SET IDENTITY_INSERT [dbo].[PaymentPlans] OFF

CREATE TABLE [dbo].[Payments] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [UserId]        NVARCHAR (128)  NOT NULL,
    [Date]          DATETIME        NOT NULL,
    [Status]        INT             NOT NULL,
    [Amount]        DECIMAL (18, 4) NOT NULL,
    [PaymentPlanId] INT             NULL,
    [EndDate]       DATETIME        NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Payments_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_Payments_PaymentPlans] FOREIGN KEY ([PaymentPlanId]) REFERENCES [dbo].[PaymentPlans] ([Id])
);


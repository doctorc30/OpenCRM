use bbomDb2
CREATE TABLE [dbo].[AccessToDiscountType] (
    [RoleId]         NVARCHAR (128) NOT NULL,
    [DiscountTypeId] INT            NOT NULL,
    CONSTRAINT [PK_AccessToDiscountType] PRIMARY KEY CLUSTERED ([RoleId] ASC, [DiscountTypeId] ASC),
    CONSTRAINT [FK_AccessToDiscountType_AspNetRoles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]),
    CONSTRAINT [FK_AccessToDiscountType_DiscountType] FOREIGN KEY ([DiscountTypeId]) REFERENCES [dbo].[DiscountType] ([Id])
);

CREATE TABLE [dbo].[DiscountTypePaymentsPlans] (
    [DiscountTypeId] INT NOT NULL,
    [PaymentPlanId]  INT NOT NULL,
    CONSTRAINT [PK_DiscountTypePaymentsPlans] PRIMARY KEY CLUSTERED ([DiscountTypeId] ASC, [PaymentPlanId] ASC),
    CONSTRAINT [FK_DiscountTypePaymentsPlans_DiscountType] FOREIGN KEY ([DiscountTypeId]) REFERENCES [dbo].[DiscountType] ([Id]),
    CONSTRAINT [FK_DiscountTypePaymentsPlans_PaymentPlans] FOREIGN KEY ([PaymentPlanId]) REFERENCES [dbo].[PaymentPlans] ([Id])
);

CREATE TABLE [dbo].[UserDiscounts] (
    [UserId]     NVARCHAR (128) NOT NULL,
    [DiscountId] INT            NOT NULL,
    CONSTRAINT [PK_UserDiscounts] PRIMARY KEY CLUSTERED ([UserId] ASC, [DiscountId] ASC),
    CONSTRAINT [FK_UserDiscounts_Discounts] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id]),
    CONSTRAINT [FK_UserDiscounts_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

ALTER TABLE bbomDb2.dbo.DiscountType ADD [Status] INT NULL;

ALTER TABLE bbomDb2.dbo.PaymentPlans ADD [Status] INT NULL;

DELETE FROM dbo.DiscountType
SET IDENTITY_INSERT [dbo].[DiscountType] ON
INSERT INTO [dbo].[DiscountType] ([Id], [Name], [Status]) VALUES (1, N'Полная скидка', NULL)
INSERT INTO [dbo].[DiscountType] ([Id], [Name], [Status]) VALUES (2, N'Скидка администратора', 1)
INSERT INTO [dbo].[DiscountType] ([Id], [Name], [Status]) VALUES (3, N'Скидка администратора на 1 месяц', 1)
INSERT INTO [dbo].[DiscountType] ([Id], [Name], [Status]) VALUES (4, N'Скидка администратора на 3 месяц', 1)
SET IDENTITY_INSERT [dbo].[DiscountType] OFF

INSERT INTO [dbo].[AccessToDiscountType] ([RoleId], [DiscountTypeId]) VALUES (N'7138ef18-c696-450f-aac4-06a692e5f75c', 2)
INSERT INTO [dbo].[AccessToDiscountType] ([RoleId], [DiscountTypeId]) VALUES (N'7138ef18-c696-450f-aac4-06a692e5f75c', 3)
INSERT INTO [dbo].[AccessToDiscountType] ([RoleId], [DiscountTypeId]) VALUES (N'7138ef18-c696-450f-aac4-06a692e5f75c', 4)
INSERT INTO [dbo].[AccessToDiscountType] ([RoleId], [DiscountTypeId]) VALUES (N'dd836c76-bcba-4700-bdd8-33d7dc608c8a', 1)


DELETE from dbo.Discounts

SET IDENTITY_INSERT [dbo].[Discounts] ON
INSERT INTO [dbo].[Discounts] ([Id], [Name], [DiscountTypeId], [DiscountAmount], [StartDate], [EndDate], [DiscountLimitationId], [LimitationTimes], [MaximumDiscountedQuantity], [Description]) VALUES (1, N'Скидка новым пользователям', 1, CAST(50.0000 AS Decimal(18, 4)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Discounts] ([Id], [Name], [DiscountTypeId], [DiscountAmount], [StartDate], [EndDate], [DiscountLimitationId], [LimitationTimes], [MaximumDiscountedQuantity], [Description]) VALUES (2, N'Скидка от администратора', 2, CAST(100.0000 AS Decimal(18, 4)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Discounts] ([Id], [Name], [DiscountTypeId], [DiscountAmount], [StartDate], [EndDate], [DiscountLimitationId], [LimitationTimes], [MaximumDiscountedQuantity], [Description]) VALUES (3, N'Скидка от администратора на 1 месяц', 3, CAST(100.0000 AS Decimal(18, 4)), NULL, NULL, NULL, NULL, NULL, NULL)
INSERT INTO [dbo].[Discounts] ([Id], [Name], [DiscountTypeId], [DiscountAmount], [StartDate], [EndDate], [DiscountLimitationId], [LimitationTimes], [MaximumDiscountedQuantity], [Description]) VALUES (4, N'Скидка от администратора на 3 месяца', 4, CAST(100.0000 AS Decimal(18, 4)), NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Discounts] OFF


INSERT INTO [dbo].[DiscountTypePaymentsPlans] ([DiscountTypeId], [PaymentPlanId]) VALUES (3, 1)
INSERT INTO [dbo].[DiscountTypePaymentsPlans] ([DiscountTypeId], [PaymentPlanId]) VALUES (4, 2)

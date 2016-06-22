USE bbomDb2
CREATE TABLE [dbo].[UserInvitedDiscounts] (
  [UserId]     NVARCHAR(128) NOT NULL,
  [DiscountId] INT           NOT NULL,
  [Amount]     INT           NOT NULL,
  CONSTRAINT [PK_UserInvitedDiscounts] PRIMARY KEY CLUSTERED ([UserId] ASC, [DiscountId] ASC),
  CONSTRAINT [FK_UserInvitedDiscounts_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
  CONSTRAINT [FK_UserInvitedDiscounts_Discounts] FOREIGN KEY ([DiscountId]) REFERENCES [dbo].[Discounts] ([Id])
);

INSERT INTO bbomDb2.dbo.DiscountType (Name, Status) VALUES ('Скидка за приглашения', NULL);
INSERT INTO bbomDb2.dbo.Discounts (Name, DiscountTypeId, DiscountAmount, StartDate, EndDate, DiscountLimitationId, LimitationTimes, MaximumDiscountedQuantity, Description)
VALUES ('Скидка за приглащения', 5, 100.0000, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO bbomDb2.dbo.DiscountTypePaymentsPlans (DiscountTypeId, PaymentPlanId) VALUES (5, 1);
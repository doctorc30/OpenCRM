USE [master]
GO
/****** Object:  Database [identityDb]    Script Date: 25.04.2016 18:40:31 ******/
CREATE DATABASE [identityDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'identityDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\identityDb.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'identityDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\identityDb_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [identityDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [identityDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [identityDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [identityDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [identityDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [identityDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [identityDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [identityDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [identityDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [identityDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [identityDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [identityDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [identityDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [identityDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [identityDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [identityDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [identityDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [identityDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [identityDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [identityDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [identityDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [identityDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [identityDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [identityDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [identityDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [identityDb] SET  MULTI_USER 
GO
ALTER DATABASE [identityDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [identityDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [identityDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [identityDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [identityDb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [identityDb]
GO
/****** Object:  Table [dbo].[AccessToDiscountType]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessToDiscountType](
	[RoleId] [nvarchar](128) NOT NULL,
	[DiscountTypeId] [int] NOT NULL,
 CONSTRAINT [PK_AccessToDiscountType] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[DiscountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[parent_id] [nvarchar](128) NULL,
	[Foot] [int] NULL,
	[InvitedUser] [nvarchar](128) NULL,
	[DateRegistration] [datetime] NULL,
	[Name] [nvarchar](128) NULL,
	[Suname] [nvarchar](128) NULL,
	[Altname] [nvarchar](128) NULL,
	[Image] [image] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Communicatios]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Communicatios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Communicatios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[DiscountTypeId] [int] NOT NULL,
	[DiscountAmount] [decimal](18, 4) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[DiscountLimitationId] [int] NULL,
	[LimitationTimes] [int] NULL,
	[MaximumDiscountedQuantity] [int] NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiscountType]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_DiscountType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DiscountTypePaymentsPlans]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiscountTypePaymentsPlans](
	[DiscountTypeId] [int] NOT NULL,
	[PaymentPlanId] [int] NOT NULL,
 CONSTRAINT [PK_DiscountTypePaymentsPlans] PRIMARY KEY CLUSTERED 
(
	[DiscountTypeId] ASC,
	[PaymentPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentPlans]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentPlans](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[WorkAmount] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_PaymentPlans] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payments]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[PaymentPlanId] [int] NULL,
	[EndDate] [datetime] NULL,
	[DiscountId] [int] NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserCommunications]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCommunications](
	[CommunicationId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_UserCommunications] PRIMARY KEY CLUSTERED 
(
	[CommunicationId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserDiscounts]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDiscounts](
	[UserId] [nvarchar](128) NOT NULL,
	[DiscountId] [int] NOT NULL,
 CONSTRAINT [PK_UserDiscounts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DiscountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInvitedDiscounts]    Script Date: 25.04.2016 18:40:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInvitedDiscounts](
	[UserId] [nvarchar](128) NOT NULL,
	[DiscountId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
 CONSTRAINT [PK_UserInvitedDiscounts] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[DiscountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 25.04.2016 18:40:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 25.04.2016 18:40:31 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 25.04.2016 18:40:31 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 25.04.2016 18:40:31 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 25.04.2016 18:40:31 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 25.04.2016 18:40:31 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccessToDiscountType]  WITH CHECK ADD  CONSTRAINT [FK_AccessToDiscountType_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AccessToDiscountType] CHECK CONSTRAINT [FK_AccessToDiscountType_AspNetRoles]
GO
ALTER TABLE [dbo].[AccessToDiscountType]  WITH CHECK ADD  CONSTRAINT [FK_AccessToDiscountType_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [dbo].[DiscountType] ([Id])
GO
ALTER TABLE [dbo].[AccessToDiscountType] CHECK CONSTRAINT [FK_AccessToDiscountType_DiscountType]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_AspNetUsers] FOREIGN KEY([parent_id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_AspNetUsers]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_AspNetUsers1] FOREIGN KEY([InvitedUser])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_AspNetUsers1]
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_Discounts_DiscountType1] FOREIGN KEY([DiscountTypeId])
REFERENCES [dbo].[DiscountType] ([Id])
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_Discounts_DiscountType1]
GO
ALTER TABLE [dbo].[DiscountTypePaymentsPlans]  WITH CHECK ADD  CONSTRAINT [FK_DiscountTypePaymentsPlans_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [dbo].[DiscountType] ([Id])
GO
ALTER TABLE [dbo].[DiscountTypePaymentsPlans] CHECK CONSTRAINT [FK_DiscountTypePaymentsPlans_DiscountType]
GO
ALTER TABLE [dbo].[DiscountTypePaymentsPlans]  WITH CHECK ADD  CONSTRAINT [FK_DiscountTypePaymentsPlans_PaymentPlans] FOREIGN KEY([PaymentPlanId])
REFERENCES [dbo].[PaymentPlans] ([Id])
GO
ALTER TABLE [dbo].[DiscountTypePaymentsPlans] CHECK CONSTRAINT [FK_DiscountTypePaymentsPlans_PaymentPlans]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_AspNetUsers]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_Discounts] FOREIGN KEY([DiscountId])
REFERENCES [dbo].[Discounts] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_Discounts]
GO
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK_Payments_PaymentPlans] FOREIGN KEY([PaymentPlanId])
REFERENCES [dbo].[PaymentPlans] ([Id])
GO
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK_Payments_PaymentPlans]
GO
ALTER TABLE [dbo].[UserCommunications]  WITH CHECK ADD  CONSTRAINT [FK_UserCommunications_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCommunications] CHECK CONSTRAINT [FK_UserCommunications_AspNetUsers]
GO
ALTER TABLE [dbo].[UserCommunications]  WITH CHECK ADD  CONSTRAINT [FK_UserCommunications_Communicatios] FOREIGN KEY([CommunicationId])
REFERENCES [dbo].[Communicatios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCommunications] CHECK CONSTRAINT [FK_UserCommunications_Communicatios]
GO
ALTER TABLE [dbo].[UserDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_UserDiscounts_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserDiscounts] CHECK CONSTRAINT [FK_UserDiscounts_AspNetUsers]
GO
ALTER TABLE [dbo].[UserDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_UserDiscounts_Discounts] FOREIGN KEY([DiscountId])
REFERENCES [dbo].[Discounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserDiscounts] CHECK CONSTRAINT [FK_UserDiscounts_Discounts]
GO
ALTER TABLE [dbo].[UserInvitedDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_UserInvitedDiscounts_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UserInvitedDiscounts] CHECK CONSTRAINT [FK_UserInvitedDiscounts_AspNetUsers]
GO
ALTER TABLE [dbo].[UserInvitedDiscounts]  WITH CHECK ADD  CONSTRAINT [FK_UserInvitedDiscounts_Discounts] FOREIGN KEY([DiscountId])
REFERENCES [dbo].[Discounts] ([Id])
GO
ALTER TABLE [dbo].[UserInvitedDiscounts] CHECK CONSTRAINT [FK_UserInvitedDiscounts_Discounts]
GO
USE [master]
GO
ALTER DATABASE [identityDb] SET  READ_WRITE 
GO

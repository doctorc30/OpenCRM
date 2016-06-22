USE [master]
GO
/****** Object:  Database [elcoin]    Script Date: 07.04.2016 16:54:58 ******/
CREATE DATABASE [elcoin]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bbomDb_test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\elcoin.mdf' , SIZE = 11456KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'bbomDb_test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\elcoin_log.ldf' , SIZE = 7040KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [elcoin] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [elcoin].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [elcoin] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [elcoin] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [elcoin] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [elcoin] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [elcoin] SET ARITHABORT OFF 
GO
ALTER DATABASE [elcoin] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [elcoin] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [elcoin] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [elcoin] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [elcoin] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [elcoin] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [elcoin] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [elcoin] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [elcoin] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [elcoin] SET  DISABLE_BROKER 
GO
ALTER DATABASE [elcoin] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [elcoin] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [elcoin] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [elcoin] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [elcoin] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [elcoin] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [elcoin] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [elcoin] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [elcoin] SET  MULTI_USER 
GO
ALTER DATABASE [elcoin] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [elcoin] SET DB_CHAINING OFF 
GO
ALTER DATABASE [elcoin] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [elcoin] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [elcoin] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'elcoin', N'ON'
GO
USE [elcoin]
GO
/****** Object:  User [su]    Script Date: 07.04.2016 16:54:58 ******/
CREATE USER [su] FOR LOGIN [su] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [cos2]    Script Date: 07.04.2016 16:54:58 ******/
CREATE USER [cos2] FOR LOGIN [cos2] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [cos]    Script Date: 07.04.2016 16:54:58 ******/
CREATE USER [cos] FOR LOGIN [cos] WITH DEFAULT_SCHEMA=[db_datareader]
GO
ALTER ROLE [db_owner] ADD MEMBER [su]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [su]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [su]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [su]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [su]
GO
ALTER ROLE [db_datareader] ADD MEMBER [su]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [su]
GO
ALTER ROLE [db_owner] ADD MEMBER [cos2]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [cos2]
GO
ALTER ROLE [db_datareader] ADD MEMBER [cos2]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [cos2]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [cos]
GO
ALTER ROLE [db_datareader] ADD MEMBER [cos]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [cos]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccessToDiscountType]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[AccessToEventTypes]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessToEventTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventTypeId] [int] NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AccessToEventTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccessToMenu]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessToMenu](
	[RoleId] [nvarchar](128) NOT NULL,
	[MenuId] [int] NOT NULL,
 CONSTRAINT [PK_AccessToMenu] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 07.04.2016 16:54:58 ******/
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
	[ActiveTemplateId] [int] NULL,
	[Image] [image] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Communicatios]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[DefaultSettingsValues]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DefaultSettingsValues](
	[SettingId] [int] NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_DefaultSettingsValues] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[DiscountType]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[DiscountTypePaymentsPlans]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Title] [varchar](128) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[Url] [varchar](max) NULL,
	[TypeId] [int] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[Spiker] [varchar](max) NULL,
	[Icon] [varchar](200) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK__Events__3214EC079518918D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventSpikers]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventSpikers](
	[EventId] [int] NOT NULL,
	[SpikerId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_EventSpikers] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC,
	[SpikerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventTypes]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTypes](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExtraRegParams]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtraRegParams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ExtraRegParams] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menus]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Discription] [nvarchar](200) NULL,
	[Icon] [nvarchar](max) NULL,
	[Action] [nvarchar](50) NOT NULL,
	[Controller] [nvarchar](50) NOT NULL,
	[Enable] [int] NULL,
	[ParentId] [int] NULL,
	[OrderNum] [int] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaymentPlans]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[Payments]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[ReceivedExtraRegParams]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceivedExtraRegParams](
	[UserId] [nvarchar](128) NOT NULL,
	[ExtraRegParamId] [int] NOT NULL,
 CONSTRAINT [PK_ReceivedExtraRegParams] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ExtraRegParamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Settings]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaskToDo]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskToDo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Templates]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[Header] [nvarchar](max) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[Footer] [nvarchar](max) NOT NULL,
	[Id] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsDefault] [int] NOT NULL,
	[UserId] [nvarchar](128) NULL,
	[Links] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DefaultTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TemplateSettings]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemplateSettings](
	[TemplateId] [int] NOT NULL,
	[SettingId] [int] NOT NULL,
 CONSTRAINT [PK_TemplateSettings] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC,
	[SettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserCommunications]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[UserComplitedTasks]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserComplitedTasks](
	[UserId] [nvarchar](128) NOT NULL,
	[TaskId] [int] NOT NULL,
 CONSTRAINT [PK_UserComplitedTasks] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserDiscounts]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[UserExtraRegParams]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserExtraRegParams](
	[UserId] [nvarchar](128) NOT NULL,
	[ExtraRegParamId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserExtraRegParams] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ExtraRegParamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInvitedDiscounts]    Script Date: 07.04.2016 16:54:58 ******/
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
/****** Object:  Table [dbo].[UsersTemplateSettings]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UsersTemplateSettings](
	[UserId] [nvarchar](128) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[SettingId] [int] NOT NULL,
	[Value] [varchar](max) NOT NULL,
 CONSTRAINT [PK_UsersTemplateSettings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 07.04.2016 16:54:58 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 07.04.2016 16:54:58 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 07.04.2016 16:54:58 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 07.04.2016 16:54:58 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 07.04.2016 16:54:58 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 07.04.2016 16:54:58 ******/
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
ALTER TABLE [dbo].[AccessToEventTypes]  WITH CHECK ADD  CONSTRAINT [FK_AccessToEventTypes_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AccessToEventTypes] CHECK CONSTRAINT [FK_AccessToEventTypes_AspNetRoles]
GO
ALTER TABLE [dbo].[AccessToEventTypes]  WITH CHECK ADD  CONSTRAINT [FK_AccessToEventTypes_EventTypes] FOREIGN KEY([EventTypeId])
REFERENCES [dbo].[EventTypes] ([Id])
GO
ALTER TABLE [dbo].[AccessToEventTypes] CHECK CONSTRAINT [FK_AccessToEventTypes_EventTypes]
GO
ALTER TABLE [dbo].[AccessToMenu]  WITH CHECK ADD  CONSTRAINT [FK_AccessToMenu_AspNetRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
GO
ALTER TABLE [dbo].[AccessToMenu] CHECK CONSTRAINT [FK_AccessToMenu_AspNetRoles]
GO
ALTER TABLE [dbo].[AccessToMenu]  WITH CHECK ADD  CONSTRAINT [FK_AccessToMenu_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menus] ([Id])
GO
ALTER TABLE [dbo].[AccessToMenu] CHECK CONSTRAINT [FK_AccessToMenu_Menus]
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
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Tempalates] FOREIGN KEY([ActiveTemplateId])
REFERENCES [dbo].[Templates] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Tempalates]
GO
ALTER TABLE [dbo].[DefaultSettingsValues]  WITH CHECK ADD  CONSTRAINT [FK_DefaultSettingsValues_Settings] FOREIGN KEY([SettingId])
REFERENCES [dbo].[Settings] ([Id])
GO
ALTER TABLE [dbo].[DefaultSettingsValues] CHECK CONSTRAINT [FK_DefaultSettingsValues_Settings]
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_Discounts_DiscountType] FOREIGN KEY([Id])
REFERENCES [dbo].[DiscountType] ([Id])
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_Discounts_DiscountType]
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
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_AspNetUsers]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_EventTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[EventTypes] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_EventTypes]
GO
ALTER TABLE [dbo].[EventSpikers]  WITH CHECK ADD  CONSTRAINT [FK_EventSpikers_AspNetUsers] FOREIGN KEY([SpikerId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[EventSpikers] CHECK CONSTRAINT [FK_EventSpikers_AspNetUsers]
GO
ALTER TABLE [dbo].[EventSpikers]  WITH CHECK ADD  CONSTRAINT [FK_EventSpikers_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([Id])
GO
ALTER TABLE [dbo].[EventSpikers] CHECK CONSTRAINT [FK_EventSpikers_Events]
GO
ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menus_Menus] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Menus] ([Id])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menus_Menus]
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
ALTER TABLE [dbo].[ReceivedExtraRegParams]  WITH CHECK ADD  CONSTRAINT [FK_ReceivedExtraRegParams_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReceivedExtraRegParams] CHECK CONSTRAINT [FK_ReceivedExtraRegParams_AspNetUsers]
GO
ALTER TABLE [dbo].[ReceivedExtraRegParams]  WITH CHECK ADD  CONSTRAINT [FK_ReceivedExtraRegParams_ExtraRegParams] FOREIGN KEY([ExtraRegParamId])
REFERENCES [dbo].[ExtraRegParams] ([Id])
GO
ALTER TABLE [dbo].[ReceivedExtraRegParams] CHECK CONSTRAINT [FK_ReceivedExtraRegParams_ExtraRegParams]
GO
ALTER TABLE [dbo].[TemplateSettings]  WITH CHECK ADD  CONSTRAINT [FK_TemplateSettings_Settings] FOREIGN KEY([SettingId])
REFERENCES [dbo].[Settings] ([Id])
GO
ALTER TABLE [dbo].[TemplateSettings] CHECK CONSTRAINT [FK_TemplateSettings_Settings]
GO
ALTER TABLE [dbo].[TemplateSettings]  WITH CHECK ADD  CONSTRAINT [FK_TemplateSettings_Templates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([Id])
GO
ALTER TABLE [dbo].[TemplateSettings] CHECK CONSTRAINT [FK_TemplateSettings_Templates]
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
ALTER TABLE [dbo].[UserComplitedTasks]  WITH CHECK ADD  CONSTRAINT [FK_UserComplitedTasks_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserComplitedTasks] CHECK CONSTRAINT [FK_UserComplitedTasks_AspNetUsers]
GO
ALTER TABLE [dbo].[UserComplitedTasks]  WITH CHECK ADD  CONSTRAINT [FK_UserComplitedTasks_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[TaskToDo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserComplitedTasks] CHECK CONSTRAINT [FK_UserComplitedTasks_Tasks]
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
ALTER TABLE [dbo].[UserExtraRegParams]  WITH CHECK ADD  CONSTRAINT [FK_UserExtraRegParams_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserExtraRegParams] CHECK CONSTRAINT [FK_UserExtraRegParams_AspNetUsers]
GO
ALTER TABLE [dbo].[UserExtraRegParams]  WITH CHECK ADD  CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams] FOREIGN KEY([ExtraRegParamId])
REFERENCES [dbo].[ExtraRegParams] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserExtraRegParams] CHECK CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams]
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
ALTER TABLE [dbo].[UsersTemplateSettings]  WITH CHECK ADD  CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersTemplateSettings] CHECK CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers]
GO
ALTER TABLE [dbo].[UsersTemplateSettings]  WITH CHECK ADD  CONSTRAINT [FK_UsersTemplateSettings_Settings] FOREIGN KEY([SettingId])
REFERENCES [dbo].[Settings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersTemplateSettings] CHECK CONSTRAINT [FK_UsersTemplateSettings_Settings]
GO
ALTER TABLE [dbo].[UsersTemplateSettings]  WITH CHECK ADD  CONSTRAINT [FK_UsersTemplateSettings_Templates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersTemplateSettings] CHECK CONSTRAINT [FK_UsersTemplateSettings_Templates]
GO
/****** Object:  StoredProcedure [dbo].[clear_users_childs]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[clear_users_childs](@userName NVARCHAR(256)) AS
BEGIN
  SET NOCOUNT ON;
  DECLARE @id NVARCHAR(128)
  DECLARE @level INT
  DECLARE @un NVARCHAR(256)
  DECLARE users CURSOR LOCAL FOR
    WITH tree (parentId, userId, userName, Level)
    AS
    (
      -- Anchor member definition
      SELECT
        u.parent_id,
        u.Id,
        u.UserName,
        0 AS Level
      FROM elcoin.dbo.AspNetUsers AS u
      WHERE u.UserName = @userName
      UNION ALL
      -- Recursive member definition
      SELECT
        u.parent_id,
        u.Id,
        u.UserName,
        Level + 1
      FROM elcoin.dbo.AspNetUsers AS u
        INNER JOIN tree AS d
          ON u.parent_id = d.userId
    )
    SELECT userId
    FROM tree
    WHERE userName <> @userName
    ORDER BY Level DESC
  OPEN users
  FETCH NEXT FROM users
  INTO @id
  WHILE @@fetch_status = 0
    BEGIN
      DELETE FROM elcoin.dbo.AspNetUsers
      WHERE id = @id
      FETCH NEXT FROM users
      INTO @id
    END
END
GO
/****** Object:  StoredProcedure [dbo].[get_conections]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_conections](@userName VARCHAR(200))
AS
BEGIN
  SELECT  spid,
        sp.[status],
        loginame [Login],
        hostname,
        blocked BlkBy,
        sd.name DBName,
        cmd Command,
        cpu CPUTime,
        physical_io DiskIO,
        last_batch LastBatch,
        [program_name] ProgramName
FROM master.dbo.sysprocesses sp
JOIN master.dbo.sysdatabases sd ON sp.dbid = sd.dbid
  WHERE sd.name = 'elcoin' AND  loginame = @userName
ORDER BY spid
END
GO
/****** Object:  StoredProcedure [dbo].[get_conections_bbom2db_cos2]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_conections_bbom2db_cos2]
AS
BEGIN
  SELECT  spid,
        sp.[status],
        loginame [Login],
        hostname,
        blocked BlkBy,
        sd.name DBName,
        cmd Command,
        cpu CPUTime,
        physical_io DiskIO,
        last_batch LastBatch,
        [program_name] ProgramName
FROM master.dbo.sysprocesses sp
JOIN master.dbo.sysdatabases sd ON sp.dbid = sd.dbid
  WHERE sd.name = 'elcoin' AND  loginame = 'cos2'
ORDER BY spid
END
GO
/****** Object:  StoredProcedure [dbo].[get_user_roles]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_user_roles](@userName VARCHAR(MAX))
AS
BEGIN
  SELECT
    u.UserName,
    r.Name
  FROM elcoin.dbo.AspNetUsers u
    JOIN elcoin.dbo.AspNetUserRoles ur
      ON u.Id = ur.UserId
    JOIN elcoin.dbo.AspNetRoles r
      ON ur.RoleId = r.Id
  WHERE u.UserName = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateRoleForNotPayUsers]    Script Date: 07.04.2016 16:54:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateRoleForNotPayUsers]
AS
  BEGIN
    SET NOCOUNT ON;
    DECLARE @id NVARCHAR(128)
    DECLARE users CURSOR LOCAL SCROLL FOR
      SELECT u.id
      FROM elcoin.dbo.AspNetUsers u
        JOIN elcoin.dbo.Payments p
          ON p.id IN (SELECT TOP 1 id
                      FROM elcoin.dbo.Payments
                      WHERE UserId = u.Id AND Status = 1
                      ORDER BY Date DESC)
      WHERE p.EndDate <= GETDATE() AND u.Id NOT IN (SELECT u.Id
                                                    FROM elcoin.dbo.AspNetUsers u
                                                      JOIN elcoin.dbo.AspNetUserRoles ur
                                                        ON u.Id = ur.UserId
                                                      JOIN elcoin.dbo.AspNetRoles r
                                                        ON ur.RoleId = r.Id
                                                    WHERE r.Name = 'notPay')
    OPEN users
    FETCH NEXT FROM users
    INTO @id
    WHILE @@fetch_status = 0
      BEGIN
        INSERT INTO elcoin.dbo.AspNetUserRoles (UserId, RoleId) VALUES (@id, '02f990cc-cbf5-4db2-8d5d-a8ad98782b0b')
        FETCH NEXT FROM users
        INTO @id
      END
  END
GO
USE [master]
GO
ALTER DATABASE [elcoin] SET  READ_WRITE 
GO

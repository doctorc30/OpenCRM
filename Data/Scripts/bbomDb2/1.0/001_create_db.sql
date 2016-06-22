USE [master]
GO
/****** Object:  Database [bbomDb2]    Script Date: 18.02.2016 14:56:17 ******/
CREATE DATABASE [bbomDb2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bbomDb_test', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\bbomDb2.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'bbomDb_test_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\bbomDb2_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [bbomDb2] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bbomDb2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bbomDb2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bbomDb2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bbomDb2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bbomDb2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bbomDb2] SET ARITHABORT OFF 
GO
ALTER DATABASE [bbomDb2] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [bbomDb2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bbomDb2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bbomDb2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bbomDb2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bbomDb2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bbomDb2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bbomDb2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bbomDb2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bbomDb2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [bbomDb2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bbomDb2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bbomDb2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bbomDb2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bbomDb2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bbomDb2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bbomDb2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bbomDb2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [bbomDb2] SET  MULTI_USER 
GO
ALTER DATABASE [bbomDb2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bbomDb2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bbomDb2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bbomDb2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [bbomDb2] SET DELAYED_DURABILITY = DISABLED 
GO
USE [bbomDb2]
GO
/****** Object:  User [cos]    Script Date: 18.02.2016 14:56:17 ******/
CREATE USER [cos] FOR LOGIN [cos] WITH DEFAULT_SCHEMA=[db_datareader]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [cos]
GO
ALTER ROLE [db_datareader] ADD MEMBER [cos]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [cos]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 18.02.2016 14:56:17 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 18.02.2016 14:56:17 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18.02.2016 14:56:17 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 18.02.2016 14:56:17 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 18.02.2016 14:56:17 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18.02.2016 14:56:17 ******/
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
	[Altname] [nvarchar](128) NULL
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CreatedTemplates]    Script Date: 18.02.2016 14:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreatedTemplates](
	[header] [nvarchar](max) NOT NULL,
	[body] [nvarchar](max) NOT NULL,
	[footer] [nvarchar](max) NOT NULL,
	[id] [int] NOT NULL,
 CONSTRAINT [PK_CreatedTemplates] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Events]    Script Date: 18.02.2016 14:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Title] [varchar](128) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[Url] [varchar](max) NULL,
	[TypeId] [int] NOT NULL,
 CONSTRAINT [PK__Events__3214EC079518918D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventTypes]    Script Date: 18.02.2016 14:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTypes](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_EventTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Templates]    Script Date: 18.02.2016 14:56:17 ******/
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
/****** Object:  Table [dbo].[UsersActiveTemplates]    Script Date: 18.02.2016 14:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersActiveTemplates](
	[UserId] [nvarchar](128) NOT NULL,
	[TemplateId] [int] NOT NULL,
 CONSTRAINT [PK_UsersActiveTemplates] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UsersTemplateSettings]    Script Date: 18.02.2016 14:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersTemplateSettings] (
    [UserId]     NVARCHAR (128) NOT NULL,
    [TemplateId] INT            NOT NULL,
    [Id]         INT            IDENTITY (0, 1) NOT NULL,
    [SettingId] INT NOT NULL, 
    [Value] VARCHAR(MAX) NOT NULL, 
    CONSTRAINT [PK_UsersTemplateSettings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_UsersTemplateSettings_Templates] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Templates] ([Id]),
	CONSTRAINT [FK_UsersTemplateSettings_Settings] FOREIGN KEY ([SettingId]) REFERENCES [dbo].[Settings] ([Id])
)

GO
/****** Object:  Table [dbo].[UsersTree]    Script Date: 18.02.2016 14:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersTree](
	[UserId] [nvarchar](128) NOT NULL,
	[InvitedUserId] [nvarchar](128) NOT NULL,
	[Foot] [int] NULL,
 CONSTRAINT [PK_UsersTree] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[InvitedUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserTemplates]    Script Date: 18.02.2016 14:56:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTemplates](
	[IdUser] [nvarchar](128) NOT NULL,
	[IdTemplateDefault] [int] NOT NULL,
	[IdTemplateCreateed] [int] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 18.02.2016 14:56:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 18.02.2016 14:56:17 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 18.02.2016 14:56:17 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 18.02.2016 14:56:17 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 18.02.2016 14:56:17 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 18.02.2016 14:56:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_EventTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[EventTypes] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_EventTypes]
GO
ALTER TABLE [dbo].[UsersActiveTemplates]  WITH CHECK ADD  CONSTRAINT [FK_UsersActiveTemplates_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UsersActiveTemplates] CHECK CONSTRAINT [FK_UsersActiveTemplates_AspNetUsers]
GO
ALTER TABLE [dbo].[UsersActiveTemplates]  WITH CHECK ADD  CONSTRAINT [FK_UsersActiveTemplates_Templates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([Id])
GO
ALTER TABLE [dbo].[UsersActiveTemplates] CHECK CONSTRAINT [FK_UsersActiveTemplates_Templates]
GO
ALTER TABLE [dbo].[UsersTemplateSettings]  WITH CHECK ADD  CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UsersTemplateSettings] CHECK CONSTRAINT [FK_UsersTemplateSettings_AspNetUsers]
GO
ALTER TABLE [dbo].[UsersTemplateSettings]  WITH CHECK ADD  CONSTRAINT [FK_UsersTemplateSettings_Templates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[Templates] ([Id])
GO
ALTER TABLE [dbo].[UsersTemplateSettings] CHECK CONSTRAINT [FK_UsersTemplateSettings_Templates]
GO
ALTER TABLE [dbo].[UsersTree]  WITH CHECK ADD  CONSTRAINT [FK_UsersTree_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UsersTree] CHECK CONSTRAINT [FK_UsersTree_AspNetUsers]
GO
ALTER TABLE [dbo].[UsersTree]  WITH CHECK ADD  CONSTRAINT [FK_UsersTree_AspNetUsers1] FOREIGN KEY([InvitedUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UsersTree] CHECK CONSTRAINT [FK_UsersTree_AspNetUsers1]
GO
USE [master]
GO
ALTER DATABASE [bbomDb2] SET  READ_WRITE 
GO

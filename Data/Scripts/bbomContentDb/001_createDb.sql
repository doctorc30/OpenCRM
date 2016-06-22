USE [master]
GO
/****** Object:  Database [bbomContentDb]    Script Date: 25.04.2016 18:44:27 ******/
CREATE DATABASE [bbomContentDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bbomContentDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\bbomContentDb.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'bbomContentDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.BIOHAZARD\MSSQL\DATA\bbomContentDb_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [bbomContentDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [bbomContentDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [bbomContentDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [bbomContentDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [bbomContentDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [bbomContentDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [bbomContentDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [bbomContentDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [bbomContentDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [bbomContentDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [bbomContentDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [bbomContentDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [bbomContentDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [bbomContentDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [bbomContentDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [bbomContentDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [bbomContentDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [bbomContentDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [bbomContentDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [bbomContentDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [bbomContentDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [bbomContentDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [bbomContentDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [bbomContentDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [bbomContentDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [bbomContentDb] SET  MULTI_USER 
GO
ALTER DATABASE [bbomContentDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [bbomContentDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [bbomContentDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [bbomContentDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [bbomContentDb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [bbomContentDb]
GO
/****** Object:  Table [dbo].[AccessToEventTypes]    Script Date: 25.04.2016 18:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessToEventTypes](
	[EventTypeId] [int] NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_AccessToMenu2] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[EventTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AccessToMenu]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[DefaultSettingsValues]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[Events]    Script Date: 25.04.2016 18:44:27 ******/
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
	[UserId] [nvarchar](128) NOT NULL,
	[Description] [varchar](max) NULL,
	[Spiker] [varchar](max) NULL,
	[Icon] [varchar](200) NULL,
	[Stats] [int] NULL,
 CONSTRAINT [PK__Events__3214EC079518918D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EventSpikers]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[EventTypes]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[ExtraRegParams]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[Menus]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[ReceivedExtraRegParams]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[Settings]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[TaskToDo]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[Templates]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[TemplateSettings]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[UserComplitedTasks]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[UserExtraRegParams]    Script Date: 25.04.2016 18:44:27 ******/
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
/****** Object:  Table [dbo].[UsersTemplateSettings]    Script Date: 25.04.2016 18:44:27 ******/
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
ALTER TABLE [dbo].[AccessToEventTypes]  WITH CHECK ADD  CONSTRAINT [FK_AccessToEventTypes_EventTypes] FOREIGN KEY([EventTypeId])
REFERENCES [dbo].[EventTypes] ([Id])
GO
ALTER TABLE [dbo].[AccessToEventTypes] CHECK CONSTRAINT [FK_AccessToEventTypes_EventTypes]
GO
ALTER TABLE [dbo].[AccessToMenu]  WITH CHECK ADD  CONSTRAINT [FK_AccessToMenu_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menus] ([Id])
GO
ALTER TABLE [dbo].[AccessToMenu] CHECK CONSTRAINT [FK_AccessToMenu_Menus]
GO
ALTER TABLE [dbo].[DefaultSettingsValues]  WITH CHECK ADD  CONSTRAINT [FK_DefaultSettingsValues_Settings] FOREIGN KEY([SettingId])
REFERENCES [dbo].[Settings] ([Id])
GO
ALTER TABLE [dbo].[DefaultSettingsValues] CHECK CONSTRAINT [FK_DefaultSettingsValues_Settings]
GO
ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_EventTypes] FOREIGN KEY([TypeId])
REFERENCES [dbo].[EventTypes] ([Id])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_EventTypes]
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
ALTER TABLE [dbo].[UserComplitedTasks]  WITH CHECK ADD  CONSTRAINT [FK_UserComplitedTasks_Tasks] FOREIGN KEY([TaskId])
REFERENCES [dbo].[TaskToDo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserComplitedTasks] CHECK CONSTRAINT [FK_UserComplitedTasks_Tasks]
GO
ALTER TABLE [dbo].[UserExtraRegParams]  WITH CHECK ADD  CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams] FOREIGN KEY([ExtraRegParamId])
REFERENCES [dbo].[ExtraRegParams] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserExtraRegParams] CHECK CONSTRAINT [FK_UserExtraRegParams_ExtraRegParams]
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
USE [master]
GO
ALTER DATABASE [bbomContentDb] SET  READ_WRITE 
GO

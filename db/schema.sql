/****** Object:  StoredProcedure [dbo].[SPC_DELETE_GOAL]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPC_DELETE_GOAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SPC_DELETE_GOAL]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__Tip__Enable__25869641]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Tip] DROP CONSTRAINT [DF__Tip__Enable__25869641]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Notification_IsPush]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Notification] DROP CONSTRAINT [DF_Notification_IsPush]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Notification_IsEmail]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Notification] DROP CONSTRAINT [DF_Notification_IsEmail]
END

GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Notification_IsSms]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Notification] DROP CONSTRAINT [DF_Notification_IsSms]
END

GO
/****** Object:  Index [IX_Operation_GoalId_GoalTypeId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operation]') AND name = N'IX_Operation_GoalId_GoalTypeId')
DROP INDEX [IX_Operation_GoalId_GoalTypeId] ON [dbo].[Operation]
GO
/****** Object:  Index [UserNameIndex]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND name = N'UserNameIndex')
DROP INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_UserId')
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_RoleId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_RoleId')
DROP INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND name = N'IX_UserId')
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
GO
/****** Object:  Index [IX_UserId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND name = N'IX_UserId')
DROP INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND name = N'RoleNameIndex')
DROP INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[UserDevice]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDevice]') AND type in (N'U'))
DROP TABLE [dbo].[UserDevice]
GO
/****** Object:  Table [dbo].[UserConfiguration]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserConfiguration]') AND type in (N'U'))
DROP TABLE [dbo].[UserConfiguration]
GO
/****** Object:  Table [dbo].[Tip]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tip]') AND type in (N'U'))
DROP TABLE [dbo].[Tip]
GO
/****** Object:  Table [dbo].[Saving]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Saving]') AND type in (N'U'))
DROP TABLE [dbo].[Saving]
GO
/****** Object:  Table [dbo].[Recurrent]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recurrent]') AND type in (N'U'))
DROP TABLE [dbo].[Recurrent]
GO
/****** Object:  Table [dbo].[Planification]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Planification]') AND type in (N'U'))
DROP TABLE [dbo].[Planification]
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operation]') AND type in (N'U'))
DROP TABLE [dbo].[Operation]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notification]') AND type in (N'U'))
DROP TABLE [dbo].[Notification]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Message]') AND type in (N'U'))
DROP TABLE [dbo].[Message]
GO
/****** Object:  Table [dbo].[GroupDetail]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupDetail]') AND type in (N'U'))
DROP TABLE [dbo].[GroupDetail]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND type in (N'U'))
DROP TABLE [dbo].[Group]
GO
/****** Object:  Table [dbo].[Goal]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Goal]') AND type in (N'U'))
DROP TABLE [dbo].[Goal]
GO
/****** Object:  Table [dbo].[Debt]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Debt]') AND type in (N'U'))
DROP TABLE [dbo].[Debt]
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Catalog]') AND type in (N'U'))
DROP TABLE [dbo].[Catalog]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUsers]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserRoles]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserLogins]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetUserClaims]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
DROP TABLE [dbo].[AspNetRoles]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__MigrationHistory]
GO
/****** Object:  Database [Salvis_db]    Script Date: 10/16/2016 6:53:39 PM ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'Salvis_db')
DROP DATABASE [Salvis_db]
GO
/****** Object:  Database [Salvis_db]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'Salvis_db')
BEGIN
CREATE DATABASE [Salvis_db] ON  PRIMARY 
( NAME = N'Salvis_db', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Salvis_db.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Salvis_db_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Salvis_db_log.ldf' , SIZE = 1344KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
END

GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Salvis_db].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Salvis_db] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Salvis_db] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Salvis_db] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Salvis_db] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Salvis_db] SET ARITHABORT OFF 
GO
ALTER DATABASE [Salvis_db] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Salvis_db] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Salvis_db] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Salvis_db] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Salvis_db] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Salvis_db] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Salvis_db] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Salvis_db] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Salvis_db] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Salvis_db] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Salvis_db] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Salvis_db] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Salvis_db] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Salvis_db] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Salvis_db] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Salvis_db] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Salvis_db] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Salvis_db] SET RECOVERY FULL 
GO
ALTER DATABASE [Salvis_db] SET  MULTI_USER 
GO
ALTER DATABASE [Salvis_db] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Salvis_db] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Salvis_db', N'ON'
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__MigrationHistory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContextKey] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderKey] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Email] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Enable] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Catalog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Catalog](
	[Category] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SubCategoryId] [int] NOT NULL,
	[Value] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DescriptionES] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DescriptionEN] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_Catalog] PRIMARY KEY CLUSTERED 
(
	[Category] ASC,
	[SubCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Debt]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Debt]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Debt](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ReasonTypeId] [int] NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Goal]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Goal]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Goal](
	[ParentId] [bigint] NOT NULL,
	[ParentTypeId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[EndDate] [smalldatetime] NOT NULL,
	[Amount] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ParentTypeId] ASC,
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Group]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Group]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Group](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[GroupDetail]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GroupDetail](
	[Id] [bigint] NOT NULL,
	[MemberId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Message]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Message]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Message](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[ParentTypeId] [int] NOT NULL,
	[InputDate] [smalldatetime] NOT NULL,
	[TypeId] [int] NOT NULL,
	[State] [int] NOT NULL,
	[Subject] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Content] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FromUserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notification]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Notification](
	[ParentId] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentTypeId] [int] NOT NULL,
	[ReleaseDate] [datetimeoffset](7) NOT NULL,
	[IsSms] [bit] NOT NULL,
	[IsEmail] [bit] NOT NULL,
	[IsPush] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ParentId] ASC,
	[ParentTypeId] ASC,
	[ReleaseDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Operation]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Operation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Operation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GoalId] [bigint] NOT NULL,
	[GoalTypeId] [int] NOT NULL,
	[IntervalTypeId] [int] NOT NULL,
	[InputDate] [smalldatetime] NOT NULL,
	[ExpValue] [float] NOT NULL,
	[RealValue] [float] NULL,
	[EffectedDate] [smalldatetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Planification]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Planification]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Planification](
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GoalId] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Recurrent]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recurrent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Recurrent](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ReasonTypeId] [int] NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Saving]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Saving]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Saving](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ReasonTypeId] [int] NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tip]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tip]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tip](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Type] [int] NOT NULL,
	[DescriptionES] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DescriptionEN] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Enable] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserConfiguration]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserConfiguration]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserConfiguration](
	[Category] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SubCategory] [int] NOT NULL,
	[Value] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SubCategory] ASC,
	[Category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[UserDevice]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserDevice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserDevice](
	[DeviceId] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
PRIMARY KEY CLUSTERED 
(
	[DeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetRoles]') AND name = N'RoleNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_RoleId')
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]') AND name = N'IX_UserId')
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[AspNetUsers]') AND name = N'UserNameIndex')
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Operation_GoalId_GoalTypeId]    Script Date: 10/16/2016 6:53:39 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Operation]') AND name = N'IX_Operation_GoalId_GoalTypeId')
CREATE NONCLUSTERED INDEX [IX_Operation_GoalId_GoalTypeId] ON [dbo].[Operation]
(
	[GoalId] ASC,
	[GoalTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Notification_IsSms]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Notification] ADD  CONSTRAINT [DF_Notification_IsSms]  DEFAULT ((0)) FOR [IsSms]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Notification_IsEmail]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Notification] ADD  CONSTRAINT [DF_Notification_IsEmail]  DEFAULT ((0)) FOR [IsEmail]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_Notification_IsPush]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Notification] ADD  CONSTRAINT [DF_Notification_IsPush]  DEFAULT ((0)) FOR [IsPush]
END

GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__Tip__Enable__25869641]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Tip] ADD  DEFAULT ((0)) FOR [Enable]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserClaims]'))
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserLogins]'))
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]') AND parent_object_id = OBJECT_ID(N'[dbo].[AspNetUserRoles]'))
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[SPC_DELETE_GOAL]    Script Date: 10/16/2016 6:53:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPC_DELETE_GOAL]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SPC_DELETE_GOAL] AS' 
END
GO
-- =============================================
-- Author:		José Cordero
-- Create date: 20150615
-- Description:	Deletes all about a Goal.
-- =============================================
ALTER PROCEDURE [dbo].[SPC_DELETE_GOAL]
	@code as nvarchar(50)
	,@type as int
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE
	@parentId as int
	
	SELECT @parentId = Id FROM dbo.Debt WHERE Code = @code;
	DELETE FROM [dbo].[Notification]
	WHERE ParentId = @parentId AND ParentTypeId = @type;
		
	-- Lo siguiente es temporal, se deberán mover los registros a tablas para posterior análisis.
	DELETE FROM dbo.Operation
	WHERE GoalId = @parentId AND GoalTypeId = @type;
	
	DELETE FROM dbo.Goal
	WHERE ParentId = @parentId AND ParentTypeId = @type;
	
	IF @type = 1 
	BEGIN
		DELETE FROM [dbo].[Saving]
		WHERE Code = @code
	END
	ELSE IF @type = 2
	BEGIN
		DELETE FROM dbo.Debt
		WHERE Code = @code
	END
	ELSE IF @type = 3
	BEGIN
		DELETE FROM dbo.[Recurrent]
		WHERE Code = @code
	END
	SELECT 0
	
END

GO
ALTER DATABASE [Salvis_db] SET  READ_WRITE 
GO

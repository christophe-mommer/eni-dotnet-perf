USE [master]
GO
/****** Object:  Database [PROFI]    Script Date: 20/10/2021 07:52:18 ******/
CREATE DATABASE [PROFI]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PROFI', FILENAME = N'C:\Users\chris\PROFI.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PROFI_log', FILENAME = N'C:\Users\chris\PROFI_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [PROFI] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PROFI].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PROFI] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PROFI] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PROFI] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PROFI] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PROFI] SET ARITHABORT OFF 
GO
ALTER DATABASE [PROFI] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [PROFI] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PROFI] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PROFI] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PROFI] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PROFI] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PROFI] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PROFI] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PROFI] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PROFI] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PROFI] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PROFI] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PROFI] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PROFI] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PROFI] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PROFI] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PROFI] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PROFI] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PROFI] SET  MULTI_USER 
GO
ALTER DATABASE [PROFI] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PROFI] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PROFI] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PROFI] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PROFI] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PROFI] SET QUERY_STORE = OFF
GO
USE [PROFI]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [PROFI]
GO
/****** Object:  Table [dbo].[CONTRAT]    Script Date: 20/10/2021 07:52:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CONTRAT](
	[uid] [nchar](32) NOT NULL,
	[titulaire] [nchar](32) NULL,
	[montant] [numeric](9, 0) NOT NULL,
	[debut] [datetime] NULL,
	[reduction] [nvarchar](8) NULL,
 CONSTRAINT [PK_CONTRAT] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERSONNE]    Script Date: 20/10/2021 07:52:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERSONNE](
	[uid] [nchar](32) NOT NULL,
	[nom] [nvarchar](60) NOT NULL,
	[prenom] [nvarchar](60) NULL,
	[description] [nvarchar](500) NULL,
 CONSTRAINT [PK_PERSONNE] PRIMARY KEY CLUSTERED 
(
	[uid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000001', N'00000000000000000000000000000001', CAST(10000 AS Numeric(9, 0)), CAST(N'2010-12-25T00:00:00.000' AS DateTime), N'[:900]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000002', N'00000000000000000000000000000001', CAST(20000 AS Numeric(9, 0)), CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000003', N'00000000000000000000000000000001', CAST(258077 AS Numeric(9, 0)), CAST(N'2010-08-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000004', N'00000000000000000000000000000001', CAST(899383 AS Numeric(9, 0)), CAST(N'2010-04-23T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000005', N'00000000000000000000000000000001', CAST(38572 AS Numeric(9, 0)), CAST(N'2010-07-30T00:00:00.000' AS DateTime), N'[:800]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000006', N'00000000000000000000000000000001', CAST(685003 AS Numeric(9, 0)), CAST(N'2010-08-09T00:00:00.000' AS DateTime), N'10')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000007', N'00000000000000000000000000000001', CAST(964420 AS Numeric(9, 0)), CAST(N'2010-09-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000008', N'00000000000000000000000000000001', CAST(924204 AS Numeric(9, 0)), CAST(N'2010-05-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000009', N'00000000000000000000000000000001', CAST(723736 AS Numeric(9, 0)), CAST(N'2010-08-28T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000010', N'00000000000000000000000000000001', CAST(875702 AS Numeric(9, 0)), CAST(N'2010-06-17T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000011', N'00000000000000000000000000000001', CAST(884930 AS Numeric(9, 0)), CAST(N'2010-04-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000012', N'00000000000000000000000000000001', CAST(198437 AS Numeric(9, 0)), CAST(N'2010-05-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000013', N'00000000000000000000000000000002', CAST(156182 AS Numeric(9, 0)), CAST(N'2010-02-04T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000014', N'00000000000000000000000000000002', CAST(371582 AS Numeric(9, 0)), CAST(N'2010-01-18T00:00:00.000' AS DateTime), N'[:1000]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000015', N'00000000000000000000000000000002', CAST(978976 AS Numeric(9, 0)), CAST(N'2010-07-13T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000016', N'00000000000000000000000000000002', CAST(95847 AS Numeric(9, 0)), CAST(N'2010-09-30T00:00:00.000' AS DateTime), N'[:1200]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000017', N'00000000000000000000000000000002', CAST(185616 AS Numeric(9, 0)), CAST(N'2010-11-14T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000018', N'00000000000000000000000000000002', CAST(953922 AS Numeric(9, 0)), CAST(N'2010-06-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000019', N'00000000000000000000000000000002', CAST(409936 AS Numeric(9, 0)), CAST(N'2010-06-28T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000020', N'00000000000000000000000000000002', CAST(814765 AS Numeric(9, 0)), CAST(N'2010-10-03T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000021', N'00000000000000000000000000000002', CAST(566784 AS Numeric(9, 0)), CAST(N'2010-05-06T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000022', N'00000000000000000000000000000002', CAST(517518 AS Numeric(9, 0)), CAST(N'2010-01-29T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000023', N'00000000000000000000000000000002', CAST(484666 AS Numeric(9, 0)), CAST(N'2010-11-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000024', N'00000000000000000000000000000002', CAST(335866 AS Numeric(9, 0)), CAST(N'2010-07-18T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000025', N'00000000000000000000000000000002', CAST(155164 AS Numeric(9, 0)), CAST(N'2010-08-19T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000026', N'00000000000000000000000000000003', CAST(788557 AS Numeric(9, 0)), CAST(N'2010-12-26T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000027', N'00000000000000000000000000000003', CAST(936828 AS Numeric(9, 0)), CAST(N'2010-08-29T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000028', N'00000000000000000000000000000003', CAST(930538 AS Numeric(9, 0)), CAST(N'2010-09-16T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000029', N'00000000000000000000000000000003', CAST(102872 AS Numeric(9, 0)), CAST(N'2010-12-27T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000030', N'00000000000000000000000000000003', CAST(232861 AS Numeric(9, 0)), CAST(N'2010-10-28T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000031', N'00000000000000000000000000000003', CAST(750056 AS Numeric(9, 0)), CAST(N'2010-05-09T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000032', N'00000000000000000000000000000003', CAST(44485 AS Numeric(9, 0)), CAST(N'2010-10-21T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000033', N'00000000000000000000000000000003', CAST(403845 AS Numeric(9, 0)), CAST(N'2010-06-15T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000034', N'00000000000000000000000000000003', CAST(463268 AS Numeric(9, 0)), CAST(N'2010-08-26T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000035', N'00000000000000000000000000000003', CAST(782845 AS Numeric(9, 0)), CAST(N'2010-11-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000036', N'00000000000000000000000000000003', CAST(867751 AS Numeric(9, 0)), CAST(N'2010-10-25T00:00:00.000' AS DateTime), N'[:1800]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000037', N'00000000000000000000000000000003', CAST(395372 AS Numeric(9, 0)), CAST(N'2010-08-15T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000038', N'00000000000000000000000000000004', CAST(47317 AS Numeric(9, 0)), CAST(N'2010-11-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000039', N'00000000000000000000000000000004', CAST(146803 AS Numeric(9, 0)), CAST(N'2010-08-21T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000040', N'00000000000000000000000000000004', CAST(645930 AS Numeric(9, 0)), CAST(N'2010-05-03T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000041', N'00000000000000000000000000000004', CAST(86940 AS Numeric(9, 0)), CAST(N'2010-04-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000042', N'00000000000000000000000000000004', CAST(269046 AS Numeric(9, 0)), CAST(N'2010-08-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000043', N'00000000000000000000000000000004', CAST(285274 AS Numeric(9, 0)), CAST(N'2010-02-24T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000044', N'00000000000000000000000000000004', CAST(954845 AS Numeric(9, 0)), CAST(N'2010-05-15T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000045', N'00000000000000000000000000000004', CAST(367553 AS Numeric(9, 0)), CAST(N'2010-06-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000046', N'00000000000000000000000000000004', CAST(84601 AS Numeric(9, 0)), CAST(N'2010-02-26T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000047', N'00000000000000000000000000000004', CAST(136239 AS Numeric(9, 0)), CAST(N'2010-03-04T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000048', N'00000000000000000000000000000004', CAST(48234 AS Numeric(9, 0)), CAST(N'2010-11-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000049', N'00000000000000000000000000000004', CAST(423289 AS Numeric(9, 0)), CAST(N'2010-09-14T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000050', N'00000000000000000000000000000004', CAST(96159 AS Numeric(9, 0)), CAST(N'2010-11-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000051', N'00000000000000000000000000000005', CAST(984908 AS Numeric(9, 0)), CAST(N'2010-10-02T00:00:00.000' AS DateTime), N'[:9000]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000052', N'00000000000000000000000000000005', CAST(412283 AS Numeric(9, 0)), CAST(N'2010-05-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000053', N'00000000000000000000000000000005', CAST(800722 AS Numeric(9, 0)), CAST(N'2010-07-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000054', N'00000000000000000000000000000005', CAST(198063 AS Numeric(9, 0)), CAST(N'2010-07-21T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000055', N'00000000000000000000000000000005', CAST(39329 AS Numeric(9, 0)), CAST(N'2010-06-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000056', N'00000000000000000000000000000005', CAST(15229 AS Numeric(9, 0)), CAST(N'2010-09-13T00:00:00.000' AS DateTime), N'[:900]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000057', N'00000000000000000000000000000005', CAST(169370 AS Numeric(9, 0)), CAST(N'2010-01-15T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000058', N'00000000000000000000000000000005', CAST(851952 AS Numeric(9, 0)), CAST(N'2010-07-06T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000059', N'00000000000000000000000000000005', CAST(951192 AS Numeric(9, 0)), CAST(N'2010-01-13T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000060', N'00000000000000000000000000000005', CAST(884508 AS Numeric(9, 0)), CAST(N'2010-11-27T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000061', N'00000000000000000000000000000005', CAST(723923 AS Numeric(9, 0)), CAST(N'2010-03-07T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000062', N'00000000000000000000000000000005', CAST(619489 AS Numeric(9, 0)), CAST(N'2010-12-19T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000063', N'00000000000000000000000000000005', CAST(436955 AS Numeric(9, 0)), CAST(N'2010-12-03T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000064', N'00000000000000000000000000000006', CAST(657332 AS Numeric(9, 0)), CAST(N'2010-04-20T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000065', N'00000000000000000000000000000006', CAST(576395 AS Numeric(9, 0)), CAST(N'2010-12-20T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000066', N'00000000000000000000000000000006', CAST(444170 AS Numeric(9, 0)), CAST(N'2010-01-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000067', N'00000000000000000000000000000006', CAST(930456 AS Numeric(9, 0)), CAST(N'2010-05-01T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000068', N'00000000000000000000000000000006', CAST(59105 AS Numeric(9, 0)), CAST(N'2010-12-26T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000069', N'00000000000000000000000000000006', CAST(679996 AS Numeric(9, 0)), CAST(N'2010-02-08T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000070', N'00000000000000000000000000000006', CAST(867285 AS Numeric(9, 0)), CAST(N'2010-06-11T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000071', N'00000000000000000000000000000006', CAST(945058 AS Numeric(9, 0)), CAST(N'2010-11-21T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000072', N'00000000000000000000000000000006', CAST(380868 AS Numeric(9, 0)), CAST(N'2010-06-12T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000073', N'00000000000000000000000000000006', CAST(475080 AS Numeric(9, 0)), CAST(N'2010-06-15T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000074', N'00000000000000000000000000000006', CAST(609866 AS Numeric(9, 0)), CAST(N'2010-07-22T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000075', N'00000000000000000000000000000006', CAST(307142 AS Numeric(9, 0)), CAST(N'2010-07-24T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000076', N'00000000000000000000000000000006', CAST(854661 AS Numeric(9, 0)), CAST(N'2010-04-18T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000077', N'00000000000000000000000000000006', CAST(526883 AS Numeric(9, 0)), CAST(N'2010-07-01T00:00:00.000' AS DateTime), N'10')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000078', N'00000000000000000000000000000007', CAST(17352 AS Numeric(9, 0)), CAST(N'2010-12-14T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000079', N'00000000000000000000000000000007', CAST(860093 AS Numeric(9, 0)), CAST(N'2010-08-08T00:00:00.000' AS DateTime), N'10')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000080', N'00000000000000000000000000000007', CAST(377518 AS Numeric(9, 0)), CAST(N'2010-12-26T00:00:00.000' AS DateTime), N'10')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000081', N'00000000000000000000000000000007', CAST(378575 AS Numeric(9, 0)), CAST(N'2010-07-15T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000082', N'00000000000000000000000000000007', CAST(759609 AS Numeric(9, 0)), CAST(N'2010-09-12T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000083', N'00000000000000000000000000000007', CAST(965847 AS Numeric(9, 0)), CAST(N'2010-03-14T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000084', N'00000000000000000000000000000007', CAST(259278 AS Numeric(9, 0)), CAST(N'2010-12-18T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000085', N'00000000000000000000000000000007', CAST(926372 AS Numeric(9, 0)), CAST(N'2010-04-25T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000086', N'00000000000000000000000000000007', CAST(298482 AS Numeric(9, 0)), CAST(N'2010-10-13T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000087', N'00000000000000000000000000000007', CAST(57154 AS Numeric(9, 0)), CAST(N'2010-10-26T00:00:00.000' AS DateTime), N'[:500]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000088', N'00000000000000000000000000000007', CAST(676705 AS Numeric(9, 0)), CAST(N'2010-05-29T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000089', N'00000000000000000000000000000007', CAST(307821 AS Numeric(9, 0)), CAST(N'2010-12-29T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000090', N'00000000000000000000000000000008', CAST(230345 AS Numeric(9, 0)), CAST(N'2010-07-25T00:00:00.000' AS DateTime), N'12')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000091', N'00000000000000000000000000000008', CAST(131037 AS Numeric(9, 0)), CAST(N'2010-02-15T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000092', N'00000000000000000000000000000008', CAST(633609 AS Numeric(9, 0)), CAST(N'2010-03-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000093', N'00000000000000000000000000000008', CAST(230312 AS Numeric(9, 0)), CAST(N'2010-07-24T00:00:00.000' AS DateTime), N'10')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000094', N'00000000000000000000000000000008', CAST(410658 AS Numeric(9, 0)), CAST(N'2010-11-03T00:00:00.000' AS DateTime), N'[:200]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000095', N'00000000000000000000000000000008', CAST(107479 AS Numeric(9, 0)), CAST(N'2010-04-17T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000096', N'00000000000000000000000000000008', CAST(440734 AS Numeric(9, 0)), CAST(N'2010-02-20T00:00:00.000' AS DateTime), N'10')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000097', N'00000000000000000000000000000008', CAST(607910 AS Numeric(9, 0)), CAST(N'2010-07-28T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000098', N'00000000000000000000000000000008', CAST(847574 AS Numeric(9, 0)), CAST(N'2010-04-04T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000099', N'00000000000000000000000000000008', CAST(979578 AS Numeric(9, 0)), CAST(N'2010-09-07T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000100', N'00000000000000000000000000000008', CAST(540112 AS Numeric(9, 0)), CAST(N'2010-06-24T00:00:00.000' AS DateTime), N'[:2000]')
GO
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000101', N'00000000000000000000000000000008', CAST(405503 AS Numeric(9, 0)), CAST(N'2010-09-21T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000102', N'00000000000000000000000000000008', CAST(654715 AS Numeric(9, 0)), CAST(N'2010-02-27T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000103', N'00000000000000000000000000000009', CAST(230345 AS Numeric(9, 0)), CAST(N'2021-07-25T00:00:00.000' AS DateTime), N'10')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000104', N'00000000000000000000000000000009', CAST(131037 AS Numeric(9, 0)), CAST(N'2021-02-15T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000105', N'00000000000000000000000000000009', CAST(633609 AS Numeric(9, 0)), CAST(N'2021-03-01T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000106', N'00000000000000000000000000000009', CAST(230312 AS Numeric(9, 0)), CAST(N'2021-07-24T00:00:00.000' AS DateTime), N'14')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000107', N'00000000000000000000000000000009', CAST(410658 AS Numeric(9, 0)), CAST(N'2021-11-03T00:00:00.000' AS DateTime), N'[:200]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000108', N'00000000000000000000000000000009', CAST(107479 AS Numeric(9, 0)), CAST(N'2021-04-17T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000109', N'00000000000000000000000000000009', CAST(440734 AS Numeric(9, 0)), CAST(N'2021-02-20T00:00:00.000' AS DateTime), N'9')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000110', N'00000000000000000000000000000009', CAST(607910 AS Numeric(9, 0)), CAST(N'2021-07-28T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000111', N'00000000000000000000000000000009', CAST(847574 AS Numeric(9, 0)), CAST(N'2021-04-04T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000112', N'00000000000000000000000000000009', CAST(979578 AS Numeric(9, 0)), CAST(N'2021-09-07T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000113', N'00000000000000000000000000000009', CAST(540112 AS Numeric(9, 0)), CAST(N'2021-06-24T00:00:00.000' AS DateTime), N'[:6000]')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000114', N'00000000000000000000000000000009', CAST(405503 AS Numeric(9, 0)), CAST(N'2021-09-21T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[CONTRAT] ([uid], [titulaire], [montant], [debut], [reduction]) VALUES (N'00000000000000000000000000000115', N'00000000000000000000000000000009', CAST(654715 AS Numeric(9, 0)), CAST(N'2021-02-27T00:00:00.000' AS DateTime), N'')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000001', N'Gouigoux', N'Jean-Philippe', N'Gaffeur inconnu d''origine picarde, grand amateur de spécialités gastronomiques farfelues, éternellement vêtu de polaires limées aux coudes à force de développer')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000002', N'Lagaffe', N'Gaston', N'Célèbre gaffeur d''origine belge, grand amateur de spécialités gastronomiques farfelues, éternellement vêtu d''espadrilles trouées et du même pull-over déformé')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000003', N'Dupond', N'Jean', N'Anonyme complet')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000004', N'Einstein', N'Albert', N'Icône de la mode scientifique mondiale')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000005', N'Bohr', N'Niels', N'Physicien presqu''aussi célèbre qu''Albert Einstein')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000006', N'An Tu All Ar Mor', N'N/A', N'Maison d''édition associative bretonne faisant dans le roman et les nouvelles')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000007', N'ENI', N'N/A', N'Grosse maison d''édition faisant plutôt dans l''informatique')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000008', N'Dejardin', N'Lenain', N'Personnage mythique dont la disposition dans un jardin (ou sur un bureau) est la marque moderne de l''infamie')
INSERT [dbo].[PERSONNE] ([uid], [nom], [prenom], [description]) VALUES (N'00000000000000000000000000000009', N'Mommer', N'Christophe', N'Illustre inconnu ayant humblement repris le présent ouvrage afin d''en faire une seconde édition')
ALTER TABLE [dbo].[CONTRAT] ADD  CONSTRAINT [DF_CONTRAT_montant]  DEFAULT ((0)) FOR [montant]
GO
ALTER TABLE [dbo].[CONTRAT]  WITH CHECK ADD  CONSTRAINT [FK_CONTRAT_PERSONNE] FOREIGN KEY([titulaire])
REFERENCES [dbo].[PERSONNE] ([uid])
GO
ALTER TABLE [dbo].[CONTRAT] CHECK CONSTRAINT [FK_CONTRAT_PERSONNE]
GO
USE [master]
GO
ALTER DATABASE [PROFI] SET  READ_WRITE 
GO

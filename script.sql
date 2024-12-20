USE [master]
GO
/****** Object:  Database [ITCenter]    Script Date: 12/6/2024 1:20:15 PM ******/
CREATE DATABASE [ITCenter]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ITCenter', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ITCenter.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ITCenter_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ITCenter_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ITCenter] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ITCenter].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ITCenter] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ITCenter] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ITCenter] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ITCenter] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ITCenter] SET ARITHABORT OFF 
GO
ALTER DATABASE [ITCenter] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ITCenter] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ITCenter] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ITCenter] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ITCenter] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ITCenter] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ITCenter] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ITCenter] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ITCenter] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ITCenter] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ITCenter] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ITCenter] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ITCenter] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ITCenter] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ITCenter] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ITCenter] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ITCenter] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ITCenter] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ITCenter] SET  MULTI_USER 
GO
ALTER DATABASE [ITCenter] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ITCenter] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ITCenter] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ITCenter] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ITCenter] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ITCenter] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ITCenter] SET QUERY_STORE = ON
GO
ALTER DATABASE [ITCenter] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ITCenter]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[account_id] [int] IDENTITY(1,1) NOT NULL,
	[mssv] [nvarchar](255) NULL,
	[email] [nvarchar](255) NULL,
	[username] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
	[role] [nvarchar](255) NULL,
	[hide] [bit] NULL,
	[datebegin] [datetime] NULL,
	[PasswordResetToken] [nvarchar](255) NULL,
	[TokenExpiration] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[attendance_id] [int] IDENTITY(1,1) NOT NULL,
	[diemdanh] [nvarchar](255) NULL,
	[ngaytruc] [datetime] NULL,
	[catruc] [int] NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[mssv] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[attendance_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Footer]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Footer](
	[footer_id] [int] IDENTITY(1,1) NOT NULL,
	[contentleft] [nvarchar](255) NOT NULL,
	[contentright] [nvarchar](255) NOT NULL,
	[link] [nvarchar](255) NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[order] [int] NULL,
	[datebegin] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[footer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Headers]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Headers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[websiteName] [nvarchar](255) NOT NULL,
	[logo] [nvarchar](255) NOT NULL,
	[favicon] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HumanResource]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HumanResource](
	[mssv] [nvarchar](255) NOT NULL,
	[hoten] [nvarchar](255) NULL,
	[avatar] [nvarchar](255) NULL,
	[trangthai] [nvarchar](255) NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[ngaysinh] [date] NULL,
	[facebookURL] [varchar](255) NULL,
	[githubURL] [varchar](255) NULL,
	[sodienthoai] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[mssv] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LandingContent]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandingContent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HeaderTitle] [nvarchar](200) NOT NULL,
	[HeaderSubtitle] [nvarchar](500) NOT NULL,
	[HeaderBackground] [nvarchar](200) NULL,
	[BannerImage] [nvarchar](200) NULL,
	[AboutTitle] [nvarchar](200) NOT NULL,
	[AboutContent] [nvarchar](max) NOT NULL,
	[ServicesTitle] [nvarchar](200) NULL,
	[Service1Image] [nvarchar](200) NULL,
	[Service1Title] [nvarchar](200) NULL,
	[Service2Image] [nvarchar](200) NULL,
	[Service2Title] [nvarchar](200) NULL,
	[Service3Image] [nvarchar](200) NULL,
	[Service3Title] [nvarchar](200) NULL,
	[SpotlightTitle] [nvarchar](200) NULL,
	[SpotlightSubtitle] [nvarchar](500) NULL,
	[SpotlightImages] [nvarchar](max) NULL,
	[FeatureImage] [nvarchar](200) NULL,
 CONSTRAINT [PK_LandingContent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[menu_id] [int] IDENTITY(1,1) NOT NULL,
	[tenmenu] [nvarchar](255) NOT NULL,
	[link] [nvarchar](255) NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[order] [int] NULL,
	[datebegin] [datetime] NULL,
	[icon] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[menu_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Navbar]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Navbar](
	[navbar_id] [int] IDENTITY(1,1) NOT NULL,
	[tenbutton] [nvarchar](255) NOT NULL,
	[link] [nvarchar](255) NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[order] [int] NULL,
	[datebegin] [datetime] NULL,
	[icon] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[navbar_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[news_id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](255) NOT NULL,
	[content] [nvarchar](max) NOT NULL,
	[thumbnail] [nvarchar](255) NULL,
	[link] [nvarchar](255) NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[order] [int] NULL,
	[datebegin] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[news_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[schedule_id] [int] IDENTITY(1,1) NOT NULL,
	[mssv] [nvarchar](255) NULL,
	[ngaytrongtuan] [int] NULL,
	[catruc] [int] NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[ngaydangki] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[schedule_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduleDetail]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduleDetail](
	[scheduledetail_id] [int] IDENTITY(1,1) NOT NULL,
	[catruc] [int] NULL,
	[giobatdau] [time](7) NULL,
	[gioketthuc] [time](7) NULL,
	[hide] [bit] NULL,
	[order] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[scheduledetail_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SoftwareStorage]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SoftwareStorage](
	[software_id] [int] IDENTITY(1,1) NOT NULL,
	[ten] [nvarchar](255) NULL,
	[mota] [nvarchar](255) NULL,
	[link] [nvarchar](255) NULL,
	[mssv] [nvarchar](255) NULL,
	[ngaydang] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[software_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportHistory]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportHistory](
	[support_id] [int] IDENTITY(1,1) NOT NULL,
	[hoten] [nvarchar](255) NULL,
	[maKH] [nvarchar](255) NULL,
	[tenmay] [nvarchar](255) NULL,
	[loaihotro] [nvarchar](255) NULL,
	[ngaygui] [datetime] NULL,
	[trangthai] [nvarchar](255) NULL,
	[meta] [nvarchar](max) NULL,
	[hide] [bit] NULL,
	[mssv] [nvarchar](255) NULL,
	[sdt] [nvarchar](50) NULL,
	[email] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[support_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Todo]    Script Date: 12/6/2024 1:20:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](255) NULL,
	[IsCompleted] [bit] NULL,
	[MSSV] [nvarchar](255) NULL,
	[CreatAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (1, N'52200049', N'truongnguyenhuu610@gmail.com', N'52200049', N'$2a$11$bFuN1aAzKT7O/msO3zUT2.qr5yOU7YurRUMxsPcZFJzK.gnp7rEX.', N'lead', NULL, CAST(N'2024-10-27T19:51:28.380' AS DateTime), N'', NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (2, N'52200045', N'maluong@email.com', N'52200045', N'$2a$11$O12myiZVx8.Aq0whd9dI1Ok5ZXxfU8CtxXOwkk3qMfdbG73xPo9ua', N'member', NULL, CAST(N'2024-10-27T19:54:54.527' AS DateTime), N'', NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (3, N'52300000', N'email@gmail.com', N'52300000', N'$2a$11$dIE1n9wBrKlU58BjKL9d9.TcjpqSScFP7D7/T4p4LmceGoQnHuh6i', N'member', NULL, CAST(N'2024-11-16T21:47:58.027' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (4, N'52200000', N'52200049@student.tdtu.edu.vn', N'52200000', N'$2a$11$LkJfuaHD09oh4.ZYcV7Cpu8Dr8xFnxvj6KBmrmnXKXYEkpI9bSBKm', N'admin', NULL, CAST(N'2024-11-23T10:34:56.927' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (5, N'52500000', N'email1@gmail.com', N'account1', N'$2a$11$IyWJtslDVfydv5O5FGIk3Of4aD0186TWsjF.xTAmNTaR4HYDQELOS', N'customer', NULL, CAST(N'2024-11-23T12:30:15.410' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (16, N'52200001', N'email2@gmail.com', N'52200001', N'$2a$11$kXxMi9eUNJOu6y6GTUSfT.8zm51Kq459QYZ3bNr6D8aiNQWcfAyD2', N'customer', NULL, CAST(N'2024-11-23T23:05:17.233' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (17, N'52200002', N'email3@gmail.com', N'52200002', N'$2a$11$YynootDxkL0LkCPadUhzEuB3GWKBkJFSuMHsZKrQ3n/AkTPJ3njJu', N'customer', NULL, CAST(N'2024-11-23T23:05:44.970' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (18, N'52200003', N'email0@gmail.com', N'52200003', N'$2a$11$rdBHDlGEzZT6e2SCqNB9iuiO3/gFyIQ71TrS71r5lw.YQgkc0nmya', N'customer', NULL, CAST(N'2024-11-23T23:06:05.513' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (19, N'52200004', N'email4@gmail.com', N'52200003', N'$2a$11$qhVQVvTh9G3aI0C7XFdCIOqMrDSn3qSDDUxNnK74NTAnWQlN51hcq', N'customer', NULL, CAST(N'2024-11-23T23:06:23.580' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (20, N'52200005', N'email5@gmail.com', N'52200005', N'$2a$11$0TGq3j4W0LW2Q2fpBAp7VOH.xAAvAkD/DQlwaL1u5lNK2EvgoYHZa', N'customer', NULL, CAST(N'2024-11-23T23:06:41.453' AS DateTime), NULL, NULL)
INSERT [dbo].[Account] ([account_id], [mssv], [email], [username], [password], [role], [hide], [datebegin], [PasswordResetToken], [TokenExpiration]) VALUES (21, N'52400000', N'email6@gmail.com', N'52200006', N'$2a$11$Kgc4ZjmDE2p09gX64vhcXOEbOq7pw3As6WIa3g2LDtGQD/ovyBzoW', N'lead', NULL, CAST(N'2024-11-23T23:11:25.180' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 

INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (1, N'có', CAST(N'2024-11-11T00:00:00.000' AS DateTime), 1, NULL, 1, N'52200049')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (2, N'có', CAST(N'2024-11-13T00:00:00.000' AS DateTime), 4, NULL, 1, N'52200049')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (9, N'có', CAST(N'2024-11-04T00:00:00.000' AS DateTime), 2, N'', 1, N'52200049')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (10, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200000')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (11, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200001')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (12, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200002')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (13, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200003')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (14, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200004')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (15, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200005')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (16, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200006')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (17, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200045')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (18, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52200049')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (19, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52300000')
INSERT [dbo].[Attendance] ([attendance_id], [diemdanh], [ngaytruc], [catruc], [meta], [hide], [mssv]) VALUES (20, N'có', CAST(N'2024-12-06T00:00:00.000' AS DateTime), 1, N'', 1, N'52500000')
SET IDENTITY_INSERT [dbo].[Attendance] OFF
GO
SET IDENTITY_INSERT [dbo].[Footer] ON 

INSERT [dbo].[Footer] ([footer_id], [contentleft], [contentright], [link], [meta], [hide], [order], [datebegin]) VALUES (1, N'Copyright © ITCenter 2024', N'Tìm hiểu thêm về chúng tôi', N'/ve-chung-toi', NULL, 1, 1, CAST(N'2024-12-04T13:46:29.520' AS DateTime))
SET IDENTITY_INSERT [dbo].[Footer] OFF
GO
SET IDENTITY_INSERT [dbo].[Headers] ON 

INSERT [dbo].[Headers] ([id], [websiteName], [logo], [favicon]) VALUES (1, N'IT-Center Web', N'20241205232358_ITCenterLogo.png', N'20241205232415_ITCenterLogoMini.png')
SET IDENTITY_INSERT [dbo].[Headers] OFF
GO
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200000', N'Mã Lương Khánh', N'52200000.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200001', N'Nguyễn Văn A', N'default-avatar.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200002', N'Nguyễn Văn B', N'default-avatar.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200003', N'Nguyễn Văn C', N'default-avatar.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200004', N'Nguyễn Văn D', N'default-avatar.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200005', N'Nguyễn Văn E', N'default-avatar.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200006', N'Nguyễn Văn Y', N'default-avatar.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200045', N'Nguyễn Văn F', N'default-avatar.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52200049', N'Mã Lương Khánh', N'52200049.gif', N'Đang hoạt động', NULL, 1, CAST(N'2004-11-22' AS Date), NULL, NULL, N'0913127856')
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52300000', N'Trần Văn H', N'52300000.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
INSERT [dbo].[HumanResource] ([mssv], [hoten], [avatar], [trangthai], [meta], [hide], [ngaysinh], [facebookURL], [githubURL], [sodienthoai]) VALUES (N'52500000', N'Lê Hùng E', N'52500000.jpg', N'Đang hoạt động', NULL, 1, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[LandingContent] ON 

INSERT [dbo].[LandingContent] ([Id], [HeaderTitle], [HeaderSubtitle], [HeaderBackground], [BannerImage], [AboutTitle], [AboutContent], [ServicesTitle], [Service1Image], [Service1Title], [Service2Image], [Service2Title], [Service3Image], [Service3Title], [SpotlightTitle], [SpotlightSubtitle], [SpotlightImages], [FeatureImage]) VALUES (1, N'Có tâm - Có tầm', N'Sẻ chia - Hỗ trợ', N'landing_bg.jpg', N'banner_img.png', N'Welcome to IT-Center', N'Don''t let technology slow you down. Let us help you go further.', N'Dịch Vụ', N'service1.jpg', N'Hỗ trợ cài đặt, sửa chữa phần mềm', N'service2.jpg', N'Hỗ trợ tư vấn lựa chọn Laptop', N'service3.jpg', N'Hỗ trợ phát chứng chỉ MOS', N'Đồng Hành Cùng IT-Center', N'Từ hỗ trợ kỹ thuật đến tư vấn công nghệ, IT-Center cam kết đem lại trải nghiệm tuyệt vời, giúp bạn tận dụng tối đa tiềm năng công nghệ.', N'screenshot1.jpg,screenshot2.jpg,screenshot3.jpg,screenshot4.jpg,screenshot5.jpg,screenshot6.jpg', N'feature.png')
SET IDENTITY_INSERT [dbo].[LandingContent] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (1, N'Trang chủ', N'/trang-chu', N'trang-chu', 1, 1, CAST(N'2024-11-25T20:59:37.867' AS DateTime), N'mdi mdi-speedometer')
INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (2, N'Điểm danh ca trực', N'/diem-danh-ca-truc', N'diem-danh-ca-truc', 1, 2, CAST(N'2024-11-24T17:19:41.900' AS DateTime), N'mdi mdi-table-large')
INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (3, N'Đăng ký ca trực', N'/dang-ky-ca-truc', N'dang-ky-ca-truc', 1, 3, CAST(N'2024-11-24T17:19:47.683' AS DateTime), N'mdi mdi-timetable')
INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (4, N'Tiếp nhận hỗ trợ', N'/tiep-nhan-ho-tro', N'tiep-nhan-ho-tro', 1, 4, CAST(N'2024-11-24T17:19:53.587' AS DateTime), N'mdi mdi-laptop')
INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (5, N'Kho phần mềm', N'/kho-phan-mem', N'kho-phan-mem', 1, 5, CAST(N'2024-11-24T17:19:59.497' AS DateTime), N'mdi mdi-file-multiple')
INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (6, N'Thành tích', N'/thanh-tich', N'thanh-tich', 1, 6, CAST(N'2024-11-24T17:20:04.313' AS DateTime), N'mdi mdi-rocket')
INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (7, N'Về trang web', N'/ve-trang-web', N've-trang-web', 1, 7, CAST(N'2024-11-24T17:20:09.010' AS DateTime), N'mdi mdi-alert-circle-outline')
INSERT [dbo].[Menu] ([menu_id], [tenmenu], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (8, N'Về chúng tôi', N'/ve-chung-toi', N've-chung-toi', 1, 8, CAST(N'2024-11-24T17:20:15.223' AS DateTime), N'mdi mdi-account-search')
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[Navbar] ON 

INSERT [dbo].[Navbar] ([navbar_id], [tenbutton], [link], [meta], [hide], [order], [datebegin], [icon]) VALUES (1, N'+ Tạo mới yêu cầu', NULL, NULL, 1, 1, NULL, N'logo-mini.svg')
SET IDENTITY_INSERT [dbo].[Navbar] OFF
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (1, N'Tuyển thành viên Gen 8!', N'<p><img alt="" src="/Content/upload/img/455967200_122167597706188858_1133607241944634137_n.jpg" style="height:451px; width:720px" />Ch&agrave;o mừng c&aacute;c t&acirc;n kỹ thuật vi&ecirc;n</p>
', N'screenshot3.jpg', NULL, NULL, 1, 1, CAST(N'2024-12-05T14:27:07.840' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (2, N'Teambuilding Gen 8!', N'<p style="text-align:center"><em><strong>Những điều chuẩn bị cho buổi teambuilding</strong></em></p>

<p style="text-align:center">&nbsp;</p>

<p><img alt="" src="/Content/upload/img/467311697_598825006000253_7802023651906371762_n.jpg" style="float:left; height:283px; width:200px" />Lorem ipsum dolor sit amet. Est dolores repudiandae ex architecto libero eos pariatur quas ut nulla ipsam ut repudiandae exercitationem cum impedit velit. Qui omnis quia sit eveniet repellendus et voluptates tenetur qui omnis consequatur. In sunt consequuntur ut debitis tempore id suscipit dolorum! Est culpa molestiae ad necessitatibus explicabo At iure mollitia quo odio dicta At voluptas ullam eos quidem animi.</p>

<p>Qui suscipit quaerat ea repudiandae porro et soluta voluptatum. Et tempore nostrum et temporibus eligendi At quibusdam maiores qui quisquam totam ad galisum iusto a nobis similique. Est laudantium tempore in repudiandae dolores sed accusantium accusantium ut quis fuga aut quia laudantium. Et quia excepturi sit enim deleniti et quis cumque.</p>

<p>Sit amet aperiam est maxime aspernatur et alias voluptates et quas iure. Sed totam nihil et enim quas aut quam porro?</p>

<p>&nbsp;</p>
', N'455967200_122167597706188858_1133607241944634137_n.jpg', NULL, NULL, 1, 3, CAST(N'2024-11-25T17:59:36.397' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (3, N'Tin Tức 1', N'Nội dung tin tức 1...', N'dashboard/img_6.jpg', NULL, NULL, 0, 1, CAST(N'2024-10-01T08:00:00.000' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (4, N'Tin Tức 2', N'Nội dung tin tức 2...', N'banner_img.png', NULL, NULL, 0, 2, CAST(N'2024-12-05T14:47:18.883' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (5, N'Tin Tức 3', N'Nội dung tin tức 3...', N'screenshot5.jpg', NULL, NULL, 1, 3, CAST(N'2024-12-05T14:32:58.850' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (6, N'Tin Tức 4', N'Nội dung tin tức 4...', N'header_bg_638689276319146331.jpg', NULL, NULL, 0, 4, CAST(N'2024-12-05T14:28:08.360' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (7, N'Tin Tức 5', N'<p><img alt="" src="/Content/upload/img/85984123_p0.jpg" style="float:left; height:198px; width:700px" />Nội dung tin tức 5...</p>
', N'screenshot_bg.jpg', NULL, NULL, 0, 5, CAST(N'2024-12-05T14:18:00.750' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (9, N'Test 2', N'<p><s>hello</s></p>
', N'455967200_122167597706188858_1133607241944634137_n.jpg', NULL, NULL, 1, 1, CAST(N'2024-11-25T16:13:07.500' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (10, N'Test 3', N'<ol>
	<li><em>Lorem</em></li>
	<li>Vũ trụ trong vỏ hạt dẻ</li>
</ol>

<p>&nbsp;</p>
', N'85984123_p0.jpg', NULL, NULL, 1, 2, CAST(N'2024-11-25T17:58:54.740' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (11, N'Thông báo lịch nghỉ', N'<p><img alt="" src="/Content/upload/img/432764104_934569764793950_1145498975879103581_n.png" style="height:191px; width:400px" />Lorem</p>
', N'Picture1.png', NULL, NULL, 1, 1, CAST(N'2024-11-26T14:07:11.333' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (12, N'Thông báo tổ chức sinh nhật ITCenter', N'<p>Th&ocirc;ng b&aacute;o lịch tổ chức sinh nhật!</p>
', N'spotlight_638689280447561704.jpg', NULL, NULL, 1, 2, CAST(N'2024-12-05T14:33:35.517' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (13, N'Thông báo họp T11', N'<p>Họp CLB T11</p>
', N'header_bg_638689276319146331.jpg', NULL, NULL, 1, 10, CAST(N'2024-12-05T15:23:26.480' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (14, N'Thông báo họp T12', N'<p>Họp CLB T 12</p>
', N'header_bg_638689276319146331.jpg', NULL, NULL, 1, 9, CAST(N'2024-12-05T15:29:43.280' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (15, N'Thông báo họp T1', N'<p>Họp CLB T1</p>
', N'screenshot_bg.jpg', NULL, NULL, 1, 8, CAST(N'2024-12-05T15:32:37.423' AS DateTime))
INSERT [dbo].[News] ([news_id], [ten], [content], [thumbnail], [link], [meta], [hide], [order], [datebegin]) VALUES (17, N'Thông báo họp T2', N'<p>Họp CLB T2</p>
', N'service2.jpg', NULL, NULL, 0, 7, CAST(N'2024-12-05T20:14:55.507' AS DateTime))
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (1, N'52200049', 2, 1, NULL, 1, CAST(N'2024-09-09T00:00:00.000' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (2, N'52200049', 6, 2, NULL, 1, CAST(N'2024-09-09T00:00:00.000' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (11, N'52200049', 4, 2, NULL, 1, CAST(N'2024-11-04T23:31:59.890' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (12, N'52200049', 2, 1, NULL, 1, CAST(N'2024-11-05T00:07:54.920' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (13, N'52200049', 2, 1, NULL, 0, CAST(N'2024-11-05T00:08:40.293' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (14, N'52200049', 4, 3, NULL, 1, CAST(N'2024-11-05T00:14:50.133' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (15, N'52200049', 2, 1, NULL, 0, CAST(N'2024-11-05T00:16:16.977' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (16, N'52200000', 2, 1, NULL, 0, CAST(N'2024-11-23T15:50:37.027' AS DateTime))
INSERT [dbo].[Schedule] ([schedule_id], [mssv], [ngaytrongtuan], [catruc], [meta], [hide], [ngaydangki]) VALUES (17, N'52200000', 2, 3, NULL, 1, CAST(N'2024-11-23T15:50:47.653' AS DateTime))
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
SET IDENTITY_INSERT [dbo].[ScheduleDetail] ON 

INSERT [dbo].[ScheduleDetail] ([scheduledetail_id], [catruc], [giobatdau], [gioketthuc], [hide], [order]) VALUES (1, 1, CAST(N'07:30:00' AS Time), CAST(N'09:30:00' AS Time), 1, 1)
INSERT [dbo].[ScheduleDetail] ([scheduledetail_id], [catruc], [giobatdau], [gioketthuc], [hide], [order]) VALUES (2, 2, CAST(N'09:30:00' AS Time), CAST(N'11:30:00' AS Time), 1, 2)
INSERT [dbo].[ScheduleDetail] ([scheduledetail_id], [catruc], [giobatdau], [gioketthuc], [hide], [order]) VALUES (3, 3, CAST(N'13:30:00' AS Time), CAST(N'15:30:00' AS Time), 1, 3)
INSERT [dbo].[ScheduleDetail] ([scheduledetail_id], [catruc], [giobatdau], [gioketthuc], [hide], [order]) VALUES (4, 4, CAST(N'15:30:00' AS Time), CAST(N'17:00:00' AS Time), 1, 4)
SET IDENTITY_INSERT [dbo].[ScheduleDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[SoftwareStorage] ON 

INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (1, N'ActiveOfficeCMD', N'kích hoạt bản quyền Office thông qua CMD', N'http://example.com', N'52200049', CAST(N'2024-10-21T00:00:00.000' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (2, N'Visual Studio Code', N'Editor for code development', N'https://code.visualstudio.com', N'52200000', CAST(N'2024-10-01T09:00:00.000' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (3, N'Postman', N'Tool for API testing', N'https://www.postman.com', N'52200000', CAST(N'2024-10-02T14:30:00.000' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (4, N'Docker', N'Containerization platform', N'https://www.docker.com', N'52200000', CAST(N'2024-10-03T10:15:00.000' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (5, N'Git', N'Version control system', N'https://git-scm.com', N'52200000', CAST(N'2024-10-04T11:45:00.000' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (6, N'Figma', N'Design and prototyping tool', N'https://www.figma.com', N'52200000', CAST(N'2024-10-05T08:30:00.000' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (7, N'Test', N'test1', N'url', N'52200049', CAST(N'2024-11-06T10:44:23.210' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (8, N'Test1', N'test2', N'123', N'52200049', CAST(N'2024-11-06T10:44:55.550' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (9, N'Test2', N'test2', N'url', N'52200049', CAST(N'2024-11-06T10:46:03.850' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (11, N'Test5', N'test5', N'123', N'52200049', CAST(N'2024-11-06T23:22:40.423' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (13, N'Test4', N'test4', N'123', N'52200049', CAST(N'2024-11-08T17:02:53.127' AS DateTime))
INSERT [dbo].[SoftwareStorage] ([software_id], [ten], [mota], [link], [mssv], [ngaydang]) VALUES (14, N'Test123', N'test123', N'https://example.com/', N'52200000', CAST(N'2024-11-23T15:20:24.387' AS DateTime))
SET IDENTITY_INSERT [dbo].[SoftwareStorage] OFF
GO
SET IDENTITY_INSERT [dbo].[SupportHistory] ON 

INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (1, N'Nguyễn Văn B', N'52000078', N'Asus', N'Cài win', CAST(N'2024-11-11T00:00:00.000' AS DateTime), N'bàn giao máy', NULL, 1, N'52200049', N'0123456789', N'email@gmail.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (2, N'Nguyễn Thị B', N'52100123', N'HP', N'Cài matlab', CAST(N'2024-10-21T00:00:00.000' AS DateTime), N'bàn giao máy', NULL, 1, N'52200049', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (3, N'Trần Văn C', N'52200098', N'Acer', N'Cài Rstudio', CAST(N'2024-10-22T00:00:00.000' AS DateTime), N'bàn giao máy', NULL, 1, N'52200049', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (4, N'Nguyen Van A', N'52200099', N'Laptop HP', N'Cài office 16', CAST(N'2024-10-01T09:30:00.000' AS DateTime), N'chờ', NULL, 0, N'52200000', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (5, N'Tran Thi B', N'52200123', N'Desktop Dell', N'Cài office 365', CAST(N'2024-10-01T10:00:00.000' AS DateTime), N'tiếp nhận', NULL, 1, N'52200000', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (6, N'Tran Thi C', N'52200453', N'MacBook Pro', N'Cài office 16', CAST(N'2024-10-02T14:00:00.000' AS DateTime), N'bàn giao máy', NULL, 0, N'52200000', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (7, N'Le Van D', N'522000198', N'PC Lenovo', N'Cài win', CAST(N'2024-10-03T11:00:00.000' AS DateTime), N'từ chối', NULL, 1, N'52200000', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (8, N'Pham Thi E', N'52200094', N'Laptop Asus', N'Cài Matlab', CAST(N'2024-10-04T15:00:00.000' AS DateTime), N'tiếp nhận', NULL, 0, N'52200000', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (10, N'Mã Lương', N'123456', N'Dynabook', N'Cài win', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'từ chối', NULL, 0, N'52200049', N'0912345678', N'email@example.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (11, N'Mã Lương', N'123456789', N'Dell', N'Cài win', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'bàn giao máy', NULL, 1, N'52200049', N'0123456789', N'email@gmail.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (12, N'Mã Lương', N'52200011', N'Dynabook', N'Cài Photoshop', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'bàn giao máy', NULL, 1, N'52200049', N'0123456789', N'maluong@email.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (13, N'Mã Lương', N'52200011', N'Dynabook', N'Cài Photoshop', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'tiếp nhận', NULL, 1, N'52200049', N'0123456789', N'maluong@email.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (14, N'Mã Lương', N'52200011', N'Asus', N'Hack FB', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'bàn giao máy', NULL, 1, N'52300000', N'0123456789', N'maluong@email.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (15, N'Mã Lương', N'52200011', N'Macbook', N'Cài ubuntu', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'chờ', NULL, 1, NULL, N'0123456789', N'maluong@email.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (16, N'Mã Lương', N'52200011', N'Asus', N'Cài Docker', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'chờ', NULL, 1, NULL, N'0123456789', N'truongnguyenhuu610@gmail.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (17, N'Mã Lương', N'52200011', N'Dell', N'Cài office', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'chờ', NULL, 1, NULL, N'0123456789', N'maluong@email.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (18, N'Mã Lương', N'52200011', N'HP', N'Cài Photoshop', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'từ chối', NULL, 1, N'52200049', N'0123456789', N'maluong@email.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (19, N'Mã Lương', N'52200011', N'Lenovo', N'Cài AutoCAD', CAST(N'2024-10-29T00:00:00.000' AS DateTime), N'chờ', NULL, 0, NULL, N'0123456789', N'maluong@email.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (20, N'Mã Lương Khánh', N'12345', N'ABC', N'Test1', CAST(N'2024-11-05T00:00:00.000' AS DateTime), N'tiếp nhận', NULL, 1, NULL, N'0123456789', N'email@gmail.com')
INSERT [dbo].[SupportHistory] ([support_id], [hoten], [maKH], [tenmay], [loaihotro], [ngaygui], [trangthai], [meta], [hide], [mssv], [sdt], [email]) VALUES (21, N'Lê Tùng Dương', N'52200044', N'Dell', N'Cài win', CAST(N'2024-11-23T00:00:00.000' AS DateTime), N'bàn giao máy', NULL, 1, N'52200000', N'0123456789', N'email@gmail.com')
SET IDENTITY_INSERT [dbo].[SupportHistory] OFF
GO
SET IDENTITY_INSERT [dbo].[Todo] ON 

INSERT [dbo].[Todo] ([Id], [Content], [IsCompleted], [MSSV], [CreatAt]) VALUES (2, N'test 1', 0, N'52200049', CAST(N'2024-11-08T17:35:02.167' AS DateTime))
INSERT [dbo].[Todo] ([Id], [Content], [IsCompleted], [MSSV], [CreatAt]) VALUES (3, N'test 2', 0, N'52200049', CAST(N'2024-11-08T17:35:24.973' AS DateTime))
SET IDENTITY_INSERT [dbo].[Todo] OFF
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_HumanResource] FOREIGN KEY([mssv])
REFERENCES [dbo].[HumanResource] ([mssv])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_HumanResource]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_HumanResource] FOREIGN KEY([mssv])
REFERENCES [dbo].[HumanResource] ([mssv])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_HumanResource]
GO
ALTER TABLE [dbo].[SoftwareStorage]  WITH CHECK ADD  CONSTRAINT [FK_SoftwareStorage_HumanResource] FOREIGN KEY([mssv])
REFERENCES [dbo].[HumanResource] ([mssv])
GO
ALTER TABLE [dbo].[SoftwareStorage] CHECK CONSTRAINT [FK_SoftwareStorage_HumanResource]
GO
ALTER TABLE [dbo].[SupportHistory]  WITH CHECK ADD  CONSTRAINT [FK_SupportHistory_HumanResource] FOREIGN KEY([mssv])
REFERENCES [dbo].[HumanResource] ([mssv])
GO
ALTER TABLE [dbo].[SupportHistory] CHECK CONSTRAINT [FK_SupportHistory_HumanResource]
GO
USE [master]
GO
ALTER DATABASE [ITCenter] SET  READ_WRITE 
GO

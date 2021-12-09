USE [master]
GO
/****** Object:  Database [VentaReal]    Script Date: 9/12/2021 1:35:51 p. m. ******/
CREATE DATABASE [VentaReal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'VentaReal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VentaReal.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'VentaReal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\VentaReal_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [VentaReal] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [VentaReal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [VentaReal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [VentaReal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [VentaReal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [VentaReal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [VentaReal] SET ARITHABORT OFF 
GO
ALTER DATABASE [VentaReal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [VentaReal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [VentaReal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [VentaReal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [VentaReal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [VentaReal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [VentaReal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [VentaReal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [VentaReal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [VentaReal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [VentaReal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [VentaReal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [VentaReal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [VentaReal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [VentaReal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [VentaReal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [VentaReal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [VentaReal] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [VentaReal] SET  MULTI_USER 
GO
ALTER DATABASE [VentaReal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [VentaReal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [VentaReal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [VentaReal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [VentaReal] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [VentaReal] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [VentaReal] SET QUERY_STORE = OFF
GO
USE [VentaReal]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 9/12/2021 1:35:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Concepto]    Script Date: 9/12/2021 1:35:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Concepto](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[id_venta] [bigint] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precioUnitario] [decimal](16, 2) NOT NULL,
	[importe] [decimal](16, 2) NOT NULL,
	[id_producto] [int] NOT NULL,
 CONSTRAINT [PK_Concepto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 9/12/2021 1:35:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[precioUnitario] [decimal](16, 2) NOT NULL,
	[costo] [decimal](16, 2) NOT NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Venta]    Script Date: 9/12/2021 1:35:51 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venta](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[id_cliente] [int] NULL,
	[total] [decimal](16, 2) NULL,
 CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Concepto]  WITH CHECK ADD  CONSTRAINT [FK_Concepto_Producto] FOREIGN KEY([id_producto])
REFERENCES [dbo].[Producto] ([id])
GO
ALTER TABLE [dbo].[Concepto] CHECK CONSTRAINT [FK_Concepto_Producto]
GO
ALTER TABLE [dbo].[Concepto]  WITH CHECK ADD  CONSTRAINT [FK_Concepto_Venta] FOREIGN KEY([id_venta])
REFERENCES [dbo].[Venta] ([id])
GO
ALTER TABLE [dbo].[Concepto] CHECK CONSTRAINT [FK_Concepto_Venta]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Cliente] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Cliente]
GO
USE [master]
GO
ALTER DATABASE [VentaReal] SET  READ_WRITE 
GO

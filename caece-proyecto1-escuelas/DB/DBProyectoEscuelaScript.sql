USE [master]
GO
/****** Object:  Database [EscuelaD]    Script Date: 04/06/2014 20:29:50 ******/
CREATE DATABASE [EscuelaD] ON  PRIMARY 
( NAME = N'EscuelaD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\EscuelaD.mdf' , SIZE = 2048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EscuelaD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\EscuelaD_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [EscuelaD] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EscuelaD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EscuelaD] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [EscuelaD] SET ANSI_NULLS OFF
GO
ALTER DATABASE [EscuelaD] SET ANSI_PADDING OFF
GO
ALTER DATABASE [EscuelaD] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [EscuelaD] SET ARITHABORT OFF
GO
ALTER DATABASE [EscuelaD] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [EscuelaD] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [EscuelaD] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [EscuelaD] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [EscuelaD] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [EscuelaD] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [EscuelaD] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [EscuelaD] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [EscuelaD] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [EscuelaD] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [EscuelaD] SET  DISABLE_BROKER
GO
ALTER DATABASE [EscuelaD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [EscuelaD] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [EscuelaD] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [EscuelaD] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [EscuelaD] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [EscuelaD] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [EscuelaD] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [EscuelaD] SET  READ_WRITE
GO
ALTER DATABASE [EscuelaD] SET RECOVERY FULL
GO
ALTER DATABASE [EscuelaD] SET  MULTI_USER
GO
ALTER DATABASE [EscuelaD] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [EscuelaD] SET DB_CHAINING OFF
GO
USE [EscuelaD]
GO
/****** Object:  User [escuelaD_dbo]    Script Date: 04/06/2014 20:29:50 ******/
CREATE USER [escuelaD_dbo] FOR LOGIN [escuelaD_dbo] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categoria](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Distrito]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Distrito](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Localidad] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CategoriaValor]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CategoriaValor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_CategoriaValor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[RolId] [int] NOT NULL,
	[Direccion] [varchar](100) NULL,
	[DistritoId] [int] NOT NULL,
	[Telefono] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Escuela]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Escuela](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TipoEstablecimientoId] [int] NOT NULL,
	[Direccion] [varchar](100) NULL,
	[DistritoId] [int] NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NULL,
	[Numero] [int] NULL,
	[Nombre] [varchar](100) NULL,
 CONSTRAINT [PK_Escuela] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Relevamiento]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relevamiento](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EscuelaId] [int] NOT NULL,
	[CantMaquinas] [int] NOT NULL,
	[tieneADM] [bit] NOT NULL,
	[Comentarios] [text] NULL,
	[FechaRelevo] [datetime] NOT NULL,
	[FechaModificacion] [datetime] NULL,
 CONSTRAINT [PK_Relevamiento] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personal]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personal](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EscuelaId] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[DNI] [varchar](10) NULL,
	[Direccion] [varchar](100) NULL,
	[DistritoId] [int] NOT NULL,
	[Cargo] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_Personal] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Servicio](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[TipoServicioId] [int] NOT NULL,
	[EsPago] [bit] NOT NULL,
	[RelevamientoId] [int] NOT NULL,
 CONSTRAINT [PK_Servicio] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Maquina]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Maquina](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [varchar](50) NOT NULL,
	[Procesador] [varchar](50) NOT NULL,
	[MemoriaRAM] [int] NOT NULL,
	[CapacidadDiscoDuro] [float] NOT NULL,
	[SistemaOperativo] [varchar](50) NOT NULL,
	[DispositivoSonido] [varchar](50) NULL,
	[TieneMicrofono] [bit] NOT NULL,
	[Comentarios] [text] NULL,
	[RelevamientoId] [int] NOT NULL,
	[PlacaVideo] [varchar](100) NULL,
	[PerteneceARed] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Dispositivo]    Script Date: 04/06/2014 20:29:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Dispositivo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [varchar](50) NOT NULL,
	[Comentarios] [text] NULL,
	[RelevamientoId] [int] NOT NULL,
	[TipoDispositivoId] [int] NOT NULL,
 CONSTRAINT [PK_Dispositivo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_CategoriaValor_Categoria]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[CategoriaValor]  WITH CHECK ADD  CONSTRAINT [FK_CategoriaValor_Categoria] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categoria] ([ID])
GO
ALTER TABLE [dbo].[CategoriaValor] CHECK CONSTRAINT [FK_CategoriaValor_Categoria]
GO
/****** Object:  ForeignKey [FK_Usuario_Localidad]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Localidad] FOREIGN KEY([DistritoId])
REFERENCES [dbo].[Distrito] ([ID])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Localidad]
GO
/****** Object:  ForeignKey [FK_Usuario_Rol]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rol] ([ID])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Rol]
GO
/****** Object:  ForeignKey [FK_Escuela_CategoriaValor]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Escuela]  WITH CHECK ADD  CONSTRAINT [FK_Escuela_CategoriaValor] FOREIGN KEY([TipoEstablecimientoId])
REFERENCES [dbo].[CategoriaValor] ([ID])
GO
ALTER TABLE [dbo].[Escuela] CHECK CONSTRAINT [FK_Escuela_CategoriaValor]
GO
/****** Object:  ForeignKey [FK_Escuela_Localidad]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Escuela]  WITH CHECK ADD  CONSTRAINT [FK_Escuela_Localidad] FOREIGN KEY([DistritoId])
REFERENCES [dbo].[Distrito] ([ID])
GO
ALTER TABLE [dbo].[Escuela] CHECK CONSTRAINT [FK_Escuela_Localidad]
GO
/****** Object:  ForeignKey [FK_Relevamiento_Escuela]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Relevamiento]  WITH CHECK ADD  CONSTRAINT [FK_Relevamiento_Escuela] FOREIGN KEY([EscuelaId])
REFERENCES [dbo].[Escuela] ([ID])
GO
ALTER TABLE [dbo].[Relevamiento] CHECK CONSTRAINT [FK_Relevamiento_Escuela]
GO
/****** Object:  ForeignKey [FK_Personal_Escuela]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Personal]  WITH CHECK ADD  CONSTRAINT [FK_Personal_Escuela] FOREIGN KEY([EscuelaId])
REFERENCES [dbo].[Escuela] ([ID])
GO
ALTER TABLE [dbo].[Personal] CHECK CONSTRAINT [FK_Personal_Escuela]
GO
/****** Object:  ForeignKey [FK_Personal_Localidad]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Personal]  WITH CHECK ADD  CONSTRAINT [FK_Personal_Localidad] FOREIGN KEY([DistritoId])
REFERENCES [dbo].[Distrito] ([ID])
GO
ALTER TABLE [dbo].[Personal] CHECK CONSTRAINT [FK_Personal_Localidad]
GO
/****** Object:  ForeignKey [FK_Servicio_CategoriaValor]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_CategoriaValor] FOREIGN KEY([TipoServicioId])
REFERENCES [dbo].[CategoriaValor] ([ID])
GO
ALTER TABLE [dbo].[Servicio] CHECK CONSTRAINT [FK_Servicio_CategoriaValor]
GO
/****** Object:  ForeignKey [FK_Servicio_Relevamiento]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD  CONSTRAINT [FK_Servicio_Relevamiento] FOREIGN KEY([RelevamientoId])
REFERENCES [dbo].[Relevamiento] ([ID])
GO
ALTER TABLE [dbo].[Servicio] CHECK CONSTRAINT [FK_Servicio_Relevamiento]
GO
/****** Object:  ForeignKey [FK_Maquina_Relevamiento]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Maquina]  WITH CHECK ADD  CONSTRAINT [FK_Maquina_Relevamiento] FOREIGN KEY([RelevamientoId])
REFERENCES [dbo].[Relevamiento] ([ID])
GO
ALTER TABLE [dbo].[Maquina] CHECK CONSTRAINT [FK_Maquina_Relevamiento]
GO
/****** Object:  ForeignKey [FK_Dispositivo_CategoriaValor]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Dispositivo]  WITH CHECK ADD  CONSTRAINT [FK_Dispositivo_CategoriaValor] FOREIGN KEY([TipoDispositivoId])
REFERENCES [dbo].[CategoriaValor] ([ID])
GO
ALTER TABLE [dbo].[Dispositivo] CHECK CONSTRAINT [FK_Dispositivo_CategoriaValor]
GO
/****** Object:  ForeignKey [FK_Dispositivo_Relevamiento]    Script Date: 04/06/2014 20:29:52 ******/
ALTER TABLE [dbo].[Dispositivo]  WITH CHECK ADD  CONSTRAINT [FK_Dispositivo_Relevamiento] FOREIGN KEY([RelevamientoId])
REFERENCES [dbo].[Relevamiento] ([ID])
GO
ALTER TABLE [dbo].[Dispositivo] CHECK CONSTRAINT [FK_Dispositivo_Relevamiento]
GO

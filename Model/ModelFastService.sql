--drop table Venta
--drop table Cliente
--drop table TipoTransaccion
--drop table Compra
--drop table Factura
--drop table ItemMenu
--drop table Logs
--drop table MetodoPago
--drop table Pago
--drop table Proveedor
--drop table PuntoDeVenta
--drop table Role
--drop table RoleMenu
--drop table Usuario
--drop table UsuarioRol


GO
USE [FastService]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [int] NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Apellido] [varchar](200) NOT NULL,
	[Mail] [varchar](200) NULL,
	[Telefono1] [varchar](20) NULL,
	[Telefono2] [varchar](20) NULL,
	[Direccion] [varchar](300) NULL,
 CONSTRAINT [pk_ClienteId] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[TipoTransaccion](
	[TipoTransaccionId] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL
 CONSTRAINT [pk_TipoTransaccionId] PRIMARY KEY CLUSTERED 
(
	[TipoTransaccionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Compra]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Compra](
	[CompraId] [int] IDENTITY(1,1) NOT NULL,
	[ProveedorId] varchar(100) NOT NULL,
	[Monto] [money] NOT NULL,
	[Descripcion] varchar(500) NULL,
	[PuntoDeVentaId] [int] NOT NULL,
	[Estado] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[CreadoPor] int NOT NULL
	CONSTRAINT [pk_CompraId] PRIMARY KEY CLUSTERED 
(
	[CompraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Factura](
	[FacturaId] [int] IDENTITY(1,1) NOT NULL,
	[TipoFactura] [int] NOT NULL,
	[Media] [varbinary](max) NULL,
 CONSTRAINT [pk_FacturaId] PRIMARY KEY CLUSTERED 
(
	[FacturaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ItemMenu]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ItemMenu](
	[ItemMenuId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Orden] [int] NOT NULL,
	[Estado] [bit] NOT NULL,
	[ItemMenuPadreId] [int] NULL,
	[Controlador] [varchar](50) NULL,
	[Accion] [varchar](50) NULL,
	[Parametros] [varchar](500) NULL,
	[Icon] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ItemMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventDateTime] [datetime] NOT NULL,
	[EventLevel] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[MachineName] [nvarchar](100) NOT NULL,
	[EventMessage] [nvarchar](max) NOT NULL,
	[ErrorSource] [nvarchar](100) NULL,
	[ErrorClass] [nvarchar](500) NULL,
	[ErrorMethod] [nvarchar](max) NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[InnerErrorMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MetodoPago]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[MetodoPago](
	[MetodoPagoId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](300) NULL,
	[Descripcion] [varchar](300) NULL,
 CONSTRAINT [pk_MetodoPagoId] PRIMARY KEY CLUSTERED 
(
	[MetodoPagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pago](
	[PagoId] [int] IDENTITY(1,1) NOT NULL,
	[CompraId] [int] NOT NULL,
	[Monto] [money] NOT NULL,
	[FechaDebito] [datetime] NOT NULL,
	[FechaEmision] [datetime] NOT NULL,
	[NroReferencia] [varchar](100) NULL,
	[Motivo] [varchar](500) NULL,
	[TipoTransaccionId] int NOT NULL,
	[FacturaId] int NULL,
	[CreadoPor] int NULL,
	[FechaCreacion] datetime NOT NULL,
	[MetodoDePagoId] int NOT NULL
 CONSTRAINT [pk_PagoId] PRIMARY KEY CLUSTERED 
(
	[PagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proveedor]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proveedor](
	[ProveedorId] varchar(100) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Mail] [varchar](200) NULL,
	[Telefono1] [varchar](20) NULL,
	[Telefono2] [varchar](20) NULL,
	[Direccion] [varchar](300) NULL,
 CONSTRAINT [pk_ProveedorId] PRIMARY KEY CLUSTERED 
(
	[ProveedorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PuntoDeVenta]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PuntoDeVenta](
	[PuntoDeVentaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
 CONSTRAINT [pk_PuntoDeVentaId] PRIMARY KEY CLUSTERED 
(
	[PuntoDeVentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[RolId] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](200) NOT NULL,
	[DefaultController] [varchar](50) NULL,
	[DefaultAction] [varchar](50) NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RoleMenu]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RolId] [int] NOT NULL,
	[ItemMenuId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[Contraseña] [varchar](200) NOT NULL,
	[Direccion] [varchar](500) NOT NULL,
	[Telefono1] [varchar](50) NOT NULL,
	[Telefono2] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UsuarioRol]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioRol](
	[RolId] [int] NOT NULL,
	[UserId] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Venta]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Venta](
	[VentaId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NULL,
	[Monto] [money] NOT NULL,
	[Descripcion] varchar(500),
	[FacturaId] int NULL,
	[RefNumber] varchar(200) NULL,
	[PuntoDeVentaId] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Vendedor] int NOT NULL,
	[MetodoPagoId] [int] NULL,
	[TipoTransaccionId] int NOT NULL,
 CONSTRAINT [pk_VentaId] PRIMARY KEY CLUSTERED 
(
	[VentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Cliente] ([ClienteId], [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (31123458, N'Matias', N'Gonzalez', N'lopezleandro03@gmail.com', N'1169411665', NULL, N'Larrea 1285 1A, Recoleta, CABA')
GO
INSERT [dbo].[Cliente] ([ClienteId], [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (36212345, N'Juan', N'Perez', NULL, N'1169411665', N'', N'Larrea 1285 1A, Recoleta, CABA')
GO
INSERT [dbo].[Cliente] ([ClienteId], [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (36234123, N'Pablo', N'Hormus', NULL, N'1169411665', N'', N'Larrea 1285 1A, Recoleta, CABA')
GO
INSERT [dbo].[Cliente] ([ClienteId], [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (36285548, N'Leandro', N'Lopez', N'lopezleandro03@gmail.com', N'1143232000', N'1143232000', N'Calle 1328 Núnero 2801')
GO

GO
SET IDENTITY_INSERT [dbo].[ItemMenu] ON 

GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (1, N'Proveedores', 10, 1, NULL, NULL, NULL, NULL, N'fa fa-truck')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (2, N'Ventas', 20, 1, NULL, NULL, NULL, NULL, N'fa fa-usd')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (3, N'Nueva Venta', 20, 1, 2, N'Venta', N'Create', NULL, N'fa fa-plus-square')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (4, N'Movimientos', 10, 1, 2, N'Venta', N'Index', NULL, N'fa fa-list')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (5, N'Nueva Compra', 20, 1, 1, N'Compra', N'Create', NULL, N'fa fa-plus-square')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (6, N'Movimientos', 10, 1, 1, N'Compra', N'Index', NULL, N'fa fa-list')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (7, N'MercadoPago', 30, 1, NULL, NULL, NULL, NULL, N'fa fa-cc-mastercard')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (8, N'Nuevo Pago', 10, 1, 7, N'Payment', N'Create', NULL, N'fa fa-plus-square')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (9, N'Nuevo Pago a Proveedor', 30, 1, 1, N'PagoProveedores', N'Create', NULL, N'fa fa-plus-square')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (1009, N'Reportes', 40, 1, NULL, NULL, NULL, NULL, N'fa fa-bar-chart')
GO
SET IDENTITY_INSERT [dbo].[ItemMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[Logs] ON 

GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (1, CAST(N'2017-05-01 22:46:46.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (2, CAST(N'2017-05-01 22:56:53.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Create.cshtml(60): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''MetodoDePago'' ni se encontró ningún método de extensión ''MetodoDePago'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (3, CAST(N'2017-05-01 22:57:00.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Create.cshtml(60): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''MetodoDePago'' ni se encontró ningún método de extensión ''MetodoDePago'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (4, CAST(N'2017-05-01 22:57:04.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Create.cshtml(60): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''MetodoDePago'' ni se encontró ningún método de extensión ''MetodoDePago'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (5, CAST(N'2017-05-01 22:57:26.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Create.cshtml(60): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''MetodoDePago'' ni se encontró ningún método de extensión ''MetodoDePago'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (6, CAST(N'2017-05-01 22:57:27.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Create.cshtml(60): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''MetodoDePago'' ni se encontró ningún método de extensión ''MetodoDePago'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (7, CAST(N'2017-05-01 22:57:40.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'The partial view ''Index'' was not found or no view engine supports the searched locations. The following locations were searched:
~/Views/Compra/Index.aspx
~/Views/Compra/Index.ascx
~/Views/Shared/Index.aspx
~/Views/Shared/Index.ascx
~/Views/Compra/Index.cshtml
~/Views/Compra/Index.vbhtml
~/Views/Shared/Index.cshtml
~/Views/Shared/Index.vbhtml', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (8, CAST(N'2017-05-01 22:57:51.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Index.cshtml(12): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''ClienteId'' ni se encontró ningún método de extensión ''ClienteId'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (9, CAST(N'2017-05-01 22:58:00.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Create.cshtml(60): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''MetodoDePago'' ni se encontró ningún método de extensión ''MetodoDePago'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (10, CAST(N'2017-05-01 22:58:55.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'c:\Users\Leandro\Documents\Visual Studio 2013\Projects\FastService\FastService\Views\Venta\Create.cshtml(60): error CS1061: ''FastService.Models.VentaModel'' no contiene una definición de ''MetodoDePago'' ni se encontró ningún método de extensión ''MetodoDePago'' que acepte un primer argumento de tipo ''FastService.Models.VentaModel'' (¿falta una directiva using o una referencia de ensamblado?)', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (11, CAST(N'2017-05-01 23:02:03.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (12, CAST(N'2017-05-01 23:05:15.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (13, CAST(N'2017-05-01 23:12:31.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'', N'', N'', N'', N'')
GO
INSERT [dbo].[Logs] ([Id], [EventDateTime], [EventLevel], [UserName], [MachineName], [EventMessage], [ErrorSource], [ErrorClass], [ErrorMethod], [ErrorMessage], [InnerErrorMessage]) VALUES (14, CAST(N'2017-05-01 23:12:46.000' AS DateTime), N'Error', N'', N'NOTEBOOK-LEO', N'Validation failed for one or more entities. See ''EntityValidationErrors'' property for more details.', N'', N'', N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Logs] OFF
GO
SET IDENTITY_INSERT [dbo].[MetodoPago] ON 

GO
INSERT [dbo].[MetodoPago] ([MetodoPagoId], [Nombre], [Descripcion]) VALUES (1, N'Cheque', N'Cheque')
GO
INSERT [dbo].[MetodoPago] ([MetodoPagoId], [Nombre], [Descripcion]) VALUES (2, N'Tarjeta', N'Tarjeta')
GO
INSERT [dbo].[MetodoPago] ([MetodoPagoId], [Nombre], [Descripcion]) VALUES (3, N'Efectivo', N'Efectivo')
GO
INSERT [dbo].[MetodoPago] ([MetodoPagoId], [Nombre], [Descripcion]) VALUES (4, N'Transferencia', N'Transferencia Bancaria')
GO
SET IDENTITY_INSERT [dbo].[MetodoPago] OFF
GO

GO
INSERT [dbo].[Proveedor] ([ProveedorId], [Nombre], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES ('1', N'NCB', N'lopezleandro03@gmail.com', N'', N'', N'Larrea 1285 1A, Recoleta, CABA')
GO
INSERT [dbo].[Proveedor] ([ProveedorId], [Nombre], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES ('2', N'Peavey', N'juanperez03@gmail.com', N'', N'', N'Larrea 1285 1A, Recoleta, CABA')
GO
INSERT [dbo].[Proveedor] ([ProveedorId], [Nombre], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES ('4', N'Samsung', N'LOPEZLEANDRO03@GMAIL.COM', N'', N'', N'')
GO

SET IDENTITY_INSERT [dbo].[PuntoDeVenta] ON 

GO
INSERT [dbo].[PuntoDeVenta] ([PuntoDeVentaId], [Nombre], [Descripcion]) VALUES (1, N'FastService', N'FastService')
GO
INSERT [dbo].[PuntoDeVenta] ([PuntoDeVentaId], [Nombre], [Descripcion]) VALUES (2, N'ElectroShop', N'ElectroShop')
GO
SET IDENTITY_INSERT [dbo].[PuntoDeVenta] OFF
GO
INSERT [dbo].[Role] ([RolId], [Status], [Nombre], [Descripcion], [DefaultController], [DefaultAction]) VALUES (1, N'1', N'Gerente', N'Acceso total a datos de todos los puntos de venta', N'Payment', N'Create')
GO
INSERT [dbo].[Role] ([RolId], [Status], [Nombre], [Descripcion], [DefaultController], [DefaultAction]) VALUES (2, N'1', N'ElectroShopAdmin', N'Acceso a datos de ElectroShop', N'Venta', N'Create')
GO
INSERT [dbo].[Role] ([RolId], [Status], [Nombre], [Descripcion], [DefaultController], [DefaultAction]) VALUES (3, N'1', N'FastServiceAdmin', N'Acceso a datos de FastService', N'Venta', N'Create')
GO
SET IDENTITY_INSERT [dbo].[RoleMenu] ON 

GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (1, 1, 1)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (2, 1, 2)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (3, 1, 3)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (4, 1, 4)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (5, 1, 5)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (6, 1, 6)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (7, 1, 7)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (8, 1, 8)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (9, 2, 2)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (10, 2, 1)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (11, 2, 3)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (12, 2, 4)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (13, 2, 5)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (14, 2, 6)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (15, 3, 2)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (16, 3, 1)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (17, 3, 3)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (18, 3, 4)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (19, 3, 5)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (20, 3, 6)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (21, 1, 9)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (1021, 1, 1009)
GO
SET IDENTITY_INSERT [dbo].[RoleMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([UserId], [Email], [Nombre], [Apellido], [Contraseña], [Direccion], [Telefono1], [Telefono2]) VALUES (1, N'lopezleandro03@gmail.com', N'Leandro', N'Lopez', N'123456', N'', N'', N'')
GO
INSERT [dbo].[Usuario] ([UserId], [Email], [Nombre], [Apellido], [Contraseña], [Direccion], [Telefono1], [Telefono2]) VALUES (2, N'luislopezfv@gmail.com', N'Leandro', N'Lopez', N'123456', N'', N'', N'')
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[UsuarioRol] ([RolId], [UserId]) VALUES (1, 1)
GO

ALTER TABLE [dbo].[Compra]  WITH CHECK ADD FOREIGN KEY([ProveedorId])
REFERENCES [dbo].[Proveedor] ([ProveedorId])
GO
ALTER TABLE [dbo].[Compra]  WITH CHECK ADD FOREIGN KEY([PuntoDeVentaId])
REFERENCES [dbo].[PuntoDeVenta] ([PuntoDeVentaId])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([CompraId])
REFERENCES [dbo].[Compra] ([CompraId])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([TipoTransaccionId])
REFERENCES [dbo].[TipoTransaccion] ([TipoTransaccionId])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Factura] ([FacturaId])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Factura] ([FacturaId])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([MetodoDePagoId])
REFERENCES [dbo].[MetodoPago] ([MetodoPagoId])
GO
ALTER TABLE [dbo].[RoleMenu]  WITH CHECK ADD  CONSTRAINT [FK_RoleMenu_ItemMenu] FOREIGN KEY([ItemMenuId])
REFERENCES [dbo].[ItemMenu] ([ItemMenuId])
GO
ALTER TABLE [dbo].[RoleMenu] CHECK CONSTRAINT [FK_RoleMenu_ItemMenu]
GO
ALTER TABLE [dbo].[RoleMenu]  WITH CHECK ADD  CONSTRAINT [FK_RoleMenu_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Role] ([RolId])
GO
ALTER TABLE [dbo].[RoleMenu] CHECK CONSTRAINT [FK_RoleMenu_Rol]
GO
ALTER TABLE [dbo].[UsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRol_Rol] FOREIGN KEY([RolId])
REFERENCES [dbo].[Role] ([RolId])
GO
ALTER TABLE [dbo].[UsuarioRol] CHECK CONSTRAINT [FK_UsuarioRol_Rol]
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Factura] ([FacturaId])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Factura] ([FacturaId])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([PuntoDeVentaId])
REFERENCES [dbo].[PuntoDeVenta] ([PuntoDeVentaId])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([PuntoDeVentaId])
REFERENCES [dbo].[PuntoDeVenta] ([PuntoDeVentaId])
GO
ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([TipoTransaccionId])
REFERENCES [dbo].[TipoTransaccion] ([TipoTransaccionId])
GO

insert TipoTransaccion values(1,'Con Factura','Cobro/Pago Facturado'),(2,'Sin Factura','Cobro/Pago NO Facturado')






CREATE VIEW [dbo].[ComprasAPagar]
AS
 SELECT DISTINCT c.*, 
		ISNULL((select c.monto - (CASE SUM(p.monto) 
							WHEN null THEN (select monto from compra c2 where c2.compraid = c.compraid)
							ELSE SUM(p.monto) 
							END)
		   from pago p where p.CompraId = c.CompraId),c.monto) as saldo
 from Compra c
where CompraId in ( select CompraId 
							from Compra c3 where c3.monto > (select ISNULL(SUM(pa.monto),0) 
															 from Pago pa 
															 where pa.CompraId = c3.CompraId )) 





USE [FastService]
GO

/****** Object:  View [dbo].[ProveedoresAcreedores]    Script Date: 07/05/2017 06:58:26 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[ProveedoresAcreedores]
AS
 SELECT DISTINCT p.*
 from Proveedor p
where proveedorid in ( select proveedorid 
							from Compra c where c.monto > (select ISNULL(SUM(pa.monto),0) 
															 from Pago pa 
															 where pa.CompraId = c.CompraId )) 




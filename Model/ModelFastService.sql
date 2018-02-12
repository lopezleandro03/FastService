
drop table Logs
drop table Pago
drop table MetodoPago
drop table Venta
drop table TipoTransaccion
drop table Compra
drop table Factura
drop table Proveedor
drop table PuntoDeVenta
drop table Reparacion
drop table Usuario
drop table UsuarioRol
drop table RoleMenu
drop table ItemMenu
drop table Role
drop table Marca
drop table TipoDispositivo
drop table tiponovedad
drop table novedad
drop table EstadoReparacion
drop table Cliente
drop table Direccion

drop table ReparacionDetalle
drop table Comercio
drop table Modelo

drop view vw_ComprasAPagar
drop view vw_ProveedoresAcreedores
drop view vw_VentasDiarias
drop view vw_VentasMensuales
drop view vw_VentasAnuales

GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 02/05/2017 08:50:25 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Direccion](
	[DireccionId] [int] identity NOT NULL,
	Altura [varchar](50) NULL,
	Calle [varchar](200) NULL,
	Ciudad [varchar](200) NULL,
	Provincia [varchar](200) NULL,
	CodigoPostal [varchar](100) NULL,
	Pais [varchar](200) NULL,
	Comentarios [varchar](200) NULL,
	Latitud decimal(9,6)  NULL,
	Longitud decimal(9,6)  NULL,
	ChangedOn datetime not null,
	ChangedBy int not null
 CONSTRAINT [pk_DireccionId] PRIMARY KEY CLUSTERED 
(
	[DireccionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


CREATE TABLE [dbo].[Cliente](
	[ClienteId] int identity NOT NULL,
	[Dni] [int] NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Apellido] [varchar](200) NOT NULL,
	[Mail] [varchar](200) NULL,
	[Telefono1] [varchar](20) NULL,
	[Telefono2] [varchar](20) NULL,
	[Direccion] [varchar](300) NOT NULL,
	DireccionId int NULL,
	[Localidad] [varchar](300) NULL,
	[Latitud] float NULL,
	[Longitud] float NULL,
 CONSTRAINT [pk_ClienteId] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD FOREIGN KEY([DireccionId])
REFERENCES [dbo].[Direccion] ([DireccionId])
GO

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
	[Facturado] bit not null default 0,
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
	[Contacto] [varchar](200) NULL,
	[Telefono1] [varchar](50) NULL,
	[Telefono2] [varchar](50) NULL,
	[Direccion] [varchar](300) NULL,
	[Localidad] [varchar](100) NULL,
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
	[UserId] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
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
	[Facturado] bit not null default 0,
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
INSERT [dbo].[Cliente] (dni, [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (31123458, N'Matias', N'Gonzalez', N'lopezleandro03@gmail.com', N'1169411665', NULL, N'Larrea 1285 1A, Recoleta, CABA')
GO						
INSERT [dbo].[Cliente] (dni, [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (36212345, N'Juan', N'Perez', NULL, N'1169411665', N'', N'Larrea 1285 1A, Recoleta, CABA')
GO						
INSERT [dbo].[Cliente] (dni, [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (36234123, N'Pablo', N'Hormus', NULL, N'1169411665', N'', N'Larrea 1285 1A, Recoleta, CABA')
GO						
INSERT [dbo].[Cliente] (dni, [Nombre], [Apellido], [Mail], [Telefono1], [Telefono2], [Direccion]) VALUES (36285548, N'Leandro', N'Lopez', N'lopezleandro03@gmail.com', N'1143232000', N'1143232000', N'Calle 1328 Núnero 2801')
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
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (10, N'Reportes', 40, 1, NULL, NULL, NULL, NULL, N'fa fa-bar-chart')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (11, N'Reparaciones', 50, 1, NULL, NULL, NULL, NULL, N'fa fa-bar-chart')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (12, N'Mis Asignaciones', 50, 1, 11, 'Reparacion', 'MyIndex', NULL, N'fa fa-bar-chart')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (13, N'Ordenes', 60, 1, 11, 'Reparacion', 'Index', NULL, N'fa fa-bar-chart')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (14, N'Domicilios', 70, 1, 11, 'Domicilio', 'Index', NULL, N'fa fa-bar-chart')
GO
INSERT [dbo].[ItemMenu] ([ItemMenuId], [Name], [Orden], [Estado], [ItemMenuPadreId], [Controlador], [Accion], [Parametros], [Icon]) VALUES (15, N'Resumen', 30, 1, 2, N'Venta', N'Resumen', NULL, N'fa fa-list')
GO

SET IDENTITY_INSERT [dbo].[ItemMenu] OFF
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
INSERT [dbo].[Role] ([RolId], [Status], [Nombre], [Descripcion], [DefaultController], [DefaultAction]) VALUES (4, N'1', N'Tecnico', N'Tecnico de FastService', N'Venta', N'Create')
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
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (22, 1, 10)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (23, 1, 11)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (24, 1, 12)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (25, 1, 13)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (26, 1, 14)
GO
INSERT [dbo].[RoleMenu] ([id], [RolId], [ItemMenuId]) VALUES (27, 1, 15)
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


insert TipoTransaccion values(1,'Con Factura','Cobro/Pago Facturado'),(2,'Sin Factura','Cobro/Pago NO Facturado')

CREATE TABLE dbo.Reparacion (
	 ReparacionId int PRIMARY KEY not null,
	 ClienteId int not null,
	 EmpleadoAsignadoId int not null,
	 TecnicoAsignadoId int not null,
	 EstadoReparacionId int not null,
	 ComercioId int null,
	 MarcaId int not null,
	 TipoDispositivoId int not null,
	 ReparacionDetalleId int null,
	 ModificadoPor varchar(100),
	 ModificadoEn datetime not null,
)

CREATE TABLE dbo.ReparacionDetalle (
	 ReparacionDetalleId int identity PRIMARY KEY not null,
	 EsGarantia bit not null,
	 EsDomicilio bit not null,

	 NroReferencia varchar(200) null, 
	 FechoCompra datetime null,
	 NroFactura varchar(200) null,

	 Presupuesto decimal  null,
	 PresupuestoFecha datetime  null,
	 Precio decimal  null,

	 Modelo varchar(100) null,
	 Serie varchar(200) null,
	 Serbus varchar(200) null,

	 ModificadoPor varchar(100) not null,
	 ModificadoEn datetime not null,
)


CREATE TABLE dbo.Marca (
	 MarcaId int PRIMARY KEY identity  not null,
	 nombre varchar(200) not null,
	 descripcion varchar(400) null,
	 modificadoPor varchar(200) null,
	 modificadoEn datetime null,
)

CREATE TABLE dbo.TipoDispositivo (
	 TipoDispositivoId int PRIMARY KEY identity  not null,
	 nombre varchar(200) not null,
	 descripcion varchar(400) null,
	 modificadoPor varchar(200) null,
	 modificadoEn datetime null,
)

CREATE TABLE dbo.TipoNovedad (
	 TipoNovedadId int PRIMARY KEY not null,
	 nombre varchar(200) not null,
	 descripcion varchar(400) null,
	 activo bit,
	 modificadoPor varchar(200) null,
	 modificadoEn datetime null,
)

CREATE TABLE dbo.Novedad (
	novedadId int PRIMARY KEY identity not null,
	reparacionId int not null,
	UserId int not null,
	tipoNovedadId int not null,
	monto decimal null,
	observacion varchar(500) null,
	modificadoPor varchar(200) not null,
	modificadoEn datetime not null,
)

CREATE TABLE dbo.EstadoReparacion (
	 EstadoReparacionId int identity PRIMARY KEY not null,
	 nombre varchar(200) not null,
	 descripcion varchar(400) null,
	 categoria varchar(200) null,
	 activo bit,
	 modificadoPor varchar(200) null,
	 modificadoEn datetime null,
)

CREATE TABLE dbo.Comercio (
	 ComercioId int identity PRIMARY KEY not null,
	 Code varchar(100)  null,
	 Descripcion varchar(200)  null,
	 Contacto varchar(200) null,
	 Direccion varchar(400) null,
	 Localidad varchar(200) null,
	 Mail varchar(200) null,
	 Telefono varchar(200) null,
	 Telefono2 varchar(200) null,
	 cuit varchar(200) null,
	 modificadoPor varchar(200) null,
	 modificadoEn datetime null,
)
GO

CREATE TABLE dbo.Modelo (
	 DispositivoModeloId int PRIMARY KEY not null,
	 MarcaId int null,
	 TipoId int null,
	 Modelo varchar(200) null,
	 Code varchar(200) null,
	 Descripcion varchar(400) null,
	 modificadoPor varchar(200) null,
	 modificadoEn datetime null,
)
GO

ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([EmpleadoAsignadoId])
REFERENCES [dbo].[Usuario] ([UserId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([TecnicoAsignadoId])
REFERENCES [dbo].[Usuario] ([UserId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([EstadoReparacionId])
REFERENCES [dbo].[EstadoReparacion] ([EstadoReparacionId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([MarcaId])
REFERENCES [dbo].[Marca] ([MarcaId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([TipoDispositivoId])
REFERENCES [dbo].[TipoDispositivo] ([TipoDispositivoId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([ReparacionDetalleId])
REFERENCES [dbo].[ReparacionDetalle] ([ReparacionDetalleId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([ComercioId])
REFERENCES [dbo].[Comercio] ([ComercioId])
GO
ALTER TABLE [dbo].[Reparacion]  WITH CHECK ADD FOREIGN KEY([TipoDispositivoId])
REFERENCES [dbo].[TipoDispositivo] ([TipoDispositivoId])
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

--ALTER TABLE [dbo].Reparacion  WITH CHECK ADD FOREIGN KEY([ClienteId])
--REFERENCES [dbo].[Cliente] ([ClienteId])

--ALTER TABLE [dbo].Reparacion  WITH CHECK ADD FOREIGN KEY(EmpleadoAsignadoId)
--REFERENCES [dbo].[Usuario] ([UserId])

--ALTER TABLE [dbo].Reparacion  WITH CHECK ADD FOREIGN KEY(TecnicoAsignadoId)
--REFERENCES [dbo].Usuario (UserId)

--ALTER TABLE [dbo].Reparacion  WITH CHECK ADD FOREIGN KEY(EstadoRaparacionId)
--REFERENCES [dbo].EstadoReparacion (EstadoRaparacionId)

--ALTER TABLE [dbo].Reparacion  WITH CHECK ADD FOREIGN KEY(MarcaId)
--REFERENCES [dbo].Marca (MarcaId)

--ALTER TABLE [dbo].Reparacion  WITH CHECK ADD FOREIGN KEY(TipoDispositivoId)
--REFERENCES [dbo].Tipo (TipoDispositivoId)

GO

CREATE VIEW [dbo].[vw_ComprasAPagar]
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

GO

/****** Object:  View [dbo].[ProveedoresAcreedores]    Script Date: 07/05/2017 06:58:26 p.m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON

GO

CREATE VIEW [dbo].[vw_ProveedoresAcreedores]
AS
 SELECT DISTINCT p.*
 from Proveedor p
where proveedorid in ( select proveedorid 
							from Compra c where c.monto > (select ISNULL(SUM(pa.monto),0) 
															 from Pago pa 
															 where pa.CompraId = c.CompraId )) 
go

CREATE VIEW [dbo].[vw_VentasDiarias]
AS
 select SUM(monto) as Total, facturado from venta group by Day(Fecha),Facturado

go

CREATE VIEW [dbo].[vw_VentasMensuales]
AS
 select SUM(monto) as Total, facturado from venta group by Month(Fecha),Facturado

go

CREATE VIEW [dbo].[vw_VentasAnuales]
AS
 select SUM(monto) as Total, facturado from venta group by Year(Fecha),Facturado


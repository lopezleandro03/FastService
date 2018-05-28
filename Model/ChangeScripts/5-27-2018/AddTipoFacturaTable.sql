--first removed references

/****** Object:  Table [dbo].[Factura]    Script Date: 5/27/2018 1:17:27 PM ******/
DROP TABLE [dbo].[Factura]
GO

/****** Object:  Table [dbo].[Factura]    Script Date: 5/27/2018 1:17:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Factura](
	[FacturaId] [int] IDENTITY(1,1) NOT NULL,
	[TipoFacturaId] [int] NOT NULL,
	[NroFactura] varchar(100) NOT NULL,
	[Media] [varbinary](max) NULL,
	[ModificadoEn] datetime not NULL,
	[ModificadoPor] int not NULL,
 CONSTRAINT [pk_FacturaId] PRIMARY KEY CLUSTERED 
(
	[FacturaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[TipoFactura](
	[TipoFacturaId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] varchar(50) NOT NULL,
	[Descripcion] varchar(100) NOT NULL,
	[ModificadoEn] datetime not NULL,
	[ModificadoPor] int not NULL,
 CONSTRAINT [pk_TipoFacturaId] PRIMARY KEY CLUSTERED 
(
	[TipoFacturaId] ASC
))

GO

ALTER TABLE [dbo].[Venta]  WITH CHECK ADD FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Factura] ([FacturaId])
GO

ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Factura] ([FacturaId])
GO

ALTER TABLE [dbo].[Factura]  WITH CHECK ADD FOREIGN KEY([TipoFacturaId])
REFERENCES [dbo].[TipoFactura] ([TipoFacturaId])
GO

insert [TipoFactura]
values ('Factura A', 'Usualmente usada para facturar a empresas', getdate(),1)

insert [TipoFactura]
values ('Factura B', 'Usualmente usada para facturar comprador final', getdate(),1)


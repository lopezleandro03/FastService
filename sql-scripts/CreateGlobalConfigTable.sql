USE [FastService]
GO

/****** Object:  Table [dbo].[ItemMenu]    Script Date: 12/31/2018 4:22:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GlobalConfig](
	[GlobalConfigId] [int] IDENTITY(1,1) NOT NULL,
	[Key] [varchar](300) NOT NULL,
	[Value] [varchar](300) NOT NULL,
	
PRIMARY KEY CLUSTERED 
(
	[GlobalConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

insert [GlobalConfig] values('NotificacionesFechaInicioAno','2018')
insert [GlobalConfig] values('NotificacionesFechaInicioMes','3')
insert [GlobalConfig] values('NotificacionesMinimoDiasInactividad','30')

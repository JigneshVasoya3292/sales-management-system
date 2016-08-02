USE [InventoryDB]
GO

/****** Object:  Table [dbo].[Inventory]    Script Date: 07/23/2014 22:37:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Inventory](
	[Brand] [varchar](50) NOT NULL,
	[StyleCode] [varchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[Pieces] [int] NOT NULL,
	[SellingPrice] [float] NOT NULL,
	[Sold] [int] NOT NULL,
	[Date] [date] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


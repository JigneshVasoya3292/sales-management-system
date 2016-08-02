USE [InventoryDB]
GO

/****** Object:  Table [dbo].[SoldItems]    Script Date: 07/23/2014 22:38:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SoldItems](
	[Brand] [nvarchar](50) NOT NULL,
	[StyleCode] [nvarchar](50) NOT NULL,
	[PurchasePrice] [float] NOT NULL,
	[Price] [float] NOT NULL,
	[Pieces] [int] NOT NULL,
	[Date] [date] NOT NULL
) ON [PRIMARY]

GO


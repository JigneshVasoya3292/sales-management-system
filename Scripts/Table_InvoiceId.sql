USE [InventoryDB]
GO

/****** Object:  Table [dbo].[InvoiceId]    Script Date: 07/23/2014 22:38:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[InvoiceId](
	[InvoiceKey] [varchar](50) NOT NULL,
	[LastInvoiceId] [bigint] NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


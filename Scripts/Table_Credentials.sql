USE [InventoryDB]
GO

/****** Object:  Table [dbo].[Credentials]    Script Date: 07/23/2014 22:37:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Credentials](
	[Username] [nchar](10) NOT NULL,
	[Password] [nchar](10) NULL
) ON [PRIMARY]

GO


USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[InsertSoldItems]    Script Date: 07/23/2014 22:41:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertSoldItems]
	-- Add the parameters for the stored procedure here
	@Brand varchar(50),
	@StyleCode varchar(50),
	@PurchasePrice float,
	@Price float,
	@Pieces int,
	@Date date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into dbo.SoldItems values(@Brand,@StyleCode,@PurchasePrice,@Price,@Pieces,@Date)
END

GO


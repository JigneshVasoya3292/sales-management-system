USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[InsertInventory]    Script Date: 07/23/2014 22:41:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertInventory]
	-- Add the parameters for the stored procedure here
	@Brand varchar(50),
	@StyleCode varchar(50),
	@Price float,
	@Pieces int,
	@SellingPrice float,
	@Sold int,
    @Date date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Inventory values(@Brand,@StyleCode,@Price,@Pieces,@SellingPrice,@Sold,@Date)
END

GO


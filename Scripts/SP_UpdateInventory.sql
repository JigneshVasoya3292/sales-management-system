USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[UpdateInventory]    Script Date: 07/23/2014 22:41:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateInventory]
	-- Add the parameters for the stored procedure here
	@Brand varchar(50),
	@StyleCode varchar(50),
	@Price float,
	@Pieces int,
	@Sold int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Inventory
	SET [Pieces] = @Pieces, [Sold] = @Sold + [Sold]
	FROM Inventory
	Where [Brand] = @Brand AND [StyleCode] = @StyleCode
END

GO


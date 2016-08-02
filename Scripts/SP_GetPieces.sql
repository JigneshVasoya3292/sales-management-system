USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[GetPieces]    Script Date: 07/23/2014 22:40:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPieces]
	-- Add the parameters for the stored procedure here
	@Brand varchar(50),
	@StyleCode varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Pieces FROM Inventory WHERE [Brand] = @Brand AND [StyleCode] =@StyleCode
END


GO


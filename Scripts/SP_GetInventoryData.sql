USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[GetInventoryData]    Script Date: 07/23/2014 22:39:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetInventoryData]
	-- Add the parameters for the stored procedure here
	@FromDate date,
	@ToDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM Inventory WHERE [Date] >= @FromDate AND [Date] <= @ToDate
END
GO


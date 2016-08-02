USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[UpdateInvoiceId]    Script Date: 07/23/2014 22:42:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateInvoiceId] 
	-- Add the parameters for the stored procedure here
	@InvoiceKey varchar(50),
	@LastInvoiceId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
    -- Insert statements for procedure here
	UPDATE InvoiceId
	SET [LastInvoiceId] = @LastInvoiceId
	FROM InvoiceId
	Where [InvoiceKey] = @InvoiceKey

END
GO


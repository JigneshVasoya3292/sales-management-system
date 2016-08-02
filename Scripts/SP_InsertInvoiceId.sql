USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[InsertInvoiceId]    Script Date: 07/23/2014 22:41:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertInvoiceId] 
	-- Add the parameters for the stored procedure here
	@InvoiceKey varchar(50),
	@LastInvoiceId bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    
    -- Insert statements for procedure here
	insert into InvoiceId values (@InvoiceKey,@LastInvoiceId)

END
GO


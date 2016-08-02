USE [InventoryDB]
GO

/****** Object:  StoredProcedure [dbo].[InsertCredentials]    Script Date: 07/23/2014 22:40:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertCredentials] 
	-- Add the parameters for the stored procedure here
	@Username nchar(10),
	@Password nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into Credentials values(@Username,@Password)
END


GO


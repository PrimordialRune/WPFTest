USE [games]
GO
/****** Object:  StoredProcedure [dbo].[insertGame]    Script Date: 18/07/2022 09:47:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TIQ-STAGE
-- Create date: 
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[insertGame] 
	-- Add the parameters for the stored procedure here
	@Name varchar(100),
	@ReleaseDate date,
	@Publisher varchar(8) = 'NTD02341',
	@Engine varchar(50) = 'Unity',
	@ConsoleID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [dbo].[Games](Title, Release_Date, Publisher, Engine) VALUES(@Name, CONVERT(DATE,@ReleaseDate), @Publisher, @Engine)
	INSERT INTO [dbo].[_GamePlatforms](GameID, ConsoleID) VALUES(SCOPE_IDENTITY(), @ConsoleID);
END

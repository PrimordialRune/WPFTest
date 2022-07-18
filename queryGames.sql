-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TIQ-STAGE
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE queryGames
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [g].[ID] AS [GameID], [g].[Title], [g].[Release_Date], [gp].[ConsoleID], [c].[Name] AS [ConsoleName], [c].[BrandID], [c].[BrandID], [br].[Name] AS [BrandName]
	FROM [dbo].[Games] AS [g] JOIN [dbo].[_GamePlatforms] AS [gp] ON [g].[ID] = [gp].[GameID] JOIN [dbo].[Consoles] AS [c] ON [gp].[ConsoleID] = [c].[ID] JOIN [dbo].[Brands] AS [br] ON [c].[BrandID] = [br].[ID]
END
GO

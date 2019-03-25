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
-- Author:		Ulises Muñoz
-- Create date: 28022018
-- Description:	Paginador
-- =============================================
alter PROCEDURE [sp_NewsPagination]
 @page INT,
 @size INT,
 @parent INT,
 @sort nvarchar(50) ,
 @param nvarchar(50) ,
 @dateI datetime ,
 @dateF datetime ,
 @filterDate bit,
 @totalrow INT  OUTPUT
AS
BEGIN
	--DECLARE @page INT = 0
	--DECLARE @size INT = 10

    DECLARE @offset INT 
    DECLARE @newsize INT 
    DECLARE @sql NVARCHAR(MAX)

    IF(@page=0)
      BEGIN
        SET @offset = @page
        SET @newsize = @size
       END
    ELSE 
      BEGIN
        SET @offset = @page*@size
        SET @newsize = @size
      END
    SET NOCOUNT ON;

     WITH OrderedSet AS
    (
      SELECT *, ROW_NUMBER() OVER (ORDER BY Fecha desc) AS [Index] FROM [dbo].[cmsNoticias]
	  where IdParent = @parent AND Active = 1 AND (@filterDate = 0 OR Fecha BETWEEN @dateI AND @dateF) and ( @param = '0' OR Titulo  LIKE '%' + @param + '%') 
    )

   SELECT * FROM OrderedSet WHERE [Index] BETWEEN CONVERT(NVARCHAR(12), @offset + 1)  AND  CONVERT(NVARCHAR(12), (@offset + @newsize)) 

   SET @totalrow = (SELECT COUNT(*) FROM OrderedSet)
END
-- ALTER_ProductsSPROC.sql
-- Backup your database files before running.
-- This script drops existing ProductsSPROC and recreates it with NVARCHAR(MAX) parameters.
-- Replace the comment block below with the original procedure body logic (the SQL that follows the parameter list in the original proc).
-- If the original script uses temp tables or casts, ensure those are adjusted to NVARCHAR(MAX) as well.

IF OBJECT_ID('dbo.ProductsSPROC', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ProductsSPROC;
GO

CREATE PROCEDURE dbo.ProductsSPROC
  @Action           NVARCHAR(50),
  @ProductID        INT = NULL,
  @CategoryID       INT = NULL,
  @Title            NVARCHAR(MAX) = NULL,
  @ShortDescription NVARCHAR(MAX) = NULL,
  @LongDescription  NVARCHAR(MAX) = NULL,
  @ImageUrl         NVARCHAR(MAX) = NULL,
  @Price            MONEY = 0.00
AS
BEGIN
  SET NOCOUNT ON;

  -- ===== PASTE ORIGINAL PROC BODY HERE =====
  -- Example:
  -- IF @Action = 'SELECT' BEGIN
  --    SELECT * FROM dbo.Product;
  -- END
  -- ELSE IF @Action = 'INSERT' BEGIN
  --    INSERT INTO dbo.Product (CategoryID, Title, ShortDescription, LongDescription, ImageUrl, Price)
  --    VALUES (@CategoryID, @Title, @ShortDescription, @LongDescription, @ImageUrl, @Price);
  -- END
  -- ========================================

END
GO

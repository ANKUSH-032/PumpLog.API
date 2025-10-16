/*
uspFuelFillingList 0,'',-1,''
*/

CREATE PROC [dbo].[uspFuelFillingList]
    @Start INT = 0,    
    @PageSize INT = 10,    
    @SortCol VARCHAR(100) = 'CreatedOn DESC',    
    @SearchKey VARCHAR(MAX) = '' 
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Status BIT = 0, 
            @Msg VARCHAR(4000),
            @recordsTotal INT;

    BEGIN TRY
        
        IF NOT EXISTS (SELECT 1 FROM [dbo].[tblFuelFilling] WHERE [IsDeleted] = 0)
        BEGIN
            SET @Status = 0;
            SET @Msg = 'No active records found';
            SELECT @Status AS [Status], @Msg AS [Message];
            RETURN;
        END;

        
        SET @SearchKey = LTRIM(RTRIM(ISNULL(@SearchKey, '')));
        SET @SortCol = LTRIM(RTRIM(ISNULL(@SortCol, 'CreatedOn DESC')));
        IF @PageSize <= 0 SET @PageSize = -1;
		SELECT 1 AS [Status],'Data Fetched Successfully' AS [Message]  
      
        SELECT 
            @recordsTotal = COUNT(1)
        FROM [dbo].[tblFuelFilling]
        WHERE [IsDeleted] = 0
          AND (
                @SearchKey = '' OR
                [DispenserNo] LIKE '%' + @SearchKey + '%' OR
                [VehicleNumber] LIKE '%' + @SearchKey + '%' OR
                [PaymentMode] LIKE '%' + @SearchKey + '%' OR
                [CreatedBy] LIKE '%' + @SearchKey + '%'
              );

        
        SELECT 
            [FuelFillingId],
            D.DispenserNo ,
            [QuantityFilled],
            [VehicleNumber],
            M.PaymentModeName AS PaymentMode,
            [PaymentProofPath],
            [CreatedOn],
            [CreatedBy],
            @recordsTotal AS TotalRecords,
            1 AS [Status],
            'Success' AS [Message]
        FROM [dbo].[tblFuelFilling] F
		JOIN [dbo].[tblDispenserMaster] D ON D.DispenserId = F.DispenserNo
		JOIN [dbo].[tblPaymentModeMaster] M ON M.PaymentModeId = F.PaymentMode
        WHERE [IsDeleted] = 0
          AND (
                @SearchKey = '' OR
                D.[DispenserNo] LIKE '%' + @SearchKey + '%' OR
                [VehicleNumber] LIKE '%' + @SearchKey + '%' OR
                [PaymentMode] LIKE '%' + @SearchKey + '%' OR
				[PaymentModeName] LIKE '%' + @SearchKey + '%' OR
                [CreatedBy] LIKE '%' + @SearchKey + '%'
              )
        ORDER BY
            CASE WHEN @SortCol = 'DispenserNo ASC' THEN D.[DispenserNo] END ASC,
            CASE WHEN @SortCol = 'DispenserNo DESC' THEN D.[DispenserNo] END DESC,
            CASE WHEN @SortCol = 'QuantityFilled ASC' THEN [QuantityFilled] END ASC,
            CASE WHEN @SortCol = 'QuantityFilled DESC' THEN [QuantityFilled] END DESC,
            CASE WHEN @SortCol = 'CreatedOn ASC' THEN [CreatedOn] END ASC,
            CASE WHEN @SortCol = 'CreatedOn DESC' THEN [CreatedOn] END DESC,
            [CreatedOn] DESC -- default
        OFFSET @Start ROWS
        FETCH NEXT 
            (CASE WHEN @PageSize = -1 THEN @recordsTotal ELSE @PageSize END) ROWS ONLY;

SET @recordsTotal  = (    
SELECT COUNT(*) FROM [dbo].[tblFuelFilling] F
		JOIN [dbo].[tblDispenserMaster] D ON D.DispenserId = F.DispenserNo
		JOIN [dbo].[tblPaymentModeMaster] M ON M.PaymentModeId = F.PaymentMode
        WHERE [IsDeleted] = 0
          AND (
                @SearchKey = '' OR
                D.[DispenserNo] LIKE '%' + @SearchKey + '%' OR
                [VehicleNumber] LIKE '%' + @SearchKey + '%' OR
                [PaymentMode] LIKE '%' + @SearchKey + '%' OR
				[PaymentModeName] LIKE '%' + @SearchKey + '%' OR
                [CreatedBy] LIKE '%' + @SearchKey + '%'
              )
) --- it will fetch total row count from table 
SELECT @recordsTotal AS totalRecords    
    END TRY
    BEGIN CATCH
        SET @Status = 0;
        SET @Msg = ERROR_MESSAGE();
        SELECT @Status AS [Status], @Msg AS [Message];
    END CATCH
END;
CREATE   PROC [dbo].[uspFuelFillingGet]
(
    @FuelFillingId UNIQUEIDENTIFIER
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Status BIT = 0, @Msg VARCHAR(4000);

    BEGIN TRY
        IF NOT EXISTS (
            SELECT 1 
            FROM [dbo].[tblFuelFilling] 
            WHERE [FuelFillingId] = @FuelFillingId AND [IsDeleted] = 0
        )
        BEGIN
            SET @Status = 0;
            SET @Msg = 'No record found';
            SELECT @Status AS [Status], @Msg AS [Message];
            RETURN;
        END;

        SET @Status = 1;
        SET @Msg = 'Success';
        SELECT @Status AS [Status], @Msg AS [Message];

        SELECT 
            [FuelFillingId],
            [DispenserNo],
            [QuantityFilled],
            [VehicleNumber],
            [PaymentMode],
            [PaymentProofPath],
            [CreatedOn],
            [CreatedBy],
            [UpdatedOn],
            [UpdatedBy]
        FROM [dbo].[tblFuelFilling]
        WHERE [FuelFillingId] = @FuelFillingId
          AND [IsDeleted] = 0;
    END TRY
    BEGIN CATCH
        SET @Status = 0;
        SET @Msg = ERROR_MESSAGE();
        SELECT @Status AS [Status], @Msg AS [Message];
    END CATCH
END;
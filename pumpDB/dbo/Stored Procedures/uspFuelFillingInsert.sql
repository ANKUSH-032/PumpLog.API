CREATE   PROC [dbo].[uspFuelFillingInsert]
(
    @DispenserNo VARCHAR(10),
    @QuantityFilled DECIMAL(10,2),
    @VehicleNumber VARCHAR(20),
    @PaymentMode VARCHAR(20),
    @PaymentProofPath VARCHAR(500) = NULL,
    @CreatedBy VARCHAR(50)
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @Status BIT = 0, @Msg VARCHAR(4000);

    BEGIN TRY
        INSERT INTO [dbo].[tblFuelFilling]
        (
            [DispenserNo],
            [QuantityFilled],
            [VehicleNumber],
            [PaymentMode],
            [PaymentProofPath],
            [CreatedBy],
            [CreatedOn],
            [IsDeleted]
        )
        VALUES
        (
            @DispenserNo,
            @QuantityFilled,
            @VehicleNumber,
            @PaymentMode,
            @PaymentProofPath,
            @CreatedBy,
            GETDATE(),
            0
        );

        SET @Status = 1;
        SET @Msg = 'Record inserted successfully';
        SELECT @Status AS [Status], @Msg AS [Message];
    END TRY
    BEGIN CATCH
        SET @Status = 0;
        SET @Msg = ERROR_MESSAGE();
        SELECT @Status AS [Status], @Msg AS [Message];
    END CATCH
END;
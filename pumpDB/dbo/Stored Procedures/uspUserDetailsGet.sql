CREATE   PROC [dbo].[uspUserDetailsGet]
(
    @UserId VARCHAR(50) = NULL,
    @Email VARCHAR(150) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Status BIT = 0;
    DECLARE @Msg VARCHAR(4000);

    BEGIN TRY
        -- Check for mandatory parameters
        IF @UserId IS NULL AND @Email IS NULL
        BEGIN
            SET @Status = 0;
            SET @Msg = 'UserId or Email is required';
            SELECT @Status AS [Status], @Msg AS [Message];
            RETURN;
        END;

        -- Normalize input
        SET @UserId = LTRIM(RTRIM(@UserId));
        SET @Email  = LTRIM(RTRIM(@Email));

        -- Handle DELETED user first
        IF EXISTS (
            SELECT 1 FROM [dbo].[tblUsers] WITH (NOLOCK)
            WHERE (UserId = @UserId OR Email = @Email)
              AND IsDeleted = 1
        )
        BEGIN
            SELECT 'DELETED' AS [Name];
            SET @Status = 0;
            SET @Msg = 'Failure';
            SELECT @Status AS [Status], @Msg AS [Message];
            RETURN;
        END;

        -- Handle NON-REGISTERED user
        IF NOT EXISTS (
            SELECT 1 FROM [dbo].[tblUsers] WITH (NOLOCK)
            WHERE (UserId = @UserId OR Email = @Email)
              AND IsDeleted = 0
        )
        BEGIN
            SELECT 'UserNotRegister' AS [Name];
            SET @Status = 0;
            SET @Msg = 'Failure';
            SELECT @Status AS [Status], @Msg AS [Message];
            RETURN;
        END;

        -- User exists and is active
        SELECT 
            U.[UserId],
            U.[Email],
            U.[PasswordHash],
            U.[PasswordSalt],
            ISNULL(U.[FirstName], '') AS [Name],
            U.[RoleId] AS [Role]
        FROM [dbo].[tblUsers] U WITH (NOLOCK)
        WHERE (U.UserId = @UserId OR U.Email = @Email)
          AND U.IsDeleted = 0;

        SET @Status = 1;
        SET @Msg = 'Success';
        SELECT @Status AS [Status], @Msg AS [Message];
    END TRY

    BEGIN CATCH
        SET @Status = 0;
        SET @Msg = ERROR_MESSAGE();
        SELECT @Status AS [Status], @Msg AS [Message];
        THROW;  -- rethrow error for debugging/logging
    END CATCH
END;
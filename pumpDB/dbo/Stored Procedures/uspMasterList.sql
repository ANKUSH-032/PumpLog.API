         -- exec    uspMasterList 'Dispenser'           
CREATE PROCEDURE [dbo].[uspMasterList] 
  @Type VARCHAR(50)                
                 
AS                
BEGIN                
 SET NOCOUNT ON;                
 SET @Type = UPPER(@Type);                
                
 BEGIN TRY          
  SELECT 1 AS [Status], 'Data Fetched Successfully' AS [Message]    
  IF @Type = 'Dispenser'                
   SELECT [DispenserId] AS Id, [DispenserNo] AS Value
        FROM [dbo].[tblDispenserMaster]
       
        ORDER BY [DispenserNo];              
   ELSE IF @Type = 'PaymentMode'
    SELECT [PaymentModeId] AS Id, [PaymentModeName] AS Value
        FROM [dbo].[tblPaymentModeMaster]
       
        ORDER BY [PaymentModeName];
               
                
 END TRY                
                
 BEGIN CATCH                
  DECLARE @Msg VARCHAR(500) = Error_message();                
  DECLARE @ErrorSeverity INT = Error_severity();                
  DECLARE @ErrorState INT = Error_state();                
                
  RAISERROR (                
    @Msg                
    ,@ErrorSeverity                
    ,@ErrorState                
    );                
 END CATCH                
END
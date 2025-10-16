CREATE TABLE [dbo].[tblPaymentModeMaster] (
    [PaymentModeId]   INT          IDENTITY (1, 1) NOT NULL,
    [PaymentModeName] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([PaymentModeId] ASC),
    UNIQUE NONCLUSTERED ([PaymentModeName] ASC)
);


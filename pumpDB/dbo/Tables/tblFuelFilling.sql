CREATE TABLE [dbo].[tblFuelFilling] (
    [FuelFillingId]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [DispenserNo]      VARCHAR (10)     NOT NULL,
    [QuantityFilled]   DECIMAL (10, 2)  NOT NULL,
    [VehicleNumber]    VARCHAR (20)     NOT NULL,
    [PaymentMode]      VARCHAR (20)     NOT NULL,
    [PaymentProofPath] VARCHAR (500)    NULL,
    [CreatedOn]        DATETIME         DEFAULT (getdate()) NULL,
    [CreatedBy]        VARCHAR (50)     NULL,
    [UpdatedOn]        DATETIME         NULL,
    [UpdatedBy]        VARCHAR (50)     NULL,
    [DeletedOn]        DATETIME         NULL,
    [DeletedBy]        VARCHAR (50)     NULL,
    [IsDeleted]        BIT              DEFAULT ((0)) NULL,
    CONSTRAINT [PK_tblFuelFilling] PRIMARY KEY CLUSTERED ([FuelFillingId] ASC)
);


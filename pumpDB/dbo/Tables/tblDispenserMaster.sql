CREATE TABLE [dbo].[tblDispenserMaster] (
    [DispenserId] INT          IDENTITY (1, 1) NOT NULL,
    [DispenserNo] VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([DispenserId] ASC),
    UNIQUE NONCLUSTERED ([DispenserNo] ASC)
);


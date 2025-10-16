CREATE TABLE [dbo].[tblMasterRole] (
    [RoleId]   VARCHAR (50) CONSTRAINT [DF_tblMasterRole_RoleId] DEFAULT (newid()) NOT NULL,
    [RoleName] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_tblMasterRole] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


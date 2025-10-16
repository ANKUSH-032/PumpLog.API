CREATE TABLE [dbo].[tblUsers] (
    [UserId]       VARCHAR (50)    NOT NULL,
    [FirstName]    VARCHAR (50)    NOT NULL,
    [LastName]     VARCHAR (50)    NOT NULL,
    [Email]        VARCHAR (50)    NOT NULL,
    [PasswordHash] VARBINARY (MAX) NOT NULL,
    [PasswordSalt] VARBINARY (MAX) NOT NULL,
    [RoleId]       VARCHAR (50)    NOT NULL,
    [CreatedOn]    DATETIME        NULL,
    [CreatedBy]    VARCHAR (50)    NULL,
    [UpdatedOn]    DATETIME        NULL,
    [UpdatedBy]    VARCHAR (50)    NULL,
    [DeletedOn]    DATETIME        NULL,
    [DeletedBy]    VARCHAR (50)    NULL,
    [IsDeleted]    BIT             NOT NULL,
    CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED ([UserId] ASC)
);


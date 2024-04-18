CREATE TABLE [dbo].[Color] (
    [ColorId] INT          IDENTITY (1, 1) NOT NULL,
    [Value]   VARCHAR (50) NOT NULL,
    [Hash]    INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([ColorId] ASC)
);


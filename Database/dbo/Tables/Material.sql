CREATE TABLE [dbo].[Material]
(
	[MaterialId] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Value] varchar(50) not null,
	[Hash] int not null
)

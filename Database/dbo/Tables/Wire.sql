CREATE TABLE [dbo].[Wire]
(
	[WireId] INT NOT NULL PRIMARY KEY Identity(1,1),
	[MaterialId] int not null references Material(MaterialId),
	[LengthId] int not null references Length(LengthId),
	[DiameterId] int not null references Diameter(DiameterId),
	[ColorId] int not null references Color(ColorId)
)

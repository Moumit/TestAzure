CREATE TABLE [dbo].[Country]
(
	[CountryId] INT NOT NULL PRIMARY KEY identity(1,1),
	[Name] nvarchar(200) not null,
	[CreatedDate] datetime not null,
	[CreatedBy] nvarchar(200) not null,
)

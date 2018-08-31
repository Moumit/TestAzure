CREATE TABLE [dbo].[Company]
(
	[CompanyId] INT NOT NULL identity(1,1),
	[Name] nvarchar(200) null,
	[Status] TinyInt not null,
	CountryId int null,
	[CreatedDate] datetime not null,
	[CreatedBy] nvarchar(200) not null,
	 CONSTRAINT [pk_Company] PRIMARY KEY CLUSTERED ([CompanyId] ASC),
    CONSTRAINT [FK_Company_country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([CountryId]),
)

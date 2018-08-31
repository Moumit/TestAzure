CREATE TABLE [dbo].[Employee]
(
    [EmployeeId]INT NOT NULL identity(1,1),
	[CompanyId] INT NOT NULL,
	[Name] nvarchar(200) null,
	[StatusId] int not null,	 
	[CreatedDate] datetime not null,
	[CreatedBy] nvarchar(200) not null,
	CONSTRAINT [pk_Employee] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    CONSTRAINT [FK_Employee_Company] FOREIGN KEY (CompanyId) REFERENCES [dbo].Company (CompanyId),
)

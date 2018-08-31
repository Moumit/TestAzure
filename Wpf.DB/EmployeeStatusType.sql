CREATE TABLE [dbo].[EmployeeStatusType]
(
	[EmployeeStatusTypeId] INT NOT NULL PRIMARY KEY,
	DisplayText nvarchar(50) not null,
	[CreatedDate] datetime not null,
	[CreatedBy] nvarchar(200) not null,
)

CREATE TABLE [dbo].[Master]
(
	[Id] BIGINT NOT NULL PRIMARY KEY,
	[MasterCategoryId] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,
    [Pan] VARCHAR(50) NOT NULL, 
	[MSISDN] VARCHAR(20) NOT NULL,
    [CanBindCustomer] BIT NOT NULL DEFAULT(1),
	[Created] DATETIME2 NOT NULL,
	[Status] NVARCHAR(50),

	constraint fk_Master_MasterCategoryId foreign key ([MasterCategoryId]) references MasterCategory ([Id])
)

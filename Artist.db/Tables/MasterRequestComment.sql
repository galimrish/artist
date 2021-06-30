CREATE TABLE [dbo].[MasterRequestComment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	[MasterRequestId] INT NOT NULL,
    [Author] NVARCHAR(100), 
    [Date] DATETIME2 NOT NULL, 
    [Message] NVARCHAR(MAX) NOT NULL,

	constraint fk_MasterRequestComment_MasterRequestId foreign key ([MasterRequestId]) references MasterRequest([Id])
)



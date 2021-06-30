CREATE TABLE [dbo].[MasterRequest]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [CustomerId] BIGINT NOT NULL, 
    [MasterCategoryId] INT NOT NULL, 
    [RequestDate] DATETIME2 NOT NULL,
	[State] INT NOT NULL, 
    [StateChangeDate] DATETIME2 NOT NULL, 
    [QuestionaryAnswerId] BIGINT NULL, 
	 
    constraint fk_MasterRequest_MasterCategoryId foreign key ([MasterCategoryId]) references MasterCategory ([Id])
)

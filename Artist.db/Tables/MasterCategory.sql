CREATE TABLE [dbo].[MasterCategory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(150) NOT NULL, 
    [MasterOfferUri] VARCHAR(300) NOT NULL,
	[MasterOfferText] NVARCHAR(MAX) NULL,
    [CustomerOfferUri] VARCHAR(300) NOT NULL, 
    [CustomerOfferText] NVARCHAR(MAX), 
    [QuestionaryId] INT NULL
)

/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[MasterCategory] ON

INSERT INTO [dbo].[MasterCategory] ([Id], [Name], [Description], [MasterOfferUri], [MasterOfferText], [CustomerOfferUri], [CustomerOfferText], [QuestionaryId])
VALUES (1, 'MAKEUPER', N'Makeuper', 'notEmpty', N'{Offer} for makeuper', 'notEmpty', N'{Offer} for customer', 10)

INSERT INTO [dbo].[MasterCategory] ([Id], [Name], [Description], [MasterOfferUri], [MasterOfferText], [CustomerOfferUri], [CustomerOfferText], [QuestionaryId])
VALUES (2, 'HAIRDRESSER', N'Hairdresser', 'notEmpty', N'{Offer} for hairdresser', 'notEmpty', N'{Offer} for customer', 11)

INSERT INTO [dbo].[MasterCategory] ([Id], [Name], [Description], [MasterOfferUri], [MasterOfferText], [CustomerOfferUri], [CustomerOfferText], [QuestionaryId])
VALUES (3, 'CASMETOLO-GIST', N'Casmetologist', 'notEmpty', N'{Offer} for casmetologist', 'notEmpty', N'{Offer} for customer', 12)

SET IDENTITY_INSERT [dbo].[MasterCategory] OFF

insert into dbo.Master (Id, MasterCategoryId, Name,                 Pan,                CanBindCustomer, Created,      [Status],	MSISDN)
                 select 0,  1,                N'First Name', '990000000001',     1,               getutcdate(), 'None',	'70000000001'

insert into dbo.Master (Id, MasterCategoryId, Name,                 Pan,                CanBindCustomer, Created,      [Status],	MSISDN)
                 select 2,  2,                N'Another Name',	    '990000000002',     1,               getutcdate(), 'None',	'70000000002'

insert into dbo.Master (Id, MasterCategoryId, Name,                 Pan,                CanBindCustomer, Created,      [Status],	MSISDN)
                 select 3,  1,                N'Jane Doe',		    '990000000003',     0,               getutcdate(), 'None',	'70000000003'

insert into dbo.Master (Id, MasterCategoryId, Name,                 Pan,                CanBindCustomer, Created,      [Status],	MSISDN)
                 select 4,  3,                N'Unknown',    '990000000004',     1,               getutcdate(), 'None',	'70000000004'


SET IDENTITY_INSERT [dbo].[MasterRequest] ON

INSERT INTO [dbo].[MasterRequest] ([Id], [CustomerId], [MasterCategoryId], [RequestDate], [State], [StateChangeDate], [QuestionaryAnswerId])
VALUES (1, 6, 1, N'2018-02-26 06:18:39', 3, N'2018-02-26 06:18:39', 252)

INSERT INTO [dbo].[MasterRequest] ([Id], [CustomerId], [MasterCategoryId], [RequestDate], [State], [StateChangeDate], [QuestionaryAnswerId])
VALUES (2, 6, 1, N'2018-02-26 06:18:39', 0, N'2018-02-26 06:18:39', 253)

INSERT INTO [dbo].[MasterRequest] ([Id], [CustomerId], [MasterCategoryId], [RequestDate], [State], [StateChangeDate], [QuestionaryAnswerId])
VALUES (3, 7, 2, N'2018-02-26 06:18:39', 1, N'2018-02-26 06:18:39', 254)

INSERT INTO [dbo].[MasterRequest] ([Id], [CustomerId], [MasterCategoryId], [RequestDate], [State], [StateChangeDate], [QuestionaryAnswerId])
VALUES (4, 2, 1, N'2018-02-26 06:18:39', 2, N'2018-02-26 06:18:39', 255)

INSERT INTO [dbo].[MasterRequest] ([Id], [CustomerId], [MasterCategoryId], [RequestDate], [State], [StateChangeDate], [QuestionaryAnswerId])
VALUES (5, 8, 3, N'2018-02-26 06:18:39', 0, N'2018-02-26 06:18:39', 256)

SET IDENTITY_INSERT [dbo].[MasterRequest] OFF
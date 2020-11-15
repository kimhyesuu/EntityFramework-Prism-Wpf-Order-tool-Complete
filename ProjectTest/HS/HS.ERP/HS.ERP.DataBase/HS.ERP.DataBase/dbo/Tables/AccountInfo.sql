CREATE TABLE [dbo].[AccountInfo]
(
	[AccountId] INT NOT NULL IDENTITY,  
    [CompanyName] NVARCHAR(20) NOT NULL, 
    [CompanyEmail] VARCHAR(100) NOT NULL, 
    [CompanyPhone] VARCHAR(12) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Descrption] NVARCHAR(MAX) NULL, 
    [CreatedDate] TIMESTAMP NOT NULL, 
    [UpdatedDate] TIMESTAMP NULL,
	
	CONSTRAINT [PK_AccountId] PRIMARY KEY ([AccountId])
)

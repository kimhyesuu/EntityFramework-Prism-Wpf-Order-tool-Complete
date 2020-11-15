CREATE TABLE [dbo].[AccountInfo]
(
	[AccountId] BIGINT NOT NULL , 
    [CompanyName] NVARCHAR(20) NOT NULL, 
    [CompanyEmail] VARCHAR(100) NOT NULL, 
    [CompanyPhone] VARCHAR(12) NOT NULL, 
    [Address] NVARCHAR(50) NOT NULL, 
    [Descrption] NVARCHAR(MAX) NULL, 
    [CreatedDate] TIMESTAMP NOT NULL, 
    [UpdatedDate] DATETIME NULL,
	 
    CONSTRAINT [PK_AccountId] PRIMARY KEY ([AccountId]),
	CONSTRAINT [UK_CompanyEmail] UNIQUE ([CompanyEmail]),
	CONSTRAINT [UK_CompanyPhone] UNIQUE ([CompanyPhone])
)

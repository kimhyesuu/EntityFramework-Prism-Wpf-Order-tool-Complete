CREATE TABLE [dbo].[AccountInfo]
(
	[PK_AccountId] INT NOT NULL IDENTITY, 
    [CompanyName] NVARCHAR(20) NULL, 
    [CompanyManager] NVARCHAR(15) NULL, 
    [PhoneNumber] VARCHAR(15) NULL, 
    [Email] VARCHAR(100) NULL, 
    [Address] NVARCHAR(100) NULL, 
    [Description] NVARCHAR(800) NULL, 
    [CreatedTime] DATETIME NULL, 
    [UpdateTime] DATETIME NULL,

	PRIMARY KEY(PK_AccountId)
)

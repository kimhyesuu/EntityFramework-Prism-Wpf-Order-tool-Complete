CREATE TABLE [dbo].[Contact]
(
	[ContactId] INT NOT NULL , 
    [AccountId] INT NOT NULL REFERENCES [AccountInfo]([AccountId]), 
    [ContactName] NVARCHAR(20) NOT NULL, 
    [Department] NVARCHAR(30) NULL, 
    [Position] NCHAR(10) NULL, 
    [PhoneNumber] VARCHAR(12) NULL,

    CONSTRAINT [PK_ContactId] PRIMARY KEY ([ContactId]), 
	CONSTRAINT [UK_AccountId] UNIQUE ([AccountId]) 
)

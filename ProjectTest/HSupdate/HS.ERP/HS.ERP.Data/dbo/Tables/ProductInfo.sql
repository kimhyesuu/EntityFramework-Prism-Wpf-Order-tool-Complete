CREATE TABLE [dbo].[ProductInfo]
(
	[PK_ProductId] INT NOT NULL IDENTITY, 
    [ProductName] NVARCHAR(30) NOT NULL, 
    [Description] NVARCHAR(800) NULL, 
    [CreatedTime] DATETIME NOT NULL, 
    [UpdatedTime] DATETIME NULL,

	PRIMARY KEY(PK_ProductId)
)

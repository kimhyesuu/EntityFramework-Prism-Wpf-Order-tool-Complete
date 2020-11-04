CREATE TABLE [dbo].[Order]
(
	[PK_OrderId] INT NOT NULL, 
    [AccountId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [OrderQuantity] INT NULL, 
    [Description] NVARCHAR(800) NULL, 
    [CreatedDate] DATETIME NULL, 

	PRIMARY KEY(PK_OrderId),
    CONSTRAINT [FK_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [AccountInfo]([PK_AccountId]), 
    CONSTRAINT [FK_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [ProductInfo]([PK_ProductId])
)

CREATE TABLE [dbo].[Order_Product]
(
	[DetailedOrderId] BIGINT NOT NULL PRIMARY KEY, 
    [ProductId] BIGINT NOT NULL, 
    [OrderId] BIGINT NOT NULL, 
    [ProductName] NVARCHAR(50) NOT NULL, 
    [ProductQuantity] INT NOT NULL, 

    CONSTRAINT [FK_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product]([ProductId]), 
    CONSTRAINT [FK_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order]([OrderId])
)

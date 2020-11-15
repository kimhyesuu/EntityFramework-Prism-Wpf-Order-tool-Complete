CREATE TABLE [dbo].[Product]
(
	[ProductId] BIGINT NOT NULL, 
    [ProductName] NVARCHAR(50) NOT NULL, 
    [ProductPrice] MONEY NULL, 
    [Descrption] NVARCHAR(MAX) NULL, 
    [ProductImage] VARBINARY(MAX) NULL, 
    [CreatedDate] TIMESTAMP NOT NULL

	CONSTRAINT [PK_ProductId] PRIMARY KEY ([ProductId])
)

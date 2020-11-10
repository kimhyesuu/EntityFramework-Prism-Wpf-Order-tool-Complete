CREATE TABLE [dbo].[Order]
(
	[OrderId] BIGINT NOT NULL , 
    [AccountId] INT NOT NULL, 
    [OrderPrice] MONEY NOT NULL, 
    [Status] BIT NOT NULL, 
    [CreatedDate] TIMESTAMP NOT NULL

	CONSTRAINT [PK_OrderId] PRIMARY KEY ([OrderId]), 
    CONSTRAINT [FK_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [AccountInfo]([AccountId])
)

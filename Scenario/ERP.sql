drop table if exists [dbo].[Order]
drop table if exists [dbo].[AccountInfo]
drop table if exists [dbo].[OrderProduct]
drop table if exists [dbo].[Contact]
drop table if exists [dbo].[Product]
drop table if exists [dbo].[__MigrationHistory]


Create Table [dbo].[Account]
(
	AccountId bigint not null,
	CompanyName nvarchar(30) not null,
	CompanyEmail nvarchar(255),
	Address nvarchar(255) not null,
	ContactName nvarchar(20) not null,
	Department nvarchar(20),
	Position nvarchar(10),
	Description nvarchar(800),
	CreatedDate datetime not null,
	UpdatedDate timestamp

	constraint AccountId_PK primary key (AccountId),
	constraint CompanyEmail_UK Unique (CompanyEmail),
)

Create Table [dbo].[TelePhone]
(
	TelephoneId int not null,
	AccountId bigint not null,
	TelePrefix varchar(3) not null,	
	TelePhoneNumber varchar(9) not null,

	constraint TelephoneId_PK primary key (TelephoneId),
	constraint AccountId_FK foreign key (AccountId) references [dbo].[Account](AccountId),
)

Create Table [dbo].[Order]
(
	OrderId bigint not null,
	AccountId bigint not null,
	OrderPrice int not null,
	Description nvarchar(800),
	CreatedDate datetime not null,

	constraint OrderId_PK primary key (OrderId),
	constraint AccountId_FK foreign key (AccountId) references [dbo].[Account](AccountId),
)

Create Table [dbo].[Product]
(
	ProductId bigint not null,
	ProductName nvarchar(50) not null,
	ProductPrice int not null,
	Description varchar(3) not null,	
	CreatedDate datetime not null,

	constraint ProductId_PK primary key (ProductId)	
)

Create Table [dbo].[OrderProduct]
(
	DetailedOrderId bigint not null,
	OrderId bigint not null,
	ProductId bigint not null,
	ProductName varchar(3) not null,	
	ProductQuantity datetime not null,

	constraint DetailedOrderId_PK primary key (DetailedOrderId),
	constraint OrderId_FK foreign key (OrderId) references [dbo].[Order](OrderId),
	constraint ProductId_FK foreign key (ProductId) references [dbo].[Product](ProductId)
)


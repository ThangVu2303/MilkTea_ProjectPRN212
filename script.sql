--create database MilkTea
USE [MilkTea]
GO

/****** Object:  Table [dbo].[Role]   **/
CREATE TABLE [dbo].[Role] (
    [roleId] INT PRIMARY KEY ,
    [roleName] NVARCHAR(50)  NOT NULL
);
/****** Object:  Table [dbo].[Account]    **/
CREATE TABLE Account (
    accountId INT PRIMARY KEY IDENTITY(1,1),
    username NVARCHAR(50)  NOT NULL,
    password NVARCHAR(255) NOT NULL,
    fullName NVARCHAR(100) NOT NULL,
    phone NVARCHAR(15)  NOT NULL,
    roleId INT NOT NULL,
    FOREIGN KEY (roleId) REFERENCES role(roleId) 
);

/****** Object:  Table [dbo].[Staff]    */
CREATE TABLE Staff (
    staffId INT PRIMARY KEY IDENTITY(1,1),
    hireDate DATE NOT NULL,
    gender NVARCHAR(10) CHECK (gender IN ('Male', 'Female')) NOT NULL,
    dateOfBirth DATE NOT NULL,
    email NVARCHAR(100)  NOT NULL,
    accountId INT UNIQUE NOT NULL,
    FOREIGN KEY (accountId) REFERENCES Account(accountId) 
);

/****** Object:  Table [dbo].[Customer]    */
CREATE TABLE Customer (
    customerId INT PRIMARY KEY IDENTITY(1,1),
    point INT DEFAULT 0,
    accountId INT  NOT NULL,
    FOREIGN KEY (accountId) REFERENCES Account(accountId) 
);


/****** Object:  Table [dbo].[Admin]    */
CREATE TABLE admin (
    adminId INT PRIMARY KEY IDENTITY(1,1),
    gender NVARCHAR(10) CHECK (gender IN ('Male', 'Female')) NOT NULL,
    dob DATE NOT NULL,
    email NVARCHAR(100)  NOT NULL,
    accountId INT UNIQUE NOT NULL,
    FOREIGN KEY (accountId) REFERENCES Account(accountId) ON DELETE CASCADE
);

/****** Object:  Table [dbo].[Category]   */
CREATE TABLE [dbo].[Category] (
    [CategoryId] INT PRIMARY KEY,
    [CategoryName] NVARCHAR(50) NOT NULL
);
/****** Object:  Table [dbo].[Product]    Script Date: 11/15/2021 1:02:52 PM ******/
CREATE TABLE [dbo].[Product] (
    [ProductId] NVARCHAR(50) PRIMARY KEY,
    [ProductName] NVARCHAR(50) NOT NULL,
    [Quantity] INT NOT NULL CHECK ([Quantity] >= 0),
	[OriginalPrice] MONEY NOT NULL CHECH ([Price] >= 0),
    [Price] MONEY NOT NULL CHECK ([Price] >= 0),
    [Origin] NVARCHAR(50) NOT NULL,
    [CategoryId] INT NOT NULL,
    [Image] NVARCHAR(250) NOT NULL,
    FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category]([CategoryId]) 
);
/****** Object:  Table [dbo].[Orders]    */
CREATE TABLE [dbo].[Orders] (
    [OrderId] INT PRIMARY KEY IDENTITY(1,1),
    [StaffId] INT NOT NULL,
    [CustomerId] INT NOT NULL,
	[Point] INT CHECK ([Point] >= 0),
    [Total] MONEY NOT NULL CHECK ([Total] >= 0),
    [DateCreate] DATE NOT NULL,
    FOREIGN KEY ([StaffId]) REFERENCES [dbo].[Staff]([StaffId]) ,
    FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer]([CustomerId]) 
);

/****** Object:  Table [dbo].[OrdersDetail]    */
CREATE TABLE [dbo].[OrdersDetail] (
    [OrderId] INT NOT NULL,
    [ProductId] NVARCHAR(50) NOT NULL,
    [Quantity] INT NOT NULL CHECK ([Quantity] > 0),
    [Price] MONEY NOT NULL CHECK ([Price] >= 0),
    PRIMARY KEY ([OrderId], [ProductId]),  
    FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders]([OrderId]) ,
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([ProductId]) 
);





INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (2, N'Staff')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (3, N'Customer')
GO

INSERT INTO [dbo].[Account] ([Username], [Password], [RoleId], [FullName], [Phone]) 
VALUES 
   
    (N'admin01', N'123', 1, N'Admin One', N'0912345678'),
    (N'admin02', N'123', 1, N'Admin Two', N'0908765432'),

	(N'staff1', N'123', 2, N'Nguyễn Văn Táo', N'0981002003'),
    (N'staff2', N'123', 2, N'Bạch Thị Mận', N'0981112113'),
    (N'staff3', N'123', 2, N'Lê Thị Bưởi', N'0981222223'),
    (N'staff4', N'123', 2, N'Nguyễn Văn Bòng', N'0981332333'),
    (N'staff5', N'123', 2, N'Trần Sầu Riêng', N'0981442443'),

    (N'customer1', N'123', 3, N'Trần Bống', N'0971552553'),
    (N'customer2', N'123', 3, N'Nguyễn Văn C', N'0971662663'),
    (N'customer3', N'123', 3, N'Trần Thị Lê', N'0971772773'),
    (N'customer4', N'123', 3, N'Nguyễn Văn Bảy', N'0971882883'),
    (N'customer5', N'123', 3, N'Trần Meo Meo', N'0971992993'),
    (N'customer6', N'123', 3, N'Nguyễn Hợi', N'0972113113'),
    (N'customer7', N'123', 3, N'Mộc Lan', N'0972223223'),
    (N'customer8', N'123', 3, N'Dương Quá', N'0972333333');


INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (1, N'Milk Teas')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (2, N'Teas')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (3, N'Foods')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (4, N'Fruits')
INSERT [dbo].[Category] ([CategoryId], [CategoryName]) VALUES (5, N'Others')


INSERT INTO [dbo].[Staff] ([HireDate], [Gender], [DateOfBirth], [Email], [AccountId]) 
VALUES 
    ('2022-04-18', N'Male', '1991-01-14', N'staff06@example.com', 
        (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'staff1')),
    ('2022-06-22', N'Female', '1993-06-25', N'staff07@example.com', 
        (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'staff2')),
    ('2023-07-10', N'Male', '1995-09-11', N'staff08@example.com', 
        (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'staff3')),
    ('2023-08-15', N'Female', '1996-12-05', N'staff09@example.com', 
        (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'staff4')),
    ('2023-09-20', N'Male', '1997-03-08', N'staff10@example.com', 
        (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'staff5'));

INSERT INTO [dbo].[Customer] ([Point], [AccountId]) 
VALUES 
    (5, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer1')),
    (10, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer2')),
    (5, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer3')),
    (6, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer4')),
    (7, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer5')),
    (10, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer6')),
    (2, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer7')),
    (8, (SELECT AccountId FROM [dbo].[Account] WHERE Username = 'customer8'));

INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'1', N'Apple', 10,12000.0000, 15000.0000, N'US', 4, N'/ProductImage/download.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'10', N'Tra Vai', 10,15000.0000, 20000.0000, N'VN', 2, N'/ProductImage/download (1).jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'11', N'Tra Chanh', 15,15000.0000, 20000.0000, N'VN', 2, N'/ProductImage/images.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'12', N'Tra O Long', 15,20000.0000, 25000.0000, N'VN', 2, N'/ProductImage/olong.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'13', N'Tra Thai Tu ', 15,20000.0000, 25000.0000, N'VN', 2, N'/ProductImage/trathai.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'14', N'Pizza', 15,100000.0000, 150000.0000, N'US', 3, N'/ProductImage/pizza.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'15', N'Xuc Xich', 15,5000.0000, 10000.0000, N'GER', 3, N'/ProductImage/xucxich.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'16', N'Hamburger', 15,35000.0000, 45000.0000, N'VN', 3, N'/ProductImage/ham.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'17', N'Banh My', 15, 20000.0000, 25000.0000, N'VN', 3, N'/ProductImage/banhmi.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'18', N'Ca Phe', 15,15000.0000, 25000.0000, N'VN', 4, N'/ProductImage/caphe.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'2', N'Mango', 15,10000.0000, 20000.0000, N'US', 4, N'/ProductImage/xoai.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'3', N'Orange', 15,10000.0000, 15000.0000, N'US', 4, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'4', N'Tran Chau Duong Den', 15, 20000.0000,30000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'5', N'Tra Hong', 15,20000.0000, 30000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'6', N'Tra Dau', 15,25000.0000, 35000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'7', N'Tra Sua Oreo', 15,25000.0000, 35000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'8', N'Tra Sua Matcha', 15,25000.0000, 35000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Quantity],[OriginalPrice], [Price], [Origin], [CategoryId], [Image]) VALUES (N'9', N'Tra Dao', 15, 10000.0000,20000.0000, N'VN', 2, N'/ProductImage/ảnh thu.jpg')
GO

INSERT INTO [dbo].[Orders] ([StaffId], [CustomerId], [Point], [Total], [DateCreate]) 
VALUES 
    (1, 3, 0, 150000, '2024-03-01'), -- Đơn hàng 1
    (2, 4, 0, 90000, '2024-03-02'),  -- Đơn hàng 2
    (3, 5, 0, 175000, '2024-03-03'), -- Đơn hàng 3
    (4, 6, 0, 145000, '2024-03-04'), -- Đơn hàng 4
    (5, 7, 0, 160000, '2024-03-05'); -- Đơn hàng 5


INSERT INTO [dbo].[OrdersDetail] ([OrderId], [ProductId], [Quantity], [Price]) 
VALUES 
    -- Đơn hàng 1
    (1, N'1', 2, 30000), -- Apple
    (1, N'2', 1, 20000), -- Mango
    (1, N'5', 1, 30000), -- Trà Hồng
    (1, N'6', 2, 70000), -- Trà Dâu

    -- Đơn hàng 2
    (2, N'9', 1, 20000), -- Trà Đào
    (2, N'10', 2, 40000), -- Trà Vải
    (2, N'11', 1, 20000), -- Trà Chanh

    -- Đơn hàng 3
    (3, N'12', 1, 25000), -- Trà Ô Long
    (3, N'14', 1, 100000), -- Pizza
    (3, N'17', 2, 50000), -- Bánh Mì

    -- Đơn hàng 4
    (4, N'3', 1, 15000), -- Orange
    (4, N'7', 2, 70000), -- Trà Sữa Oreo
    (4, N'8', 1, 35000), -- Trà Sữa Matcha
    (4, N'18', 1, 25000), -- Cà Phê

    -- Đơn hàng 5
    (5, N'4', 2, 60000), -- Trân Châu Đường Đen
    (5, N'13', 1, 25000), -- Trà Thái Tử
    (5, N'15', 3, 30000), -- Xúc Xích
    (5, N'16', 1, 45000); -- Hamburger





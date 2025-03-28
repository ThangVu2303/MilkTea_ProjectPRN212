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
    [Quantity] INT NULL,
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
	[OriginalTotal] MONEY NOT NULL CHECK ([OriginalTotal] >= 0),
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
/****** Object:  Table [dbo].[RawMaterial]    */
CREATE TABLE [dbo].[RawMaterial] (
    [MaterialId] INT PRIMARY KEY IDENTITY(1,1),
    [MaterialName] NVARCHAR(100) NOT NULL,
    [Unit] NVARCHAR(20) NOT NULL, -- Đơn vị tính (kg, ml, cái, ...)
    [CostPerUnit] MONEY NOT NULL CHECK ([CostPerUnit] >= 0), -- Giá nhập mỗi đơn vị
    [Quantity] FLOAT NOT NULL CHECK ([Quantity] >= 0) -- Số lượng tồn kho
);

/****** Object:  Table [dbo].[Recipe]    */
CREATE TABLE [dbo].[Recipe] (
    [RecipeId] INT PRIMARY KEY IDENTITY(1,1),
    [ProductId] NVARCHAR(50) NOT NULL,
    [MaterialId] INT NOT NULL,
    [QuantityRequired] FLOAT NOT NULL CHECK ([QuantityRequired] > 0), -- Lượng nguyên liệu cần thiết để sản xuất 1 sản phẩm
    FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product]([ProductId]),
    FOREIGN KEY ([MaterialId]) REFERENCES [dbo].[RawMaterial]([MaterialId])
);
CREATE TABLE DisposedMaterials (
    DisposedId INT PRIMARY KEY IDENTITY(1,1),
    MaterialId INT NOT NULL,
    Quantity FLOAT NOT NULL CHECK (Quantity > 0),
    DateDisposed DATE NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (MaterialId) REFERENCES RawMaterial(MaterialId)
);


CREATE TRIGGER trg_UpdateProductQuantity_AfterRecipe
ON Recipe
AFTER INSERT
AS
BEGIN
    UPDATE p
    SET p.Quantity = (
        SELECT MIN(rm.Quantity / r.QuantityRequired) 
        FROM Recipe r
        JOIN RawMaterial rm ON r.MaterialId = rm.MaterialId
        WHERE r.ProductId = p.ProductId
    )
    FROM Product p
    WHERE p.ProductId IN (SELECT ProductId FROM inserted);
END;


INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (2, N'Staff')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (3, N'Customer')
INSERT [dbo].[Role] ([roleId], [roleName]) VALUES (4, N'Admin')
GO


INSERT INTO [dbo].[Account] ([Username], [Password], [RoleId], [FullName], [Phone]) 
VALUES 
   
    (N'admin01', N'123', 1, N'Admin One', N'0912345678'),
    (N'admin02', N'123', 4, N'Admin Two', N'0908765432'),

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

INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'1', N'Apple', 15000.0000, N'US', 4, N'/ProductImage/download.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'10', N'Tra Vai', 20000.0000, N'VN', 2, N'/ProductImage/download (1).jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'11', N'Tra Chanh', 20000.0000, N'VN', 2, N'/ProductImage/images.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'12', N'Tra O Long', 25000.0000, N'VN', 2, N'/ProductImage/olong.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'13', N'Tra Thai Tu ', 25000.0000, N'VN', 2, N'/ProductImage/trathai.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'14', N'Pizza', 150000.0000, N'US', 3, N'/ProductImage/pizza.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'15', N'Xuc Xich', 10000.0000, N'GER', 3, N'/ProductImage/xucxich.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'16', N'Hamburger', 45000.0000, N'VN', 3, N'/ProductImage/ham.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'17', N'Banh My', 25000.0000, N'VN', 3, N'/ProductImage/banhmi.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'18', N'Ca Phe', 25000.0000, N'VN', 4, N'/ProductImage/caphe.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'2', N'Mango', 20000.0000, N'US', 4, N'/ProductImage/xoai.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'3', N'Orange', 15000.0000, N'US', 4, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'4', N'Tran Chau Duong Den',30000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'5', N'Tra Hong', 30000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'6', N'Tra Dau', 35000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'7', N'Tra Sua Oreo', 35000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'8', N'Tra Sua Matcha', 35000.0000, N'VN', 1, N'/ProductImage/ảnh thu.jpg')
INSERT [dbo].[Product] ([ProductId], [ProductName], [Price], [Origin], [CategoryId], [Image]) VALUES (N'9', N'Tra Dao',20000.0000, N'VN', 2, N'/ProductImage/ảnh thu.jpg')
GO
INSERT INTO [dbo].[RawMaterial] ([MaterialName], [Unit], [CostPerUnit], [Quantity]) 
VALUES 
(N'Trái Táo', N'kg', 30000, 10),
(N'Trái Xoài', N'kg', 25000, 10),
(N'Trái Cam', N'kg', 28000, 15),
(N'Trái Vải', N'kg', 35000, 25),
(N'Trái Đào', N'kg', 34000, 10),
(N'Trà Xanh', N'kg', 50000, 10),
(N'Trà Đen', N'kg', 40000, 10),
(N'Trà Oolong', N'kg', 60000, 10),
(N'Bột Matcha', N'kg', 70000, 15),
(N'Bột Oreo', N'kg', 80000, 10),
(N'Bột Sữa', N'kg', 75000, 15),
(N'Đường', N'kg', 20000, 10),
(N'Kem Cheese', N'kg', 90000, 10),
(N'Trân Châu', N'kg', 30000, 10),
(N'Siro Hoa Quả', N'lit', 25000, 10),
(N'Bột Bánh Pizza', N'kg', 70000, 10),
(N'Xúc Xích', N'kg', 50000, 10),
(N'Bánh Mì', N'cái', 10000, 20),
(N'Phô Mai', N'kg', 80000, 15),
(N'Bột Cà Phê', N'kg', 90000, 20);

INSERT INTO [dbo].[Recipe] ([ProductId], [MaterialId], [QuantityRequired]) 
VALUES 
-- Apple
(1, 1, 0.3), -- 300g Táo

-- Mango
(2, 2, 0.3), -- 300g Xoài

-- Orange
(3, 3, 0.3), -- 300g Cam

-- Trân Châu Đường Đen
(4, 7, 0.03), -- 30g Trà Đen
(4, 12, 0.05), -- 50g Đường
(4, 14, 0.1), -- 100g Trân Châu

-- Trà Hồng
(5, 6, 0.03),
(5, 12, 0.05),

-- Trà Dâu
(6, 6, 0.03),
(6, 12, 0.05),
(6, 15, 0.02), -- 20ml Siro Hoa Quả

-- Trà sữa Oreo
(7, 11, 0.3), -- 300g Bột Sữa
(7, 12, 0.05),
(7, 10, 0.02),
(7, 14, 0.1),

-- Trà sữa Matcha
(8, 11, 0.3),
(8, 12, 0.05),
(8, 9, 0.02),
(8, 14, 0.1),

-- Trà Đào
(9, 8, 0.03),
(9, 12, 0.05),
(9, 5, 0.02),

-- Trà Vải
(10, 4, 0.03),
(10, 12, 0.05),
(10, 15, 0.02),

-- Trà Chanh
(11, 6, 0.03),
(11, 12, 0.05),

-- Trà Oolong
(12, 8, 0.03),
(12, 12, 0.05),

-- Trà Thái Tứ
(13, 7, 0.03),
(13, 12, 0.05),

-- Pizza
(14, 16, 0.2), -- 200g Bột Bánh Pizza
(14, 19, 0.05), -- 50g Phô Mai
(14, 17, 0.1), -- 100g Xúc Xích

-- Xúc xích
(15, 17, 0.1),

-- Hamburger
(16, 18, 1), -- 1 cái Bánh Mì
(16, 17, 0.1),
(16, 19, 0.05),

-- Bánh Mì
(17, 18, 1),

-- Cà phê
(18, 20, 0.02); -- 20g Bột Cà Phê



INSERT INTO [dbo].[Orders] ([StaffId], [CustomerId], [Point],[OriginalTotal], [Total], [DateCreate]) 
VALUES 
    (1, 3, 0, 100000, 150000, '2024-03-01'), -- Đơn hàng 1
    (2, 4, 0,50000, 90000, '2024-03-02'),  -- Đơn hàng 2
    (3, 5, 0,120000, 175000, '2024-03-03'), -- Đơn hàng 3
    (4, 6, 0, 100000,145000, '2024-03-04'), -- Đơn hàng 4
    (5, 7, 0,115000, 160000, '2024-03-05'); -- Đơn hàng 5


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
INSERT INTO DisposedMaterials (MaterialId, Quantity, DateDisposed)
VALUES (2, 3, '2025-03-05') -- Bỏ 3 đơn vị nguyên liệu 2 vào ngày 5/3/2025



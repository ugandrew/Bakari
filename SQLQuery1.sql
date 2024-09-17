IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE TABLE [Category] (
        [CategoryId] int NOT NULL IDENTITY,
        [CategoryCode] nvarchar(max) NOT NULL,
        [CategoryName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE TABLE [Order] (
        [OrderId] int NOT NULL IDENTITY,
        [OrderDate] datetime2 NOT NULL,
        [OrderNumber] nvarchar(max) NULL,
        [SubTotal] decimal(18,2) NOT NULL,
        [Discount] decimal(18,2) NOT NULL,
        [OrderTotal] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Order] PRIMARY KEY ([OrderId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE TABLE [Transanction] (
        [Id] int NOT NULL IDENTITY,
        [Type] int NOT NULL,
        [Amount] decimal(18,2) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Date] datetime2 NOT NULL,
        [PerformedBy] nvarchar(max) NULL,
        [TransanctionId] int NULL,
        CONSTRAINT [PK_Transanction] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Transanction_Transanction_TransanctionId] FOREIGN KEY ([TransanctionId]) REFERENCES [Transanction] ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE TABLE [Item] (
        [ItemId] int NOT NULL IDENTITY,
        [CategoryId] int NOT NULL,
        [ItemCode] nvarchar(max) NOT NULL,
        [ItemName] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [ItemPrice] decimal(18,2) NOT NULL,
        [ImagePath] nvarchar(max) NULL,
        CONSTRAINT [PK_Item] PRIMARY KEY ([ItemId]),
        CONSTRAINT [FK_Item_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE TABLE [Basket] (
        [BasketId] int NOT NULL IDENTITY,
        [ItemId] int NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        [ImagePath] nvarchar(max) NULL,
        CONSTRAINT [PK_Basket] PRIMARY KEY ([BasketId]),
        CONSTRAINT [FK_Basket_Item_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Item] ([ItemId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE TABLE [OrderDetail] (
        [OrderDetailId] int NOT NULL IDENTITY,
        [OrderId] int NOT NULL,
        [ItemId] int NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [UnitPrice] decimal(18,2) NOT NULL,
        [TotalPrice] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_OrderDetail] PRIMARY KEY ([OrderDetailId]),
        CONSTRAINT [FK_OrderDetail_Item_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Item] ([ItemId]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderDetail_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([OrderId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE TABLE [Stock] (
        [StockId] int NOT NULL IDENTITY,
        [ItemId] int NOT NULL,
        [Quantity] decimal(18,2) NOT NULL,
        [CostPrice] decimal(18,2) NOT NULL,
        [Total] decimal(18,2) NOT NULL,
        [ImagePath] nvarchar(max) NULL,
        CONSTRAINT [PK_Stock] PRIMARY KEY ([StockId]),
        CONSTRAINT [FK_Stock_Item_ItemId] FOREIGN KEY ([ItemId]) REFERENCES [Item] ([ItemId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE INDEX [IX_Basket_ItemId] ON [Basket] ([ItemId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE INDEX [IX_Item_CategoryId] ON [Item] ([CategoryId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE INDEX [IX_OrderDetail_ItemId] ON [OrderDetail] ([ItemId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE INDEX [IX_OrderDetail_OrderId] ON [OrderDetail] ([OrderId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE INDEX [IX_Stock_ItemId] ON [Stock] ([ItemId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    CREATE INDEX [IX_Transanction_TransanctionId] ON [Transanction] ([TransanctionId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240716184919_Items'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240716184919_Items', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    ALTER TABLE [Transanction] ADD [CustomerId] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    ALTER TABLE [Stock] ADD [Category] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    ALTER TABLE [Stock] ADD [LastUpDated] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD [Orderby] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    ALTER TABLE [Order] ADD [Orderby] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    CREATE TABLE [Customer] (
        [CustomerId] int NOT NULL IDENTITY,
        [CustomerName] nvarchar(max) NOT NULL,
        [PhoneNumber] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Customer] PRIMARY KEY ([CustomerId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    CREATE INDEX [IX_Transanction_CustomerId] ON [Transanction] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    ALTER TABLE [Transanction] ADD CONSTRAINT [FK_Transanction_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([CustomerId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240730124711_Stocks'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240730124711_Stocks', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240812101048_Stock'
)
BEGIN
    ALTER TABLE [Stock] ADD [ItemName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240812101048_Stock'
)
BEGIN
    ALTER TABLE [Stock] ADD [SalePrice] decimal(18,2) NOT NULL DEFAULT 0.0;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240812101048_Stock'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240812101048_Stock', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240819184246_Bas'
)
BEGIN
    ALTER TABLE [Basket] ADD [BasketId1] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240819184246_Bas'
)
BEGIN
    CREATE INDEX [IX_Basket_BasketId1] ON [Basket] ([BasketId1]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240819184246_Bas'
)
BEGIN
    ALTER TABLE [Basket] ADD CONSTRAINT [FK_Basket_Basket_BasketId1] FOREIGN KEY ([BasketId1]) REFERENCES [Basket] ([BasketId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240819184246_Bas'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240819184246_Bas', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240822165517_Cust'
)
BEGIN
    ALTER TABLE [Transanction] DROP CONSTRAINT [FK_Transanction_Customer_CustomerId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240822165517_Cust'
)
BEGIN
    DROP INDEX [IX_Transanction_CustomerId] ON [Transanction];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240822165517_Cust'
)
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Transanction]') AND [c].[name] = N'CustomerId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Transanction] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Transanction] DROP COLUMN [CustomerId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240822165517_Cust'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD [CustomerId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240822165517_Cust'
)
BEGIN
    CREATE INDEX [IX_OrderDetail_CustomerId] ON [OrderDetail] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240822165517_Cust'
)
BEGIN
    ALTER TABLE [OrderDetail] ADD CONSTRAINT [FK_OrderDetail_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240822165517_Cust'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240822165517_Cust', N'8.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    ALTER TABLE [OrderDetail] DROP CONSTRAINT [FK_OrderDetail_Customer_CustomerId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    DROP INDEX [IX_OrderDetail_CustomerId] ON [OrderDetail];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderDetail]') AND [c].[name] = N'CustomerId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [OrderDetail] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [OrderDetail] DROP COLUMN [CustomerId];
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    ALTER TABLE [Order] ADD [CustomerId] int NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    ALTER TABLE [Order] ADD [Debtor] bit NOT NULL DEFAULT CAST(0 AS bit);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    CREATE INDEX [IX_Order_CustomerId] ON [Order] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    ALTER TABLE [Order] ADD CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([CustomerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240908134837_Custordes'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240908134837_Custordes', N'8.0.8');
END;
GO

COMMIT;
GO


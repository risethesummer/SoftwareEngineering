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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812102946_initial')
BEGIN
    CREATE TABLE [UserAccounts] (
        [ID] uniqueidentifier NOT NULL,
        [Account] nvarchar(50) NOT NULL,
        [Password] varbinary(16) NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Email] nvarchar(100) NULL,
        [DayOfBirth] datetime2 NOT NULL,
        [Address] nvarchar(100) NULL,
        CONSTRAINT [PK_UserAccounts] PRIMARY KEY ([ID]),
        CONSTRAINT [AlternateKey_Account] UNIQUE ([Account])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812102946_initial')
BEGIN
    CREATE TABLE [ResetPasswordRequests] (
        [UserId] uniqueidentifier NOT NULL,
        [Code] int NOT NULL,
        [Time] datetime2 NOT NULL,
        CONSTRAINT [PK_ResetPasswordRequests] PRIMARY KEY ([UserId]),
        CONSTRAINT [FK_ResetPasswordRequests_UserAccounts_UserId] FOREIGN KEY ([UserId]) REFERENCES [UserAccounts] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210812102946_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210812102946_initial', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210813162840_changeDTUsers')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserAccounts]') AND [c].[name] = N'DayOfBirth');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UserAccounts] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [UserAccounts] ALTER COLUMN [DayOfBirth] nvarchar(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210813162840_changeDTUsers')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210813162840_changeDTUsers', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [Movies] (
        [ID] uniqueidentifier NOT NULL,
        [Name] nvarchar(100) NOT NULL,
        [Year] int NOT NULL,
        [Nation] nvarchar(20) NOT NULL,
        [Genre] nvarchar(20) NOT NULL,
        [RequiredAge] int NOT NULL,
        [Duration] int NOT NULL,
        [Description] nvarchar(1000) NOT NULL,
        [ImageID] uniqueidentifier NOT NULL,
        [IMDBStar] int NOT NULL,
        [YoutubeLink] nvarchar(100) NULL,
        CONSTRAINT [PK_Movies] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [Orders] (
        [ID] uniqueidentifier NOT NULL,
        [UserID] uniqueidentifier NOT NULL,
        [PurchasedTime] datetime2 NOT NULL,
        [IsPurchased] bit NOT NULL,
        [TotalPrice] bigint NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([ID]),
        CONSTRAINT [FK_Orders_UserAccounts_UserID] FOREIGN KEY ([UserID]) REFERENCES [UserAccounts] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [People] (
        [ID] uniqueidentifier NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [DayOfBirth] datetime2 NOT NULL,
        [Nation] nvarchar(20) NOT NULL,
        [Description] nvarchar(1000) NOT NULL,
        [ImageID] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_People] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [Products] (
        [ID] uniqueidentifier NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [ImageID] uniqueidentifier NOT NULL,
        [Type] nvarchar(30) NOT NULL,
        [Price] int NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([ID]),
        CONSTRAINT [AlternateKey_Product] UNIQUE ([Name])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [Theaters] (
        [ID] uniqueidentifier NOT NULL,
        [Name] nvarchar(50) NOT NULL,
        [Location] nvarchar(100) NOT NULL,
        CONSTRAINT [PK_Theaters] PRIMARY KEY ([ID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [Reviews] (
        [MovieID] uniqueidentifier NOT NULL,
        [UserID] uniqueidentifier NOT NULL,
        [Star] int NOT NULL,
        [Comment] nvarchar(1000) NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([MovieID], [UserID]),
        CONSTRAINT [FK_Reviews_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Reviews_UserAccounts_UserID] FOREIGN KEY ([UserID]) REFERENCES [UserAccounts] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [MovieStaff] (
        [MovieID] uniqueidentifier NOT NULL,
        [PersonID] uniqueidentifier NOT NULL,
        [Role] nvarchar(max) NULL,
        CONSTRAINT [PK_MovieStaff] PRIMARY KEY ([MovieID], [PersonID]),
        CONSTRAINT [FK_MovieStaff_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_MovieStaff_People_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [People] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [OrderProduct] (
        [OrderID] uniqueidentifier NOT NULL,
        [ProductID] uniqueidentifier NOT NULL,
        [Amount] int NOT NULL,
        CONSTRAINT [PK_OrderProduct] PRIMARY KEY ([OrderID], [ProductID]),
        CONSTRAINT [FK_OrderProduct_Orders_OrderID] FOREIGN KEY ([OrderID]) REFERENCES [Orders] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_OrderProduct_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [Tickets] (
        [ProductID] uniqueidentifier NOT NULL,
        [MovieID] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Tickets] PRIMARY KEY ([ProductID]),
        CONSTRAINT [FK_Tickets_Movies_MovieID] FOREIGN KEY ([MovieID]) REFERENCES [Movies] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_Tickets_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [TheaterProducts] (
        [TheaterID] uniqueidentifier NOT NULL,
        [ProductID] uniqueidentifier NOT NULL,
        [Remains] int NOT NULL,
        CONSTRAINT [PK_TheaterProducts] PRIMARY KEY ([TheaterID], [ProductID]),
        CONSTRAINT [FK_TheaterProducts_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ID]) ON DELETE CASCADE,
        CONSTRAINT [FK_TheaterProducts_Theaters_TheaterID] FOREIGN KEY ([TheaterID]) REFERENCES [Theaters] ([ID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE TABLE [TicketShowTime] (
        [TicketID] uniqueidentifier NOT NULL,
        [ShowTime] datetime2 NOT NULL,
        CONSTRAINT [PK_TicketShowTime] PRIMARY KEY ([TicketID], [ShowTime]),
        CONSTRAINT [FK_TicketShowTime_Tickets_TicketID] FOREIGN KEY ([TicketID]) REFERENCES [Tickets] ([ProductID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE INDEX [IX_MovieStaff_PersonID] ON [MovieStaff] ([PersonID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE INDEX [IX_OrderProduct_ProductID] ON [OrderProduct] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE INDEX [IX_Orders_UserID] ON [Orders] ([UserID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE INDEX [IX_Reviews_UserID] ON [Reviews] ([UserID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE INDEX [IX_TheaterProducts_ProductID] ON [TheaterProducts] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    CREATE INDEX [IX_Tickets_MovieID] ON [Tickets] ([MovieID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822031213_addTables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210822031213_addTables', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822140236_finish')
BEGIN
    DROP TABLE [TicketShowTime];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822140236_finish')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'ImageID');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Products] DROP COLUMN [ImageID];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822140236_finish')
BEGIN
    ALTER TABLE [Tickets] ADD [ShowTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822140236_finish')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210822140236_finish', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822143313_addServer')
BEGIN
    ALTER TABLE [Tickets] ADD [Seat] nvarchar(5) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822143313_addServer')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210822143313_addServer', N'5.0.8');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822174059_fixServer')
BEGIN
    DROP TABLE [OrderProduct];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822174059_fixServer')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'TotalPrice');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Orders] DROP COLUMN [TotalPrice];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822174059_fixServer')
BEGIN
    ALTER TABLE [Orders] ADD [ProductID] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822174059_fixServer')
BEGIN
    CREATE INDEX [IX_Orders_ProductID] ON [Orders] ([ProductID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822174059_fixServer')
BEGIN
    ALTER TABLE [Orders] ADD CONSTRAINT [FK_Orders_Products_ProductID] FOREIGN KEY ([ProductID]) REFERENCES [Products] ([ID]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210822174059_fixServer')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210822174059_fixServer', N'5.0.8');
END;
GO

COMMIT;
GO


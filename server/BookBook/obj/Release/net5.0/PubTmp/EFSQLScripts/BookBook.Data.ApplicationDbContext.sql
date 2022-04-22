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


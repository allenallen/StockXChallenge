
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server Compact Edition
-- --------------------------------------------------
-- Date Created: 03/16/2016 15:08:09
-- Generated from EDMX file: C:\Users\OJT\Documents\Visual Studio 2012\Projects\Portfolio\PortfolioController\Model1.edmx
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- NOTE: if the constraint does not exist, an ignorable error will be reported.
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- NOTE: if the table does not exist, an ignorable error will be reported.
-- --------------------------------------------------

    DROP TABLE [Cash];
GO
    DROP TABLE [ClientAverageCost];
GO
    DROP TABLE [MatchedOrder];
GO
    DROP TABLE [PosCost];
GO
    DROP TABLE [Summary];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ClientAverageCosts'
CREATE TABLE [ClientAverageCosts] (
    [Date] nvarchar(100)  NULL,
    [Side] nvarchar(8)  NULL,
    [Cost] decimal(19,4)  NULL,
    [Volume] int  NULL,
    [NetVolume] int  NULL,
    [BuyPrice] decimal(19,4)  NULL,
    [SellPrice] decimal(19,4)  NULL,
    [AverageCost] decimal(19,4)  NULL,
    [ClientAverageCostID] int IDENTITY(1,1) NOT NULL,
    [SumOfNetPrice] decimal(19,4)  NULL,
    [MatchedOrderID] int  NULL
);
GO

-- Creating table 'Summaries'
CREATE TABLE [Summaries] (
    [SummaryID] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(100)  NULL,
    [AccountCode] nvarchar(100)  NULL,
    [StockCode] nvarchar(8)  NULL,
    [BoardLot] nvarchar(8)  NULL,
    [AveragePriceBuy] decimal(19,4)  NULL,
    [AveragePriceSell] decimal(19,4)  NULL,
    [NetQuantity] int  NULL
);
GO

-- Creating table 'PosCosts'
CREATE TABLE [PosCosts] (
    [PosCostID] int IDENTITY(1,1) NOT NULL,
    [AccountCode] nvarchar(100)  NULL,
    [StockCode] nvarchar(100)  NULL,
    [Volume] int  NULL,
    [AverageCost] decimal(19,4)  NULL
);
GO

-- Creating table 'MatchedOrders'
CREATE TABLE [MatchedOrders] (
    [MatchedOrderID] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(100)  NULL,
    [AccountCode] nvarchar(100)  NULL,
    [Side] nvarchar(8)  NULL,
    [BoardLot] nvarchar(8)  NULL,
    [StockCode] nvarchar(8)  NULL,
    [Price] decimal(19,4)  NULL,
    [Quantity] int  NULL,
    [OrderDatetime] datetime  NULL,
    [MatchDate] nvarchar(100)  NULL,
    [NetPrice] decimal(19,4)  NULL,
    [SumOfNetPrice] decimal(19,4)  NULL,
    [NetVolume] int  NULL,
    [AvgCost] decimal(19,4)  NULL
);
GO

-- Creating table 'Cashes'
CREATE TABLE [Cashes] (
    [CashId] int IDENTITY(1,1) NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [Margin] decimal(19,4)  NOT NULL,
    [Withdrawable] decimal(19,4)  NOT NULL,
    [AccountCode] nvarchar(100)  NULL,
    [LastUpdatedDate] nvarchar(100)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ClientAverageCostID] in table 'ClientAverageCosts'
ALTER TABLE [ClientAverageCosts]
ADD CONSTRAINT [PK_ClientAverageCosts]
    PRIMARY KEY ([ClientAverageCostID] );
GO

-- Creating primary key on [SummaryID] in table 'Summaries'
ALTER TABLE [Summaries]
ADD CONSTRAINT [PK_Summaries]
    PRIMARY KEY ([SummaryID] );
GO

-- Creating primary key on [PosCostID] in table 'PosCosts'
ALTER TABLE [PosCosts]
ADD CONSTRAINT [PK_PosCosts]
    PRIMARY KEY ([PosCostID] );
GO

-- Creating primary key on [MatchedOrderID] in table 'MatchedOrders'
ALTER TABLE [MatchedOrders]
ADD CONSTRAINT [PK_MatchedOrders]
    PRIMARY KEY ([MatchedOrderID] );
GO

-- Creating primary key on [CashId] in table 'Cashes'
ALTER TABLE [Cashes]
ADD CONSTRAINT [PK_Cashes]
    PRIMARY KEY ([CashId] );
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
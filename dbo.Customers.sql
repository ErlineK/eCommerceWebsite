USE [eCommerce]
GO

/****** Object: Table [dbo].[Customers] Script Date: 7/17/2019 11:44:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Customers] (
    [CustomerId] INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (50) NOT NULL,
    [LastName]   VARCHAR (50) NOT NULL,
    [Address]    VARCHAR (50) NULL,
    [City]       VARCHAR (20) NULL,
    [Province]   VARCHAR (15) NULL,
    [PostalCode] VARCHAR (6)  NULL
);



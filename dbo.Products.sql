USE [eCommerce]
GO

/****** Object: Table [dbo].[Products] Script Date: 7/17/2019 11:44:33 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products] (
    [ProductId]   INT            IDENTITY (1, 1) NOT NULL,
    [manufacCode] VARCHAR (10)   NULL,
    [description] VARCHAR (50)   NULL,
    [pic]         VARCHAR (30)   NULL,
    [qtyOnHand]   INT            NULL,
    [price]       DECIMAL (8, 2) NULL
);



CREATE TABLE ShoppingItems
(
    ItemId      UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Name        NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price       DECIMAL(18, 2)   NOT NULL
);
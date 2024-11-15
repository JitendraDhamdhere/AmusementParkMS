CREATE TABLE Food_Beverages (
    ItemID INT PRIMARY KEY IDENTITY(1,1),
    ItemType VARCHAR(50) NOT NULL,  
    ItemName VARCHAR(50) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Quantity INT NOT NULL,
    Availability VARCHAR(255) NOT NULL 
);
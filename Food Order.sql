CREATE TABLE Food_Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    VisitorID INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    ItemID INT NOT NULL,
    Quantity INT NOT NULL,
    TotalCost DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(255) NOT NULL,  -- Pending, In Progress, Delivered, Cancelled
    FOREIGN KEY (VisitorID) REFERENCES Visitors(VisitorID),
    FOREIGN KEY (ItemID) REFERENCES Food_Beverages(ItemID)
);
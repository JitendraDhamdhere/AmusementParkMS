CREATE TABLE Packages (
    PackageID INT PRIMARY KEY IDENTITY(1,1),
    PackageName VARCHAR(255) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Duration VARCHAR(255) NOT NULL,  
    Includes VARCHAR(255) NOT NULL,  
    Status VARCHAR(255) NOT NULL  
    );
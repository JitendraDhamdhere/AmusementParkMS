CREATE TABLE Maintenance (
  MaintenanceID INT PRIMARY KEY,
  RideID INT,
  MaintenanceDate DATE,
  MaintenanceType VARCHAR(50),
  Status VARCHAR(20),
  AssignedTo VARCHAR(50),
  CompletedBy VARCHAR(50),
  CompletionDate DATE,
  FOREIGN KEY (RideID) REFERENCES Rides(RideID)
);

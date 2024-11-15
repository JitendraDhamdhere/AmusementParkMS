CREATE TABLE Ticket_Cancellations (
    CancellationID INT PRIMARY KEY IDENTITY(1,1),
    BookingID INT NOT NULL,
    CancellationDate DATE NOT NULL,
    CancellationReason VARCHAR(50) NOT NULL,
    RefundAmount DECIMAL(10, 2) NULL,
    Status VARCHAR(50) NOT NULL 
    FOREIGN KEY (BookingID) REFERENCES Ride_Ticket_Bookings(BookingID)
);
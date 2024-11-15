CREATE TABLE Ride_Ticket_Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    RideID INT NOT NULL,
    CustomerID INT NOT NULL,
    BookingDate DATE NOT NULL,
    BookingTime TIME NOT NULL,
    TicketType VARCHAR(255) NOT NULL,  -- e.g., adult, child, etc.
    TicketPrice DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(255) NOT NULL 
);
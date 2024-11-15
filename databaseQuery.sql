CREATE TABLE [dbo].[Employee] (
    [EmployeeID] INT           IDENTITY (1, 1) NOT NULL,
    [Username]   NVARCHAR (50) NOT NULL,
    [Password]   NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
);

CREATE TABLE [dbo].[Admin] (
    [AdminID]  INT          IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (50) NOT NULL,
    [Password] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([AdminID] ASC)
);


CREATE TABLE [dbo].[Employess] (
    [EmployeeId]   INT          NOT NULL,
    [EmployeeName] VARCHAR (50) NULL,
    [DOB]          DATE         NULL,
    [Contact]      VARCHAR (50) NULL,
    [Address]      VARCHAR (50) NULL,
    [JobTitle]     VARCHAR (50) NULL,
    [HireDate]     DATE         NULL,
    [Department]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC)
);

CREATE TABLE [dbo].[Feedback] (
    [FeedbackID]   INT          NOT NULL,
    [CustomerName] VARCHAR (50) NOT NULL,
    [Email]        VARCHAR (50) NOT NULL,
    [Rating]       INT          NOT NULL,
    [Comments]     VARCHAR (50) NOT NULL,
    [Date]         DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([FeedbackID] ASC)
);

CREATE TABLE [dbo].[food_beverages] (
    [ItemId]       INT          NOT NULL,
    [ItemName]     VARCHAR (50) NOT NULL,
    [ItemType]     VARCHAR (50) NOT NULL,
    [Category]     VARCHAR (50) NOT NULL,
    [Price]        DECIMAL (18) NULL,
    [Quantity]     DECIMAL (18) NULL,
    [Availability] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([ItemId] ASC)
);


CREATE TABLE [dbo].[FoodOrder] (
    [OrderId]     INT          NOT NULL,
    [VisitorName] VARCHAR (50) NOT NULL,
    [OrderDate]   DATETIME     NULL,
    [ItemName]    VARCHAR (50) NOT NULL,
    [Quantity]    DECIMAL (18) NULL,
    [TotalCost]   DECIMAL (18) NULL,
    [Status]      VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([OrderId] ASC)
);


CREATE TABLE [dbo].[Maintenance] (
    [MaintenanceId]    INT          NOT NULL,
    [Ridename]         VARCHAR (50) NULL,
    [Maintenancedate]  VARCHAR (50) NULL,
    [Type]             VARCHAR (50) NULL,
    [Status]           VARCHAR (50) NULL,
    [Assignedto]       VARCHAR (50) NULL,
    [completedby]      VARCHAR (50) NULL,
    [completationdate] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([MaintenanceId] ASC)
);


CREATE TABLE [dbo].[Rides] (
    [RideID]    INT          NOT NULL,
    [RideName]  VARCHAR (50) NOT NULL,
    [RideType]  VARCHAR (50) NOT NULL,
    [Capacity]  INT          NOT NULL,
    [Duration]  TIME (7)     NOT NULL,
    [Opentime]  TIME (7)     NOT NULL,
    [closetime] TIME (7)     NOT NULL,
    [status]    VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RideID] ASC)
);


CREATE TABLE [dbo].[Supplier] (
    [SupplierId]  INT          NOT NULL,
    [Name]        VARCHAR (50) NULL,
    [Address]     VARCHAR (50) NULL,
    [Phone]       VARCHAR (50) NULL,
    [ProductName] VARCHAR (50) NULL,
    [Price]       VARCHAR (50) NULL,
    [Quantity]    VARCHAR (50) NULL,
    [OrderDate]   VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([SupplierId] ASC)
);

CREATE TABLE [dbo].[Ticket_Cancle] (
    [TicketId]     INT          NOT NULL,
    [VisitorName]  VARCHAR (50) NOT NULL,
    [TicketType]   VARCHAR (50) NOT NULL,
    [TicketStatus] VARCHAR (10) NULL,
    [PurchaseDate] DATETIME     NULL,
    [RefundAmount] DECIMAL (18) NULL,
    [CancleReason] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([TicketId] ASC),
    CHECK ([TicketStatus]='Cancelled' OR [TicketStatus]='Booked')
);


CREATE TABLE [dbo].[TicketBooking] (
    [TicketId]      INT          NOT NULL,
    [Ride_Name]     VARCHAR (50) NOT NULL,
    [Visitior_Name] VARCHAR (50) NOT NULL,
    [TicketType]    VARCHAR (50) NOT NULL,
    [TicketPrice]   DECIMAL (18) NOT NULL,
    [Booking_Date]  DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([TicketId] ASC)
);

CREATE TABLE [dbo].[Visitor] (
    [VisitorId]     INT          NOT NULL,
    [VisitorName]   VARCHAR (50) NOT NULL,
    [Age]           INT          NULL,
    [ContactNumber] VARCHAR (11) NOT NULL,
    [Address]       VARCHAR (50) NOT NULL,
    [VisitDate]     DATETIME     NULL,
    [TicketType]    VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([VisitorId] ASC)
);
